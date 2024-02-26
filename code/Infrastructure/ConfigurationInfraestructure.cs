using Application.Interfaces;
using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using ConnectureOS.Framework.Repository.Generics;
using Couchbase.Extensions.DependencyInjection;
using Domain.Entities.DynamicFormAggregate;
using Domain.Entities.ListAggregate;
using Domain.Entities.RulesAggregate;
using Infrastructure.CouchbaseDB.Helper;
using Infrastructure.CouchbaseDB.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure;

public static class ConfigurationInfrastructure
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped(typeof(IReadOnlyRepository<,,>), typeof(ReadOnlyRepository<,,>));
        services.AddScoped(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
        services.AddScoped<ITemplateComponentRepository, TemplateComponentRepository>();

        services.AddScoped<Application.Interfaces.Repositories.IDynamicFormRepository, Persistence.Repositories.DynamicFormRepository>();
        services.AddScoped<IDynamicFormPlanRepository, DynamicFormPlanRepository>();
        services.AddScoped<IDynamicFormComponentRuleRepository, DynamicFormComponentRuleRepository>();

        services.AddScoped<IDynamicFormTemplateRepository, DynamicFormTemplateRepository>();

        services.AddScoped<IDynamicFormItemRepository, DynamicFormItemRepository>();
        services.AddScoped<IBulkRepository, BulkRepository>();

        services.AddScoped<Application.Interfaces.Repositories.IGenericRepositoryAsync<DynamicFormItem>, GenericRepositoryAsync<DynamicFormItem>>();
        services.AddScoped<Application.Interfaces.Repositories.IGenericRepositoryAsync<ListDefinition>, GenericRepositoryAsync<ListDefinition>>();
        services.AddScoped<Application.Interfaces.Repositories.IGenericRepositoryAsync<ListTenantWorkflow>, GenericRepositoryAsync<ListTenantWorkflow>>();
        services.AddScoped<Application.Interfaces.Repositories.IGenericRepositoryAsync<ListValue>, GenericRepositoryAsync<ListValue>>();
        services.AddScoped<Application.Interfaces.Repositories.IGenericRepositoryAsync<DynamicFormTemplate>, GenericRepositoryAsync<DynamicFormTemplate>>();
        services.AddScoped<Application.Interfaces.Repositories.IGenericRepositoryAsync<RuleDynamic>, GenericRepositoryAsync<RuleDynamic>>();
        services.AddScoped<Application.Interfaces.Repositories.IGenericRepositoryAsync<RuleAction>, GenericRepositoryAsync<RuleAction>>();
        services.AddScoped<IRuleRepository, RuleRepository>();
        services.AddScoped<IRuleActionRepository, RuleActionRepository>();
        services.AddScoped<IActionParameterRepository, ActionParameterRepository>();
        services.AddScoped<IBulckComponentRepository, BulckComponentRepository>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddScoped<IListValueRepository, ListValueRepository>();


        services.AddScoped<ICacheRepository, CacheRepository>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddTransient<IDateTime, DateTimeService>();



        //couchdb
        services.AddCouchbase(configuration.GetSection("Couchbase"))

            .AddCouchbaseBucket<IRulesBucketProvider>("drx-cos-rules")
        .AddCouchbaseBucket<IDynamicFormBucketProvider>("drx-cos-workflow");




        services.AddSingleton<ICouchbaseService, CouchbaseService>();

        services.AddScoped<IDocRulesRepository, DocRulesRepository>();
        services.AddScoped<IDocDynamicFormRepository, DocDynamicFormRepository>();
        services.AddTransient<IDBHelper, DBHelper>();
        return services;
    }
    public static IServiceCollection SetupStorage(this IServiceCollection services, IConfiguration configuration, String App, String Region, String Enviroment)
    {
        var StrConnectionString = "Data Source=localhost;Initial Catalog=ConnectureDB;Integrated Security=False;";

        if (Enviroment == "localhost")
        {
            StrConnectionString = configuration["DatabaseSettings:ConnectionString"].Trim();
        }
        else
        {
            String secret = String.Format("{0}/{1}/DatabaseSettings__ConnectionString", Enviroment, App);
            var ConnectionDb = Infrastructure.AWSSecretManager.GetSecretAWS.GetSecret(secret, Region);
            if (ConnectionDb != null)
            {
                StrConnectionString = ConnectionDb.Result;
            }
        }
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(StrConnectionString,
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            options.EnableDetailedErrors(true);
            options.EnableSensitiveDataLogging(true);
        });
        services.AddScoped<ApplicationDbContextInitialiser>();
        return services;
    }
}
