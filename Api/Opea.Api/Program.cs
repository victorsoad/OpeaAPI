using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Opea.Api.Extensions;
using Opea.Api.Features;
using Opea.Application.Services;
using Opea.Domain.Interfaces;
using Opea.Infrastructure.Data;
using Opea.Infrastructure.Repositories;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        // Configura o FluentValidation para integração com ASP.NET Core
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();

        // Adiciona o MediatR e registra os handlers da camada de Application
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(ClienteProjectionSyncService).GetTypeInfo().Assembly);
        });

        // Adiciona o FluentValidation para validação automática dos requests
        builder.Services.AddValidatorsFromAssemblyContaining<CreateClienteRequestValidator>();

        // Adiciona o DbContext para SQLite
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Opea.Infrastructure")));

        // Adiciona o serviço de sincronização de projeção
        builder.Services.AddScoped<ClienteProjectionSyncService>();

        // Adiciona os repositórios para injeção de dependência
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Adiciona o middleware de logging de requisições
        app.UseRequestLogging();

        // Adiciona o middleware global de tratamento de exceções
        app.UseGlobalExceptionMiddleware();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}