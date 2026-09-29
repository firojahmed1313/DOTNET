using AiTutorial.Setup;
using AiTutorial.Service;
using AiTutorial.AiTools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAiServices(builder.Configuration);
builder.Services.AddScoped<IAiChat, AiChat>();
builder.Services.AddScoped<AiToolProvider>();
builder.Services.AddScoped<EmployeeTools>();
builder.Services.AddScoped<ProjectTools>();
builder.Services.AddScoped<OrderTools>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
