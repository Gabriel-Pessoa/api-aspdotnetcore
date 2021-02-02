using System.Collections.Generic;
using System.Linq;
using Course.Api.Business.Entities;
using Course.Api.Business.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Course.Api.Infraestruture.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseDbContext _context;

        public CourseRepository(CourseDbContext context)
        {
            _context = context;
        }

        public void Add(CourseEntity course)
        {
            _context.Course.Add(course);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IList<CourseEntity> GetForUser(int userCode)
        {
            return _context.Course.Include(i => i.User).Where(w => w.UserCode == userCode).ToList();
        }
    }
}