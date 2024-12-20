﻿using RentACar.Data.Models.Interfaces;
using System.Linq.Expressions;
namespace RentACar.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId> where TType : class, ISoftDeletable
    {
        Task<TType> GetByIdAsync(TId id);

        Task<TType> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate);

        Task<IEnumerable<TType>> GetAllAsync();

        IQueryable<TType> GetAllAttached();

        Task ApplyAsModified(TType entity);
        Task AddAsync(TType item);

        Task AddRangeAsync(TType[] items);

        Task<bool> DeleteAsync(TType entity);

        Task<bool> UpdateAsync(TType item);

        Task<bool> SaveChangesAsync();
    }
}
