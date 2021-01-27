using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Course.Api.Business.Entities;


namespace Course.Api.Infraestruture.Data.Mappings
{
    public class CourseMapping : IEntityTypeConfiguration<CourseEntity>
    {
        public void Configure(EntityTypeBuilder<CourseEntity> builder)
        {
            builder.ToTable("TB_COURSE");
            builder.HasKey(p => p.Code);
            builder.Property(p => p.Code).ValueGeneratedOnAdd(); //Auto incrementada
            builder.Property(p => p.Name);
            builder.Property(p => p.Description);
            builder.HasOne(p => p.User)
                .WithMany().HasForeignKey(fk => fk.UserCode);
        }
    }
}
