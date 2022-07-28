using CsvReader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CsvReader.Infrastructure.Persistance.Configurations
{
    public class CodeTypeConfiguration : IEntityTypeConfiguration<Code>
    {
        public void Configure(EntityTypeBuilder<Code> builder)
        {
            builder.ToTable("Codes", BusinessProcessesContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .UseHiLo("codesSeq", BusinessProcessesContext.DEFAULT_SCHEMA);

            builder.Property(o => o.StringCode)
                .IsRequired();

            builder.Ignore(o => o.BusinessProcessId);
        }
    }
}
