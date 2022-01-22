using Microsoft.EntityFrameworkCore;
using Taksi.Server.DAL.Entities;

namespace Taksi.Server.DAL.Repositories.Implementations.Ef
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<DriverEntity> Drivers { get; set; }
        public DbSet<RideEntity> Rides { get; set; }
        public DbSet<CreditCardEntity> CreditCards { get; set; }
    }
}