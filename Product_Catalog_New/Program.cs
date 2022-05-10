using Microsoft.EntityFrameworkCore;
using Product_Catalog_New;
using Product_Catalog_New.Models;
using Product_Catalog_New.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IBlobService, BlobService>();
builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IAppConfig, AppConfig>();
builder.Services.AddTransient<IAppConfig, AppConfig>();
builder.Services.AddDbContext<ProductContext>(opt =>
    opt.UseInMemoryDatabase("Product"));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();