using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class CallCenterContext:DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<Status> Statuses { get; set; }
        //Thieu
        public DbSet<MemberAccessToken> MemberAccessTokens { get; set; }        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MemberAccessToken>().HasNoKey();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            ConfigurationBuilder builder = new ConfigurationBuilder();
            var configuration = builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CallCenter"));
        }
    }
}
