

using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using Persistence.Repositories;
using System.Collections.Concurrent;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderManagementDbContext _context;
        private readonly ConcurrentDictionary<string, object> _repository;

        public UnitOfWork(OrderManagementDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>() 
            where TEntity : BaseEntity
            => (IGenericRepository<TEntity>)_repository
            .GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity>(_context));

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();
        public async ValueTask DisposeAsync()
            => await _context.DisposeAsync();
    }
}
