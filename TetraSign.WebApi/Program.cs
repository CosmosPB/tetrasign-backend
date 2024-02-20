// namespace TetraSign.WebAp;

// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.Identity.Web;
// using Microsoft.Identity.Web.Resource;

using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using TetraSign.Core.Application;
using TetraSign.Core.Application.Configuration;
using TetraSign.Core.Domain.Configuration;
using TetraSign.Core.Helpers;
using TetraSign.Core.Infraestructure;
using TetraSign.WebApi.Routers;

string API_V1_CONFIGURATION = "/api/v1/configuration";
string DOCS_V1 = "docs v1";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<TetraSignDatabaseSettings>(builder.Configuration.GetSection("TetraSignDatabase"));
// builder.Services.AddSingleton<IDatabaseSettings, TetraSignDatabaseSettings>();
builder.Services.AddSingleton<IRepository<Configuration, TetraSignDatabaseSettings>, MongoRepository<Configuration, TetraSignDatabaseSettings>>();
builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new Map()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => {
    OpenApiInfo info = new() {
        Version = "V1",
        Title = "TetraSign",
        Description = "TetraSign - pending",
        TermsOfService = new Uri("https://www.cosmospb.com"),
        Contact = new OpenApiContact() {
            Name = "Cosmos PB Support",
            Email = "support@cosmospb.com"
        }
    };

    x.SwaggerDoc(DOCS_V1, info);
});
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// Add services to the container.
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));
// builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

var isSwaggerEnabledFromConfig = bool.TrueString.Equals(builder.Configuration["EnableSwagger"] ?? "", StringComparison.OrdinalIgnoreCase);
if (app.Environment.IsDevelopment() || isSwaggerEnabledFromConfig)
{
    app.UseSwagger();
    app.UseSwaggerUI(x => {
        x.EnableTryItOutByDefault();
        x.SwaggerEndpoint("/swagger/docs v1/swagger.json", DOCS_V1);
    });
}

app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseRouting();

app.MapGroup(API_V1_CONFIGURATION).MapConfigurationApi()
    .WithGroupName(DOCS_V1)
    .WithTags("Products");

// var scopeRequiredByApi = app.Configuration["AzureAd:Scopes"] ?? "";
// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", (HttpContext httpContext) =>
// {
//     // httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();
// // .RequireAuthorization();

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
