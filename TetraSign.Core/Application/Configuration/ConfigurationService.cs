using AutoMapper;
using TetraSign.Core.Helpers;
using TetraSign.Core.Infraestructure;
using DomainConfiguration = TetraSign.Core.Domain.Configuration;

namespace TetraSign.Core.Application.Configuration;

public class ConfigurationService: IConfigurationService {


    private readonly IRepository<DomainConfiguration.Configuration, TetraSignDatabaseSettings> configuration_repository;
    private readonly IMapper mapper;

    public ConfigurationService(
        IRepository<DomainConfiguration.Configuration, TetraSignDatabaseSettings> configuration_repository,
        IMapper mapper
    ) {
        this.configuration_repository = configuration_repository;
        this.mapper = mapper;
    }

    public async Task<ConfigurationDTO> FindById(string id) {

        DomainConfiguration.Configuration configuration = await configuration_repository.FindById(id) 
                                                            ?? throw new Exception("Configuration not found");

        return mapper.Map<ConfigurationDTO>(configuration);
    }

    public async Task<ConfigurationDTO> Add(ConfigurationDTO configuration) {

        IEnumerable<DomainConfiguration.Configuration> configurations = await configuration_repository.Find();

        if (configurations.Count() > 1) throw new Exception("You only can add one configuration");

        DomainConfiguration.ConfigurationSunatAuthentication new_configuration_sunat_authentication = DomainConfiguration.ConfigurationSunatAuthentication.Create(
            configuration.configuration_sunat_authentication.grant_type,
            configuration.configuration_sunat_authentication.scope,
            configuration.configuration_sunat_authentication.client_id,
            configuration.configuration_sunat_authentication.client_secret,
            configuration.configuration_sunat_authentication.username,
            configuration.configuration_sunat_authentication.password
        );

        DomainConfiguration.ConfigurationSunatEndpoints new_configuration_sunat_endpoints = DomainConfiguration.ConfigurationSunatEndpoints.Create(
            configuration.configuration_sunat_endpoints.despatch_advice_url
        );

        DomainConfiguration.ConfigurationPaths new_configuration_paths = DomainConfiguration.ConfigurationPaths.Create(
            configuration.configuration_paths.certificate,
            configuration.configuration_paths.certificate_password,
            configuration.configuration_paths.input,
            configuration.configuration_paths.output,
            configuration.configuration_paths.despatch_advice_template
        );

        DomainConfiguration.Configuration new_configuration = DomainConfiguration.Configuration.Create(
            configuration.party_identification,
            configuration.party_name,
            configuration.registration_name,
            configuration.address_type_code,
            configuration.city_subdivision_name,
            configuration.city_name,
            configuration.country_subentity,
            configuration.country_subentity_code,
            configuration.district,
            configuration.address_line,
            configuration.identification_code,
            new_configuration_sunat_authentication,
            new_configuration_sunat_endpoints,
            new_configuration_paths
        );

        // await configuration_repository.Add(new_configuration);

        return mapper.Map<DomainConfiguration.Configuration, ConfigurationDTO>(new_configuration);
    }
}