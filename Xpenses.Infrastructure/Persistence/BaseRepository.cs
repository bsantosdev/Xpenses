﻿namespace Xpenses.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xpenses.Application.Common.Interfaces.Persistence;

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly XpensesDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(XpensesDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            var primaryKey = _context.Entry(entity).Metadata.FindPrimaryKey().Properties.Single().Name;
            var primaryKeyValue = (Guid)_context.Entry(entity).Property(primaryKey).CurrentValue;

            var existingEntity = await _dbSet.FindAsync(primaryKeyValue);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}