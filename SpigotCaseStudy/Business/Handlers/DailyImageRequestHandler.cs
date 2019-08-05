using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpigotCaseStudy.Models;
using SpigotCaseStudy.Models.DbModels;

namespace SpigotCaseStudy.Business.Handlers
{
    public class DailyImageRequestHandler : ConfigurationAccess
    {
        #region Properties
        private readonly SpigotCaseStudyDbContext _context;
        private readonly HttpClient _client;
        private static string _apiKey;

        public ApiEndpoint Endpoint { get; set; }
        public bool DailyImageRequestErrored { get; set; }
        #endregion

        #region Constructors
        public DailyImageRequestHandler(SpigotCaseStudyDbContext context, HttpClient client, string apiKey, IConfiguration config)
            : base(config)
        {
            _context = context;
            _client = client;
            _apiKey = apiKey;
            Endpoint = ApiEndpoints.First(x => x.EndpointName == "apod");
        }
        #endregion

        #region Methods
        public async Task<DailyImage> OnGet(DateTime? date)
        {
            var url = $"https://{Endpoint.RootUrl}{Endpoint.EndpointPath}?api_key={_apiKey}";
            if (date.HasValue)
                url += $"&date={date.Value.ToString("yyyy-MM-dd")}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _client.SendAsync(request);

            DailyImage image;
            if (!response.IsSuccessStatusCode)
            {
                image = new DailyImage();
                DailyImageRequestErrored = true;
            }
            else
                image = JsonConvert.DeserializeObject<DailyImage>(await response.Content.ReadAsStringAsync());

            return image;

        }
        #endregion
    }
}
