using MediatR;
using Shared.DTOs;
using Core.Entities;
using FluentValidation;

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

    //public class Validator : AbstractValidator<CreateProductCommand>
    //{
    //    public Validator()
    //    {
    //        RuleFor(x => x.Name)
    //            .NotEmpty().WithMessage("Product name is required")
    //            .MinimumLength(3).WithMessage("Product name must be at least 3 characters");

    //        RuleFor(x => x.Price)
    //            .GreaterThan(0).WithMessage("Price must be greater than zero");
    //    }
    //}
}