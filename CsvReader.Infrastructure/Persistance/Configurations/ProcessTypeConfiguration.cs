using CsvReader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CsvReader.Infrastructure.Persistance.Configurations
{
    public class ProcessTypeConfiguration : IEntityTypeConfiguration<Process>
    {
        public void Configure(EntityTypeBuilder<Process> builder)
        {
            builder.ToTable("Processes", BusinessProcessesContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .UseHiLo("processesSeq", BusinessProcessesContext.DEFAULT_SCHEMA);

            builder.Property(o => o.Name)
                .IsRequired();

            builder.Ignore(o => o.BusinessProcessId);
        }
    }
}
