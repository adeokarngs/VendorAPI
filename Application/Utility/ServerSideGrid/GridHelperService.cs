using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Utility.ServerSideGrid
{
    public class GridHelperService
    {

        public static async Task<List<T>> GetPagedList<T>(IQueryable<T> list, GridRequest request)
        {
            return  await ApplyDynamicSorting(list, request.sortColumn, request.sortOrder).Skip(request.page * request.pageSize).Take(request.pageSize).ToListAsync();
        }

        public static async Task<List<T>> GetPagedList<T>(IQueryable<T> list, GridRequest<T> request)
        {
            return await ApplyDynamicSorting(list, request.sortColumn, request.sortOrder).Skip(request.page * request.pageSize).Take(request.pageSize).ToListAsync();
        }
        public static IQueryable<T> ApplyDynamicSorting<T>(IQueryable<T> query, string? oSortColumn, string oSortOrder = "desc")
        {
            var sortColumn = string.IsNullOrEmpty(oSortColumn) ? "createdDate" : oSortColumn;
            var sortOrder = string.IsNullOrEmpty(oSortOrder) ? "desc" : oSortOrder;

            if (string.IsNullOrEmpty(sortColumn))
            {
                // Default sorting if no column is provided
                return sortOrder.ToLower() == "asc"
                    ? query.OrderBy(e => EF.Property<object>(e, "CreatedDate"))
                    : query.OrderByDescending(e => EF.Property<object>(e, "CreatedDate"));
            }

            // Split the sortColumn into nested properties (e.g., "role.vendor.address" -> ["role", "vendor", "address"])
            var propertyNames = sortColumn.Split('.');

            // Create the initial parameter (representing the entity itself)
            var parameter = Expression.Parameter(typeof(T), "e");

            // Traverse the property path and build the nested property expression dynamically
            Expression propertyExpression = parameter;
            foreach (var propertyName in propertyNames)
            {
                // Access the next level of property (handling nested properties)
                propertyExpression = Expression.PropertyOrField(propertyExpression, propertyName);
            }

            // Create the lambda expression for sorting (e.g., x => x.role.vendor.address)
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(propertyExpression, typeof(object)), parameter);

            // Apply sorting based on the direction (ascending or descending)
            var methodName = sortOrder.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { typeof(T), typeof(object) },
                query.Expression,
                Expression.Quote(lambda)
            );

            return query.Provider.CreateQuery<T>(resultExpression);
        }


    }
}
