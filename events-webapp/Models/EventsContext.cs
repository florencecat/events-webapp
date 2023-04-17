using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace wa_dev_coursework.Models.EventsContext
{
    public class EventsContext : IdentityDbContext
    {
        // Constructor
        public EventsContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // Attributes
        protected readonly IConfiguration configuration;

        // Properties
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }

        // Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
