using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Taksi.DTO.Enums;
using Taksi.Server.DAL.Entities;

namespace Taksi.Server.DAL.Repositories.Implementations.Ef
{
    public class EfRideRepository : EfRepository<RideEntity>
    {
        public EfRideRepository(DbContext context, DbSet<RideEntity> container)
            : base(context, container)
        {
        }

        public new async Task<RideEntity> GetByIdAsync(Guid id)
        {
            return await GetDbSet().FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public new async Task<IEnumerable<RideEntity>> GetAllAsync()
        {
            return await GetDbSet().ToListAsync();
        }

        public new async Task<IEnumerable<RideEntity>> GetWhereAsync(Func<RideEntity, bool> predicate)
        {
            return await GetDbSet().Where(predicate).AsQueryable().ToListAsync();
        }

        private IIncludableQueryable<RideEntity, RideStatus> GetDbSet()
        {
            return DbSetContainer.Include(entity => entity.Status);   
        }
    }
}