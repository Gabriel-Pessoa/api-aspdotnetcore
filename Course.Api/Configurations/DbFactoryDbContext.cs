using Course.Api.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Course.Api.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CourseDbContext>
    {
        public CourseDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CourseDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=course;user=sa;password=App@220030"); 
            CourseDbContext context = new CourseDbContext(optionsBuilder.Options);

            return context;
        }
    }
}
