namespace Netfirebase.Api.Pagination;

public interface IPagedList
{
    Task<PagedResults<T>> CreatePagedGenericResults<T>(
        IQueryable<T> queryable,
        int page,
        int pageSize,
        string orderBy,
        bool ascending);

    Task<PagedResults<TResult>> CreatePagedEntryAndGenericResults<T, TResult>(
       IQueryable<T> queryable,
       int page,
       int pageSize,
       string orderBy,
       bool ascending);

}
