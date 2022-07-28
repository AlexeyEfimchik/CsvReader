using CsvReader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CsvReader.Infrastructure.Persistance.Configurations
{
    public class BusinessProcessTypeConfiguration : IEntityTypeConfiguration<BusinessProcess>
    {
        public void Configure(EntityTypeBuilder<BusinessProcess> builder)
        {
            builder.ToTable("BusinessProcesses", BusinessProcessesContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .UseHiLo("businessProcessSeq", BusinessProcessesContext.DEFAULT_SCHEMA);

            builder.HasOne(o => o.Code)
                .WithMany(o => o.BusinessProcess)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Division)
                .WithMany(o => o.BusinessProcess)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Process)
                .WithMany(o => o.BusinessProcess)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.CodeId).IsRequired(false);
            builder.Property(o => o.DivisionId).IsRequired(false);
        }
    }
}
