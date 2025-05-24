using MediatR;
using Shared.DTOs;

public record UpdateProductCommand(Guid Id, string Name, decimal Price) : IRequest<ProductDto>;