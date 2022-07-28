using CsvReader.Domain.Entities;
using CsvReader.Infrastructure.Persistance.Configurations;
using CsvReader.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace CsvReader.Infrastructure.Persistance
{
    public class BusinessProcessesContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";
        public DbSet<Code> Codes { get; }
        public DbSet<Division> Divisions { get; }
        public DbSet<Process> Processes { get; }
        public DbSet<BusinessProcess> BusinessProcesses { get; }

        private readonly AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor;

        public BusinessProcessesContext(DbContextOptions<BusinessProcessesContext> options) : base(options) { }
        public BusinessProcessesContext(DbContextOptions<BusinessProcessesContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : this(options)
        {
            this.auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor ??
                throw new ArgumentNullException(nameof(auditableEntitySaveChangesInterceptor));

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(this.auditableEntitySaveChangesInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProcessTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DivisionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CodeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BusinessProcessTypeConfiguration());
        }
    }
}
