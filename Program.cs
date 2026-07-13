using Microsoft.EntityFrameworkCore;
using OrnivaApi.Data;
using OrnivaApi.Mapping;
using OrnivaApi.Repositories;
using OrnivaApi.Repositories.Interfaces;
using OrnivaApi.Services;
using OrnivaApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Registering
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DB_CONNECTION_STRING");
builder.Services.AddDbContext<OrnivaDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
