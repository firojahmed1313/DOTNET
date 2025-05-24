using MediatR;
using Shared.DTOs;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IProductRepository _repo;

    public UpdateProductCommandHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByIdAsync(request.Id);
        if (existing == null) return null;

        existing.Name = request.Name;
        existing.Price = request.Price;

        await _repo.UpdateAsync(existing);

        return new ProductDto { Id = existing.Id, Name = existing.Name, Price = existing.Price };
    }
}