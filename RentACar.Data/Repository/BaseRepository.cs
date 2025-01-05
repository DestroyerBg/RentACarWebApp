using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RentACar.Data.Models.Interfaces;
using RentACar.Data.Repository.Interfaces;

namespace RentACar.Data.Repository
{
    public class BaseRepository<TType, TId> : IRepository<TType, TId>
    where TType : class, ISoftDeletable
    {
        private readonly RentACarDbContext dbContext;
        private readonly DbSet<TType> dbSet;

        public BaseRepository(RentACarDbContext _dbContext)
        {
            dbContext = _dbContext;
            dbSet = dbContext.Set<TType>();
        }
        public virtual async Task<TType> GetByIdAsync(TId id)
        {
            TType? entity = await dbSet.FindAsync(id);  

            return entity;
        }

        public virtual async Task<TType> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate)
        {
            TType entity = await dbSet.FirstOrDefaultAsync(predicate);

            return entity;
        }

        public virtual async Task<IEnumerable<TType>> GetAllAsync()
        {
            IEnumerable<TType> entities = await dbSet.ToArrayAsync();

            return entities;
        }

        public virtual IQueryable<TType> GetAllAttached()
        {
            IQueryable<TType> entities = dbSet.AsQueryable();

            return entities;
        }

        public virtual async Task ApplyAsModified(TType entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task AddAsync(TType item)
        {
            EntityEntry<TType> type = dbSet.Add(item);

            await SaveChangesAsync();
        }

        public virtual async Task AddRangeAsync(TType[] items)
        {
            dbSet.AddRangeAsync(items);

            await SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteAsync(TType entity)
        {
            try
            {
                entity.IsDeleted = true;

                await SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual async Task<bool> UpdateAsync(TType item)
        {
            try
            {
                dbSet.Attach(item);
                dbSet.Entry(item).State = EntityState.Modified;

                await SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            try
            {
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
