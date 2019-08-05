using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpigotCaseStudy.Models
{
    public class ApiEndpoint
    {
        public int ApiEndpointId { get; set; }
        public string ApiName { get; set; }
        public string RootUrl { get; set; }
        public string EndpointName { get; set; }
        public string EndpointPath { get; set; }
        public string Method { get; set; }
    }
}
