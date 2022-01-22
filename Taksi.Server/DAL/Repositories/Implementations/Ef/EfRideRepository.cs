using Microsoft.EntityFrameworkCore;
using Taksi.Server.DAL.Entities;

namespace Taksi.Server.DAL.Repositories.Implementations.Ef
{
    public class EfRideRepository : EfRepository<RideEntity>
    {
        public EfRideRepository(EfContext context) : base(context, context.Rides)
        {
        }
    }
}