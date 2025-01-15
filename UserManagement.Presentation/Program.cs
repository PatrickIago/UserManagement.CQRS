using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application;
using UserManagement.Application.Interfaces;
using UserManagement.Infra.Data;
using UserManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Initializer).Assembly));

builder.Services.AddScoped<IUserRepository, UserRepository>();

// Swagger/OpenAPI configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "User Management",
        Version = "1.0",
        Description = "API do sistema UserManagement para gerenciamento de Usuários.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Patrick",
            Email = "Mendespatrick720@gmail.com"
        }
    });
});

Initializer.Initialize(builder.Services);

// Database context configuration
builder.Services.AddDbContext<UserManagementDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
