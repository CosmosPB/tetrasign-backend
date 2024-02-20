using TetraSign.Core.Application.Configuration;
using TetraSign.Core.Helpers;

namespace TetraSign.WebApi.Routers;

public static class ConfigurationEndpoint
{
    public static RouteGroupBuilder MapConfigurationApi(this RouteGroupBuilder group)
    {
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

        return group;
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
        return TypedResults.Ok(new_configuration);
    }
}