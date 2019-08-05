using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SpigotCaseStudy.Models.DbModels
{
    public class SpigotCaseStudyDbContext : DbContext
    {

        public SpigotCaseStudyDbContext(DbContextOptions<SpigotCaseStudyDbContext> options) : base(options)
        {
        }

        public DbSet<MediaItem> MediaItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MediaItem>().ToTable("MediaItems");
        }
    }
}
