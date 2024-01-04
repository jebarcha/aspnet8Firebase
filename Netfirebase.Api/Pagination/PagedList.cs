
using Netfirebase.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Netfirebase.Api.Pagination;

public class PagedList : IPagedList
{
    public async Task<PagedResults<T>> CreatePagedGenericResults<T>(
        IQueryable<T> queryable, 
        int page, 
        int pageSize, 
        string orderBy, 
        bool ascending
    )
    {
        var skipAmount = pageSize * (page - 1);
        var totalNumberOfRecords = await queryable.CountAsync();

        var results = new List<T>();

        if (orderBy is null)
        {
            results = await queryable
                .Skip(skipAmount)
                .Take(pageSize)
                .ToListAsync();
        }
        else
        {
            results = await queryable
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(pageSize)
                .ToListAsync();             
        }

        var mod = totalNumberOfRecords % pageSize;
        var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

        return new PagedResults<T>
        {
            Results = results,
            PageNumber = page,
            PageSize = pageSize,
            TotalNumberOfPages = totalPageCount,
            TotalNumberOfRecords = totalNumberOfRecords
        };

    }
}
