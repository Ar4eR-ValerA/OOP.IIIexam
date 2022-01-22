using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Taksi.Server.DAL.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetWhereAsync(Func<T, bool> predicate);

        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(Guid id);
    }
}