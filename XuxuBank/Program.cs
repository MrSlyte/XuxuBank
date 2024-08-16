using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;
using XuxuBank.Configuration;
using XuxuBank.Controller;
using XuxuBank.Domain.Config;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").AddJsonFile("appsettings.Development.json", true).AddEnvironmentVariables();
builder.Services.Configure<SystemConfiguration>(builder.Configuration);
builder.Services.ConfigureDependencyInjection();
builder.Services.Configure<JsonSerializerSettings>(options =>
{
    options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.Converters.Add(
        new StringEnumConverter
        {
            NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy(),
        });
});
ServicePointManager.DefaultConnectionLimit = 500;
var app = builder.Build();

app.Services.GetRequiredService<ClientsController>().Map(app);

await app.RunAsync();