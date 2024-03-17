using OnlineBookstoreAPI.Domain.Models.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Domain.Helpers
{
    public static class ExtensionMethods
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string orderByMember, SortOrders sortingOrder) where T : class
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);
            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                sortingOrder == SortOrders.Asc ? "OrderBy" : "OrderByDescending",
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector)
                );
            return query.Provider.CreateQuery<T>(orderBy);

        }
    
        public static PagedList<T> Paging<T>(this IQueryable<T> query,int pageSize = 20, int pageNumber = 1) where T : class
        {
            var count = query.Count();
            var items = pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList() : query.ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
        public static PagedList<T> ApplyFilterSortingPagination<T>(this IQueryable<T> query, FilterAndPaginationModel filterAndPaginationModel) where T : class
        {
            if (filterAndPaginationModel.FilterUtility != null)
            {
                query = CompositeFilter<T>.ApplyFilter(query, filterAndPaginationModel.FilterUtility);
            }
            if (filterAndPaginationModel.SortingUtility != null)
            {
                foreach (var sortingParam in filterAndPaginationModel.SortingUtility.SortingParams!.Where(x => !String.IsNullOrEmpty(x.ColumnName)))
                {
                    query = query.OrderByDynamic<T>(sortingParam.ColumnName, sortingParam.SortOrder);
                }
            }
            var finalResult = query!.Paging(filterAndPaginationModel.PageSize, filterAndPaginationModel.PageNumber);
            return finalResult;
        }
    }
}
