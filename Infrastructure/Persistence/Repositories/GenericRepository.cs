
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly OrderManagementDbContext _context;
        public GenericRepository(OrderManagementDbContext context)
        {
            _context = context ;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() 
            => await _context.Set<TEntity>().ToListAsync();
        public async Task<TEntity?> GetByIdAsync(int id)
            => await _context.Set<TEntity>().FindAsync(id);
        public async Task AddAsync(TEntity entity) 
            => await _context.Set<TEntity>().AddAsync(entity);
        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);
    }
}
