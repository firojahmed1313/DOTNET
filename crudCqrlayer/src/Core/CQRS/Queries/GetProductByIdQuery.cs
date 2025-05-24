using MediatR;
using Shared.DTOs;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;