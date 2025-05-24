using FluentValidation;
using FluentValidation.AspNetCore;
using Core.Validators;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;



var builder = WebApplication.CreateBuilder(args);

// Configuration from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DConn"),
        b => b.MigrationsAssembly("Api")));

builder.Services.AddMediatR(typeof(CreateProductCommandHandler).Assembly);

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Optional: FluentValidation setup
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
