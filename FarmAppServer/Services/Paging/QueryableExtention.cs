using System;
using System.Linq;

namespace FarmAppServer.Services.Paging
{
    public static class QueryableExtention
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double) result.RowCount / pageSize;
            result.PageCount = (int) Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Data = query.Skip(skip).Take(pageSize);

            return result;
        }
    }
}