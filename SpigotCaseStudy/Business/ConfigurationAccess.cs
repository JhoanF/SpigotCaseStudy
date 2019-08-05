using Microsoft.Extensions.Configuration;
using SpigotCaseStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpigotCaseStudy.Business
{
    public class ConfigurationAccess
    {
        public IConfiguration Config { get; set; }

        public ConfigurationAccess(IConfiguration config)
        {
            Config = config;
        }

        public List<ApiEndpoint> ApiEndpoints { get { return Config.GetSection("ApiEndpoints").Get<List<ApiEndpoint>>(); } }
            
    }
}
