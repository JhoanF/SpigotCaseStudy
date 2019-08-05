using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpigotCaseStudy.Models.DbModels
{
    public static class DbInitializer
    {
        public static void Initialize(SpigotCaseStudyDbContext context)
        {
            context.Database.EnsureCreated();

            //if (context.ApiEndpoints.Any()) { return; }
            //else
            //{
            //    var apiEndpoints = new ApiEndpoint[]
            //    {
            //        new ApiEndpoint(){ApiName="images.nasa.gov",RootUrl="images.nasa.gov",EndpointPath="/search",EndpointName="search", Method="GET"},
            //        new ApiEndpoint(){ApiName="APOD",RootUrl="api.nasa.gov",EndpointPath="/planetary/apod",EndpointName="apod",Method="GET"}

            //    };
            //    foreach (var endpoint in apiEndpoints)
            //    {
            //        context.ApiEndpoints.Add(endpoint);
            //    }
            //    context.SaveChanges();
            //}
            
        }

    }
}
