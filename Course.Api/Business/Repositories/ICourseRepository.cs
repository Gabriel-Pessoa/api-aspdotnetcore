using System.Collections.Generic;
using Course.Api.Business.Entities;

namespace Course.Api.Business.Repositories
{
    public interface ICourseRepository
    {
        void Add(CourseEntity course);
        void Commit();
        IList<CourseEntity> GetForUser(int userCode);
    }
}