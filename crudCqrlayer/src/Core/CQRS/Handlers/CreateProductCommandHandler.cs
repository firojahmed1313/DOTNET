using MediatR;
using Shared.DTOs;
using Core.Entities;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _repo;

    public CreateProductCommandHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product { Name = request.Name, Price = request.Price };
        await _repo.AddAsync(product);
        return new ProductDto { Id = product.Id, Name = product.Name, Price = product.Price };
    }
}