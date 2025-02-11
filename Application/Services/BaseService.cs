using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interface;
using Mapster;

namespace Application.Services
{
    public  class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<T>().GetByIdAsync(id);
        }
        public virtual async Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            return await _unitOfWork.Repository<T>().GetByIdAsync(id, include);
        }



        public IQueryable<T> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            return _unitOfWork.Repository<T>().GetAllAsync(includes);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var response = await _unitOfWork.Repository<T>().AddAsync(entity);
            await _unitOfWork.SaveAsync();  // Commit the changes
            return response;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var response = await _unitOfWork.Repository<T>().UpdateAsync(entity);
            await _unitOfWork.SaveAsync();  // Commit the changes
            return response;
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<T>().DeleteAsync(id);
            await _unitOfWork.SaveAsync();  // Commit the changes
        }

        public IQueryable<T> GetByConditionAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return _unitOfWork.Repository<T>().GetByConditionAsync(predicate, includes);
        }

        public IQueryable<T> GetByConditionAsync(
    Expression<Func<T, bool>> predicate,
    Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _unitOfWork.Repository<T>().GetByConditionAsync(predicate);
            if (include != null)
            {
                query = include(query);
            }
            return query;
        }
    }



}
