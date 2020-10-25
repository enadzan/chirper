using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Chirper.Server.EF
{
    public class ChirpDbContext: DbContext
    {
        public ChirpDbContext(DbContextOptions<ChirpDbContext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DataSource=.\\;Initial Catalog=ChirpDb;IntegratedSecurity=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("seq_chirp_user")
                .StartsAt(1)
                .IncrementsBy(10);

            modelBuilder.HasSequence<long>("seq_chirp")
                .StartsAt(1)
                .IncrementsBy(100);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
