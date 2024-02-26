using Amazon.SecretsManager;
using Application;
using Autofac.Extensions.DependencyInjection;
using Coravel;
using Couchbase.Extensions.DependencyInjection;
using Domain.Configs;
using drx.cos.workflow.rulemanager;
using drx.cos.workflow.rulemanager.Services.Rules;
using drx.cos.workflow.rulemanager.Services.Rules.HelperFunctions;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen(c =>
 {
 });

builder.Configuration
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
              .AddUserSecrets<Program>()
              .AddEnvironmentVariables();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));


// Add services to the container.
builder.Services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);




var env = Environment.GetEnvironmentVariable("KubernetesEnv");
if (env == null) env = "localhost";
var appName = "drx-cos-api-workflow";

builder.Services.AddApplicationServices();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonSecretsManager>();

builder.Services.AddOptions<DatabaseSettings>().BindConfiguration(nameof(DatabaseSettings));
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.SetupStorage(builder.Configuration, appName, "us-east-2", env);


builder.Services.AddCache(builder.Configuration);
builder.Services.AddMvc();

builder.Services.AddSwaggerGen(c =>
 {

 });
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); ;
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));



builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Information); // Configura el nivel de log según tus necesidades
});


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ValidateRuleHandler).GetTypeInfo().Assembly));




Common.InitializeListaDataServiceLambda(builder.Services);





var app = builder.Build();










app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");




app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swagger, httpReq) =>
    {
        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"https://{httpReq.Host.Value}" } };
    });
});

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));



app.Run();
app.Lifetime.ApplicationStopped.Register(() =>
{
    app.Services.GetRequiredService<ICouchbaseLifetimeService>().CloseAsync().ConfigureAwait(false);
});