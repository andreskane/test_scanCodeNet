
using Amazon.SecretsManager;
using ApiOS;
using Application;
using Application.BackgroundServices.Invocables;
using Application.Infrastructure.Mapper;
using Application.Services.Rules.HelperFunctions;
using Autofac.Extensions.DependencyInjection;
using Coravel;
using Couchbase.Extensions.DependencyInjection;
using Domain.Configs;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var env = Environment.GetEnvironmentVariable("KubernetesEnv");
if (env == null) env = "localhost";
var appName = builder.Environment.ApplicationName;

builder.Services.AddApplicationServices();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonSecretsManager>();

builder.Services.AddOptions<DatabaseSettings>().BindConfiguration(nameof(DatabaseSettings));


builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.SetupStorage(builder.Configuration, appName, "us-east-2", env);
builder.Services.AddWebUIServices();
builder.Services.AddCache(builder.Configuration);
builder.Services.AddMvc();

builder.Services.AddSwagger(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));



builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Information); // Configura el nivel de log según tus necesidades
});

Common.InitializeListaDataService(builder.Services);

var app = builder.Build();




app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<ProcessBulckInvocable>()
    // .EveryFifteenSeconds()    
    .EveryMinute()
    ;
});



using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

    string? aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    if (!string.IsNullOrEmpty(aspNetCoreEnvironment))
    {
        if (aspNetCoreEnvironment.ToUpper() != "SWAGGER")
        {
            await initialiser.InitialiseAsync();
        }
    }
    else
    {
        await initialiser.InitialiseAsync();
    }


}

app.UseHttpsRedirection();
app.UseHsts();
app.UseCors(x => x.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
           );

app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swagger, httpReq) =>
    {
        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"https://{httpReq.Host.Value}" } };
    });
});

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));


app.UseDeveloperExceptionPage();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapControllers();
app.Run();
app.Lifetime.ApplicationStopped.Register(() =>
{
    app.Services.GetRequiredService<ICouchbaseLifetimeService>().CloseAsync().ConfigureAwait(false);
});