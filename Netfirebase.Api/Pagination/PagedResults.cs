﻿namespace Netfirebase.Api.Pagination;

public class PagedResults<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalNumberOfPages { get; set; }
    public int TotalNumberOfRecords { get; set; }
    public List<T> Results { get; set; } = new List<T>();

}
