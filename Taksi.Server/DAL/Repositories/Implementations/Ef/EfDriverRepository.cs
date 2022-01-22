using Taksi.Server.DAL.Entities;

namespace Taksi.Server.DAL.Repositories.Implementations.Ef
{
    public class EfDriverRepository : EfRepository<DriverEntity>
    {
        public EfDriverRepository(EfContext context) : base(context, context.Drivers)
        {
        }
    }
}