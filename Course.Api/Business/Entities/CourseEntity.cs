namespace Course.Api.Business.Entities
{
    public class CourseEntity
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int UserCode { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
