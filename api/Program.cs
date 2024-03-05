using Hvalfangst.api.configuration;
using Hvalfangst.api.middleware;
using Hvalfangst.api.repository;
using Hvalfangst.api.service;
using Hvalfangst.api.util.logging;

namespace Hvalfangst.api;

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