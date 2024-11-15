using LearnLogic.Application.Commands;
using LearnLogic.Application.Services;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.Interfaces.Application;
using LearnLogic.Domain.Interfaces.Repository;
using LearnLogic.Domain.ViewModels;
using LearnLogic.Infra.CrossCutting.Bus;
using LearnLogic.Infra.CrossCutting.IoC.AutoMapper;
using LearnLogic.Infra.Data.Context;
using LearnLogic.Infra.Data.Repositories;
using LearnLogic.Infra.Data.UoW;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using System;

namespace LearnLogic.Infra.CrossCutting.IoC
{
    public static class BootStrapper 
    {

        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDatabase(services, configuration);
            ConfigureAutoMapper(services);
            ConfigureServices(services);
            ConfigureRepositories(services);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Mediator));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IUserClaimsAccessor, UserClaimsAccessor>();
            services.AddScoped<IHomeApplication, HomeApplication>();

            RegisterApplicationServices(services);
            RegisterCommandHandlers(services);
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IDataUnitOfWork, DataUnitOfWork>();
            services.AddScoped<IDataContextSolution, DataContextSolution>();
            RegisterRepositoryImplementations(services);
        }

        private static void RegisterRepositoryImplementations(IServiceCollection services)
        {
            var baseRepositoryInterface = typeof(IBaseRepository<BaseDTO>).Name;
            var items = Assembly.GetAssembly(typeof(UserRepository)).DefinedTypes.Where(x => x.IsClass && !x.IsAbstract && !x.IsNested && x.ImplementedInterfaces.Any(y => y.Name == baseRepositoryInterface)).ToList();
            foreach (var item in items)
                services.AddScoped(item.GetInterfaces().First(), item.AsType());
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            var baseApplicationInterface = typeof(IBaseApplication<BaseViewModel, BaseDTO>).Name;
            var items = Assembly.GetAssembly(typeof(UserApplication)).DefinedTypes.Where(x => x.IsClass && !x.IsAbstract && !x.IsNested && x.ImplementedInterfaces.Any(y => y.Name == baseApplicationInterface)).ToList();
            foreach (var item in items)
                services.AddScoped(item.GetInterfaces().First(), item.AsType());
        }

        private static void RegisterCommandHandlers(IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(UserCommandHandler));

            var handlerTypes = assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))).ToList();

            foreach (var type in handlerTypes)
            {
                var interfaces = type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)).ToList();

                foreach (var item in interfaces)
                {
                    services.AddScoped(item, type);
                }
            }
        }

        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddDbContext<DataContextSolution>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                       .EnableSensitiveDataLogging()
                       .LogTo(Console.WriteLine, LogLevel.Information));
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
