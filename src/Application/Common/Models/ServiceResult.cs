namespace Application.Common.Models;

/// <summary>
/// Class to create a paginatedr result
/// </summary>
/// <typeparam name="T">type of data in the list</typeparam>
public class PaginatedServiceResult<T> : ServiceResult
{
    public int PageNumber { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }

    public bool NextPage { get; set; }

    public bool PreviousPage { get; set; }

    public List<T> Data { get; set; }

    public PaginatedServiceResult()
    {
    }

    public PaginatedServiceResult(ServiceError error) : base(error)
    {
    }

    public PaginatedServiceResult(
        int pageNumber,
        int totalPages,
        int pageSize,
        int totalRecords,
        List<T> data)
    {
        PageNumber = totalRecords.Equals(0) ? 0 : pageNumber;
        TotalPages = totalPages;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        NextPage = (PageNumber + 1) < TotalPages;
        PreviousPage = (PageNumber - 1) > 0;
        Data = data;
    }
}

/// <summary>
/// Create a service result
/// </summary>
/// <typeparam name="T">Type of data</typeparam>
public class ServiceResult<T> : ServiceResult
{
    public T Data { get; set; }

    public ServiceResult(T data)
    {
        Data = data;
    }

    public ServiceResult(T data, string message)
    {
        Data = data;
        Message = message;
    }

    public ServiceResult(T data, ServiceError error) : base(error)
    {
        Data = data;
    }

    public ServiceResult(ServiceError error) : base(error)
    {
    }
}

/// <summary>
/// Base class of service result
/// </summary>
public class ServiceResult
{
    public bool Succeeded => Error == null;

    public ServiceError Error { get; set; }

    public string Message { get; set; }

    public ServiceResult()
    {
    }

    public ServiceResult(ServiceError error)
    {
        if (error == null)
        {
            error = ServiceError.DefaultError;
        }

        Error = error;
    }

    #region Helper Methods

    public static ServiceResult Failed(ServiceError error)
    {
        return new ServiceResult(error);
    }

    public static ServiceResult<T> Failed<T>(ServiceError error)
    {
        return new ServiceResult<T>(error);
    }

    public static ServiceResult<T> Failed<T>(T data, ServiceError error)
    {
        return new ServiceResult<T>(data, error);
    }

    public static ServiceResult<T> Success<T>(T data)
    {
        return new ServiceResult<T>(data);
    }

    #endregion
}
