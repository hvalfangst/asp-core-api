using HvalfangstApi.configuration;
using HvalfangstApi.middleware;
using HvalfangstApi.repository;
using HvalfangstApi.service;
using HvalfangstApi.util.logging;

namespace HvalfangstApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddScoped<DataContext>();
        builder.Services.AddControllers();
        builder.Services.AddScoped<IHeroRepository, HeroRepository>();
        builder.Services.AddScoped<HeroService>();
        builder.Services.AddSingleton<ILoggerFactory, ApiLoggerFactory>();
        
        var app = builder.Build();
        app.MapControllers();
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.Run();
    }
}