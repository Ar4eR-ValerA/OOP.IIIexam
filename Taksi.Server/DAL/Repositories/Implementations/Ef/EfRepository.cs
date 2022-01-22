using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taksi.Server.DAL.Entities.Helpers;
using Taksi.Server.DAL.Exceptions;
using Taksi.Server.DAL.Repositories.Interfaces;

namespace Taksi.Server.DAL.Repositories.Implementations.Ef
{
    public class EfRepository<T> : IRepository<T>
        where T : class, IIdentifiable
    {
        public EfRepository(DbContext context, DbSet<T> container)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DbSetContainer = container ?? throw new ArgumentNullException(nameof(container));
        }

        protected DbContext Context { get; }
        protected DbSet<T> DbSetContainer { get; }
        
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSetContainer.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSetContainer.ToListAsync();   
        }
        
        public IEnumerable<T> GetWhereAsync(Func<T, bool> predicate)
        {
            return DbSetContainer.Where(predicate).AsQueryable().ToList();
        }

        public async Task InsertAsync(T entity)
        {
            if (await DbSetContainer.FindAsync(entity.Id) != null)
            {
                throw new EntityAlreadyExistsException(
                    $"Trying to insert an entity with id = {entity.Id}, " + 
                    "but entity with such id is already stored in database");
            }
            
            DbSetContainer.Add(entity);
            SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            if (await DbSetContainer.FindAsync(entity.Id) == null)
            {
                throw new EntityDoesNotExistException(
                    $"Trying to update an entity with id = {entity.Id}, " + 
                    "but entity with such id is not stored in database");
            }
                
            DbSetContainer.Update(entity);
            SaveChanges();
        }

        public async Task RemoveAsync(Guid id)
        {
            T entity = await DbSetContainer.FindAsync(id);
            if (entity == null)
            {
                throw new EntityDoesNotExistException(
                    $"Trying to remove an entity with id = {id}, " + "" +
                    "but entity with such id is not stored in database");
            }
                
            DbSetContainer.Remove(entity);
            SaveChanges();
        }

        private async void SaveChanges() => await Context.SaveChangesAsync();
    }
}