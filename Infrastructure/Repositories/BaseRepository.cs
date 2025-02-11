using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Interface;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            IQueryable<T> query = _context.Set<T>();

            // Get all navigation properties dynamically
            var navigationProperties = _context.Model
                .FindEntityType(typeof(T))?
                .GetNavigations()
                .Select(n => n.Name)
                .ToList();

            if (navigationProperties != null)
            {
                foreach (var navigationProperty in navigationProperties)
                {
                    query = query.Include(navigationProperty);
                }
            }

            // Retrieve the entity by its ID
            return await query.AsNoTracking().Where(e => EF.Property<int>(e, "Id") == id).FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            // Apply dynamic includes if provided
            if (includes != null)
            {
                query = includes(query);
            }

            // Retrieve the entity by its ID with optional includes
            return await query.Where(e => EF.Property<int>(e, "Id") == id).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            // Apply dynamic Includes
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query; // Return IQueryable instead of executing ToListAsync()
        }



        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = Activator.CreateInstance<T>();
            _context.Entry(entity).Property("Id").CurrentValue = id;
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetByConditionAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            // Apply dynamic Includes if provided
            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }
    }
}
