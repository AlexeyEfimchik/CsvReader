using CsvReader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CsvReader.Infrastructure.Persistance.Configurations
{
    public class DivisionTypeConfiguration : IEntityTypeConfiguration<Division>
    {
        public void Configure(EntityTypeBuilder<Division> builder)
        {
            builder.ToTable("Divisions", BusinessProcessesContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .UseHiLo("divisionsSeq", BusinessProcessesContext.DEFAULT_SCHEMA);

            builder.Property(o => o.Name)
                .IsRequired();

            builder.Ignore(o => o.BusinessProcessId);
        }
    }
}
