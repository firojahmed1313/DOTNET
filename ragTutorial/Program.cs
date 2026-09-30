using RagMultiSource.Ingestion;
using ragTutorial.Service;
using ragTutorial.Setup;
using ragTutorial.Source;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddGeminiAiPipeline(builder.Configuration);
builder.Services.AddVectorDataStore();
builder.Services.AddSingleton<IVectorStoreService, VectorStoreService>();
builder.Services.AddSingleton<IDocumentSource, PdfDocumentSource>();
builder.Services.AddSingleton<TextChunker>();
builder.Services.AddSingleton<IngestionService>();
builder.Services.AddScoped<RagPipeline>();

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
