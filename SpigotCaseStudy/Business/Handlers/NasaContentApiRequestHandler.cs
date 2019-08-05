using Microsoft.Extensions.Configuration;
using SpigotCaseStudy.Models;
using SpigotCaseStudy.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpigotCaseStudy.Business.Handlers
{
    public class NasaContentApiRequestHandler : ConfigurationAccess
    {
        #region Properties
        private readonly SpigotCaseStudyDbContext _context;
        private readonly HttpClient _client;
        private static string _apiKey;

        public ApiEndpoint Endpoint { get; set; }
        public bool RequestErrored { get; set; }
        #endregion

        #region Constructors
        public NasaContentApiRequestHandler(SpigotCaseStudyDbContext context, HttpClient client, string apiKey, IConfiguration config)
            : base(config)
        {
            _context = context;
            _client = client;
            _apiKey = apiKey;
            Endpoint = ApiEndpoints.First(x => x.EndpointName == "search");
        }
        #endregion

        #region Methods
        public async Task<string> OnGet(string q)
        {
            var url = $"https://{Endpoint.RootUrl}{Endpoint.EndpointPath}?q={q}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"Basic {_apiKey}");
            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                RequestErrored = true;

            return await response.Content.ReadAsStringAsync();
        }
        #endregion
    }
}
