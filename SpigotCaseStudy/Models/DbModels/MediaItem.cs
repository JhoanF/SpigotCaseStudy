using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpigotCaseStudy.Models.DbModels
{
    public class MediaItem
    {
        public int MediaItemId { get; set; }
        public string ExternalId {get;set;}
        public int ApiEndpointId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string LargeImageUrl { get; set; }
        public string VideoUrl { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
        public DateTime DateLastAccessed { get; set; }
        /// <summary>
        ///     Comma delimeted string of keywords used to label content. 
        /// </summary>
        public string KeyWords { get; set; }
        public string MediaType { get; set; }
    }
}
