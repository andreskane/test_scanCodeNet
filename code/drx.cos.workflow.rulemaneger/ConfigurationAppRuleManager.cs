using Application.Handlers.CommandHandlers.DynamicForm;
using Application.Handlers.CommandHandlers.DynamicFormPlanHandler;
using Application.Handlers.QueryHandlers;
using Application.Handlers.QueryHandlers.BulkProcess;
using Application.Handlers.QueryHandlers.DynamicFormItem;
using Application.Infrastructure.Mapper;
using Application.Interfaces.Services;
using Application.Services;
using Application.Services.Rules;
using Application.Services.Template;
using AutoMapper;
using ConnectureOS.Framework.Caching.MemCache;
using Couchbase.Extensions.Caching;
using FluentValidation;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;


namespace drx.cos.workflow.rulemanager
{
    public static class ConfigurationAppRuleManager
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });

            IMapper mapper = mapperConfig.CreateMapper();


            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            ///Handler DI
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateDynamicFormCommandHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateDynamicFormPlanCommandHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(UpdateDynamicFormCommandHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DeleteDynamicFormCommandHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetDynamicFormByIdQueryHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetDynamicFormByFiltersQueryHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetLastPublishedDynamicFormItemQueryHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetDynamicFormComponentByFiltersQueryHandler).GetTypeInfo().Assembly));



            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllBulkProcessQueryHandler).GetTypeInfo().Assembly));
            services.AddSingleton<IMediator, Mediator>();







            var dbContext = services.BuildServiceProvider().GetService<ApplicationDbContext>();


            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IListaDataService, ListaDataService>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IProcessRuleEngine, ProcessRuleEngine>();


            services.AddSingleton(mapper);



            return services;




        }
        public static IServiceCollection AddCacheCouchDB(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddDistributedMemoryCache();
            services.AddDistributedCouchbaseCache(
                configuration["Caching:bucket"].Trim()
                , opt => { });


            return services;

        }
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            var children = configuration.GetSection("Caching").GetChildren();
            var cachingConfiguration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            ICacheStore cacheStore = new MemoryCacheStore(memoryCache, cachingConfiguration);

            services.AddSingleton(cacheStore);
            return services;
        }
    }
}
