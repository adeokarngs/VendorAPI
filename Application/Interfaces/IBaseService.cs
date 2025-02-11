using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>> include = null);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);

        IQueryable<T> GetByConditionAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetByConditionAsync(
    Expression<Func<T, bool>> predicate,
    Func<IQueryable<T>, IQueryable<T>> include = null);
    }

}
