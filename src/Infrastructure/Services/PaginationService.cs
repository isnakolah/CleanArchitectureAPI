using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class PaginationService : IPaginate
{
    private readonly IUriService _uriService;
    private readonly IMapper _mapper;

    public PaginationService(IUriService uriService, IMapper mapper)
    {
        _uriService = uriService;
        _mapper = mapper;
    }

    public async Task<PaginatedServiceResult<Out>> CreateAsync<In, Out>(
        IQueryable<In> source,
        PaginationFilter filter,
        CancellationToken cancellationToken,
        List<QueryParam> queryParams)
        where In : class
        where Out : class
    {
        var data = await GetData<In, Out>(source, filter, cancellationToken);

        var totalRecords = await source.CountAsync(cancellationToken);

        var pageSize = filter.PageSize;

        var pageNumber = filter.PageNumber;

        var totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / pageSize));


        var response = new PaginatedServiceResult<Out>(
            pageNumber, totalPages, pageSize, totalRecords, data);

        return response;
    }

    public Task<PaginatedServiceResult<Out>> CreateAsync<In, Out>(
        IQueryable<In> source,
        PaginationFilter filter,
        CancellationToken cancellationToken,
        QueryParam queryParam = null)
        where In : class
        where Out : class
    {
        var queryParams = queryParam is not null
            ? new List<QueryParam>
            {
                    queryParam
            }
            : new List<QueryParam>();

        return CreateAsync<In, Out>(source, filter, cancellationToken, queryParams);
    }

    private async Task<List<Out>> GetData<In, Out>(IQueryable<In> source, PaginationFilter filter, CancellationToken cancellationToken)
        where In : class
        where Out : class
    {
        return await source
            .AsNoTracking()
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ProjectTo<Out>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
