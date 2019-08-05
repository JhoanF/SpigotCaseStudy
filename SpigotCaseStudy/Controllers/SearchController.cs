using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpigotCaseStudy.Business.Handlers;
using SpigotCaseStudy.Models.DbModels;

namespace SpigotCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SpigotCaseStudyDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public SearchController(SpigotCaseStudyDbContext context, IHttpClientFactory clientFactory, IConfiguration config)
        {
            _context = context;
            _clientFactory = clientFactory;
            _config = config;
        }

        // GET: api/dailyimage
        [HttpGet]
        public async Task<IActionResult> GetSearch(string q)
        {
            //search api
            NasaContentApiRequestHandler requestHandler = new NasaContentApiRequestHandler(_context, _clientFactory.CreateClient(), _config.GetValue<string>("ApiKey:NASA", ""), _config);
            string reply = await requestHandler.OnGet(q);
            if (requestHandler.RequestErrored)
                throw new Exception("500: Internal Server Error - Unable to retrieve daily image.");
        
            return new JsonResult(JsonConvert.SerializeObject(reply));
        }
    }
}
