using ConnectureOS.Framework.AWS.APIGateway;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO.Compression;
using System.Reflection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        services.AddControllersWithViews();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);



        services.AddResponseCompression(options =>
        {
            IEnumerable<string> MimeTypes = new[]
            {
                     "text/plain",
                     "application/json",
               };
            options.EnableForHttps = true;
            options.MimeTypes = MimeTypes;
            options.Providers.Add<GzipCompressionProvider>();
        });
        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });
        services.Configure<FormOptions>(options =>
        {
            options.ValueLengthLimit = int.MaxValue;
            options.MultipartBodyLengthLimit = long.MaxValue; // <-- ! long.MaxValue
            options.MultipartBoundaryLengthLimit = int.MaxValue;
            options.MultipartHeadersCountLimit = int.MaxValue;
            options.MultipartHeadersLengthLimit = int.MaxValue;
        });
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var integrationSettings = new ApiGatewayIntegrationSettings();

        integrationSettings.AccessControlSettings().AllowOrigenWtihValue("*");


        var strBuildVersion = "#{BuildVersion}#";
        var strVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        
        string stra8kEnviroment = "<b>sin configuracion</b>";
        var strDescription = "";
        if (configuration["AWS:KubernetesEnv"].Trim() != null)
        {
            stra8kEnviroment = configuration["AWS:KubernetesEnv"].Trim();
        }

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = string.Format("{0}", strVersion),
                Title = "API ConnectureOS Workflow",
                Description = string.Format(
                "{0} Version: {1} | Build Version:{2} | Kubernetes Enviroment:{3}",
                strDescription,
                strVersion,
                strBuildVersion,
                stra8kEnviroment)


            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the bearer scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            c.CustomOperationIds(apiDesc =>
            {
                return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
            });
            ApiGatewayIntegrationFilter.IntegrationSettings = integrationSettings;

            c.OperationFilter<ApiGatewayIntegrationFilter>();

        });
        return services;
    }



}
