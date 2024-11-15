using LearnLogic.Infra.CrossCutting.APIConfiguration.Swagger;
using LearnLogic.Infra.CrossCutting.APIConfiguration.Controllers;
using LearnLogic.Infra.CrossCutting.APIConfiguration.Jwt;
using LearnLogic.Infra.CrossCutting.Extensions;
using LearnLogic.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configure app settings
ConfigureAppSettings(builder);

// Register services
ConfigureServices(builder);

// Build the app
var app = builder.Build();

// Configure the app
ConfigureApp(app);

app.Run();

void ConfigureAppSettings(WebApplicationBuilder builder)
{
    builder.Configuration
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
}

void ConfigureServices(WebApplicationBuilder builder)
{
    BootStrapper.RegisterServices(builder.Services, builder.Configuration);

    builder.Services.AddControllersWithViews(options =>
    {
        options.Conventions.Add(new LowercaseControllerModelConvention());
    });

    builder.Services.ConfigureJwt(builder.Configuration);
    builder.Services.ConfigureServices();
    builder.Services.AddSwaggerServices();

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
}

void ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerSetup();
    }

    app.ConfigureDefaultServerSettings();
    app.ConfigureDefaultAuthSettings();
    app.MiddlewareConfig();
    app.MapControllers();
}
