using System.Linq;
using Course.Api.Business.Entities;
using Course.Api.Infraestruture.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Course.Api.Infraestruture.Data
{
    public class CourseDbContext : DbContext //Herdando do EntityFramework
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
        {
            if (this.Database.GetPendingMigrations().Count() > 0)
            {
                this.Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<CourseEntity> Course { get; set; }

    }
}
