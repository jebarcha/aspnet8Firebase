
using Netfirebase.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Netfirebase.Api.Vms;

namespace Netfirebase.Api.Pagination;

public class PagedList : IPagedList
{
    private readonly IMapper _mapper;

    public PagedList(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<PagedResults<TResult>> CreatePagedEntryAndGenericResults<T, TResult>(
        IQueryable<T> queryable, 
        int page, int pageSize, 
        string orderBy, 
        bool ascending)
    {

        var skipAmount = pageSize * (page - 1);
        var totalNumberOfRecords = await queryable.CountAsync();

        var records = new List<T>();

        if (orderBy is null)
        {
            records = await queryable.Skip(skipAmount).Take(pageSize).ToListAsync();
        }
        else
        {
            records = await queryable
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(pageSize)
                .ToListAsync();
        }

        var results = _mapper.Map<List<TResult>>(records);

        var mod = totalNumberOfRecords % pageSize;
        var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

        return new PagedResults<TResult>
        {
            Results = results,
            PageNumber = page,
            PageSize = pageSize,
            TotalNumberOfPages = totalPageCount,
            TotalNumberOfRecords = totalNumberOfRecords
        };

    }

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
