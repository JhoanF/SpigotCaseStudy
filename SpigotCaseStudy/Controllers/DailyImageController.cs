using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpigotCaseStudy.Business;
using SpigotCaseStudy.Business.Handlers;
using SpigotCaseStudy.Models;
using SpigotCaseStudy.Models.DbModels;

namespace SpigotCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyImageController : ControllerBase
    {

        private readonly SpigotCaseStudyDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public DailyImageController(SpigotCaseStudyDbContext context, IHttpClientFactory clientFactory, IConfiguration config)
        {
            _context = context;
            _clientFactory = clientFactory;
            _config = config;
        }

        // GET: api/dailyimage
        [HttpGet]
        public async Task<IActionResult> GetDailyImage(DateTime? date)
        {
            //search db 
            var searchDate = (date.HasValue ? date.Value.ToString("yyyy-MM-dd") : DateTime.UtcNow.ToString("yyyy-MM-dd"));
            MediaItem mediaItemFromDb = _context.MediaItems.FirstOrDefault(x => x.DateCreated != null && x.DateCreated.ToString("yyyy-MM-dd") == searchDate);
            if (mediaItemFromDb != null)
                return new JsonResult(JsonConvert.SerializeObject(mediaItemFromDb));

            //search api
            DailyImageRequestHandler requestHandler = new DailyImageRequestHandler(_context, _clientFactory.CreateClient(), _config.GetValue<string>("ApiKey:NASA", ""), _config);
            DailyImage dailyImage =  await requestHandler.OnGet(date);
            if (requestHandler.DailyImageRequestErrored)
                throw new Exception("500: Internal Server Error - Unable to retrieve daily image.");
            
            //update db
            var mediaItem = Mapper.MapToMediaItem(dailyImage);
            mediaItem.ApiEndpointId = requestHandler.Endpoint.ApiEndpointId;
            mediaItem.DateLastAccessed = DateTime.UtcNow;
            _context.MediaItems.Add(mediaItem);

            await _context.SaveChangesAsync();

            return new JsonResult(JsonConvert.SerializeObject(mediaItem));
        }
    }
}