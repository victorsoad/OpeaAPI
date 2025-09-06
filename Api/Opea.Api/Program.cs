using FluentValidation;
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

        // Adiciona o MediatR e registra os handlers da camada de Application
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(ClienteProjectionSyncService).GetTypeInfo().Assembly);
        });

        // Adiciona o FluentValidation para valida��o autom�tica dos requests
        builder.Services.AddValidatorsFromAssemblyContaining<CreateClienteRequestValidator>();

        // Adiciona o DbContext para SQLite
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Opea.Infrastructure")));

        // Adiciona o servi�o de sincroniza��o de proje��o
        builder.Services.AddScoped<ClienteProjectionSyncService>();

        // Adiciona os reposit�rios para inje��o de depend�ncia
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