using TetraSign.Core.Application.Configuration;
using TetraSign.Core.Helpers;

namespace TetraSign.WebApi.Routers;

public static class ConfigurationEndpoint
{
    public static RouteGroupBuilder MapConfigurationApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetConfigurations)
            .Produces<List<ConfigurationDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetConfigurations")
            .WithSummary("An endpoint to get all configurations")
            .WithDescription("An endpoint to get all configuratios")
            .WithOpenApi();

        group.MapGet("/{id}", GetConfiguration)
            .Produces<ConfigurationDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("GetConfiguration")
            .WithSummary("An endpoint to get a configuration")
            .WithDescription("An endpoint to get a configuration")
            .WithOpenApi();

        group.MapPost("/", PostConfiguration)
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError)
            .Accepts<ConfigurationDTO>("application/json")
            .WithName("PostConfiguration")
            .WithSummary("An endpoint to create a configuration")
            .WithDescription("An endpoint to create a configuration")
            .WithOpenApi();

        group.MapPost("/{id}", PutConfiguration)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError)
            .Accepts<ConfigurationDTO>("application/json")
            .WithName("PutConfiguration")
            .WithSummary("An endpoint to update a configuration")
            .WithDescription("An endpoint to update a configuration")
            .WithOpenApi();

        return group;
    }

    static async Task<IResult> GetConfigurations(IConfigurationService configuration_service)
    {
        // logger.LogInformation("{userId} - MSProducts.ProductsEndpoints.GetAllProducts", userId);
        IEnumerable<ConfigurationDTO> configurations = await configuration_service.Find();
        return TypedResults.Ok(configurations);
    }

    static async Task<IResult> GetConfiguration(string id, IConfigurationService configuration_service)
    {
        // logger.LogInformation("{userId} - MSProducts.ProductsEndpoints.GetAllProducts", userId);
        ConfigurationDTO configuration = await configuration_service.FindById(id);
        return TypedResults.Ok(configuration);
    }

    static async Task<IResult> PostConfiguration(ConfigurationDTO configuration, IConfigurationService configuration_service)
    {
        // logger.LogInformation("{userId} - MSProducts.ProductsEndpoints.GetAllProducts", userId);
        ConfigurationDTO new_configuration = await configuration_service.Add(configuration);
        return TypedResults.Created(new_configuration.id);
    }

    static async Task<IResult> PutConfiguration(ConfigurationDTO configuration, IConfigurationService configuration_service)
    {
        // logger.LogInformation("{userId} - MSProducts.ProductsEndpoints.GetAllProducts", userId);
        await configuration_service.Update(configuration);
        return TypedResults.NoContent();
    }
}