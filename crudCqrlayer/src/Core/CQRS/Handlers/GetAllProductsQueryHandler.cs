using MediatR;
using Shared.DTOs;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResponse<ProductDto>>
{
    private readonly IProductRepository _repo;

    public GetAllProductsQueryHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<PagedResponse<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var allProducts = await _repo.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            allProducts = allProducts
                .Where(p => p.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        var totalCount = allProducts.Count;
        var skip = (request.PageNumber - 1) * request.PageSize;
        var pagedItems = allProducts
            .Skip(skip)
            .Take(request.PageSize)
            .Select(p => new ProductDto { Id = p.Id, Name = p.Name, Price = p.Price })
            .ToList();

        return new PagedResponse<ProductDto>(pagedItems, totalCount, request.PageNumber, request.PageSize);
    }
}