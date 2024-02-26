
using Application.BackgroundServices.Invocables;
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
using Coravel;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ConfigurationApp
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

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


        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<IProcessRuleEngine, ProcessRuleEngine>();

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutoMapperConfig());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);


        services.AddScheduler();
        services.AddTransient<ProcessBulckInvocable>();

        services.AddScoped<IDynamicFormService, DynamicFormService>();
        services.AddScoped<IFormComponentRuleService, FormComponentRuleService>();

        return services;
    }
    public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        return services;
    }
}
