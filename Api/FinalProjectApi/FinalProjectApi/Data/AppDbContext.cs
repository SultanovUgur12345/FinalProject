using FinalProjectApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

        public DbSet<Worker> Workers { get; set; }
        public DbSet<ShipSlider> ShipSliders { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<ShipHeroVideo> ShipHeroVideos { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<AboutUsAchievement> AboutUsAchievements { get; set; }
        public DbSet<Setting> Settings { get; set; }


    }
}
