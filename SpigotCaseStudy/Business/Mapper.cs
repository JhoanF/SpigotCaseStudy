using SpigotCaseStudy.Models;
using SpigotCaseStudy.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpigotCaseStudy.Business
{
    public class Mapper
    {

        public static MediaItem MapToMediaItem(DailyImage image)
        {
            return new MediaItem()
            {
                DateCreated = image.date,
                Description = image.explanation,
                Title = image.title,
                ImageUrl = image.url,
                VideoUrl = image.media_type != null && image.media_type == "video" ? image.url: null,
                LargeImageUrl = image.hdurl,
                MediaType = image.media_type
            };
        }
        public static DailyImage MapToDailyImage(MediaItem item)
        {
            return new DailyImage()
            {
                date = item.DateCreated,
                explanation = item.Description,
                title = item.Title,
                hdurl = item.LargeImageUrl,
                url = item.ImageUrl,
                media_type = item.MediaType
            };
        }
    }
}
