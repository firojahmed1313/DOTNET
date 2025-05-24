using MediatR;
using Shared.DTOs;

public record CreateProductCommand(string Name, decimal Price) : IRequest<ProductDto>;