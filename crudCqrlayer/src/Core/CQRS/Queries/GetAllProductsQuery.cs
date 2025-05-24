using MediatR;
using Shared.DTOs;

public record GetAllProductsQuery(int PageNumber = 1, int PageSize = 10, string? SearchTerm = null) : IRequest<PagedResponse<ProductDto>>;