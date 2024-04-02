using AutoMapper;
using TetraSign.Core.Helpers;
using TetraSign.Core.Helpers.Database;
using TetraSign.Core.Infraestructure;
using DomainConfiguration = TetraSign.Core.Domain.Configuration;

namespace TetraSign.Core.Application.Configuration;

public class ConfigurationService: IConfigurationService {


    private readonly IRepository<DomainConfiguration.Configuration, ConfigurationDBSettings> configuration_repository;
    private readonly IMapper mapper;

    public ConfigurationService(
        IRepository<DomainConfiguration.Configuration, ConfigurationDBSettings> configuration_repository,
        IMapper mapper
    ) {
        this.configuration_repository = configuration_repository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<ConfigurationDTO>> Find() {

        IEnumerable<DomainConfiguration.Configuration> configuration = await configuration_repository.Find();

        return mapper.Map<IEnumerable<ConfigurationDTO>>(configuration);
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

        await configuration_repository.Add(new_configuration);

        return mapper.Map<DomainConfiguration.Configuration, ConfigurationDTO>(new_configuration);
    }

    public async Task Update(ConfigurationDTO configuration) {

        if (string.IsNullOrEmpty(configuration.id))
                throw new Exception("Id can't be empty");

        DomainConfiguration.Configuration _configuration = await configuration_repository.FindById(configuration.id) ?? throw new Exception("No such configuration exists");
        DomainConfiguration.ConfigurationSunatAuthentication configuration_sunat_authentication;
        DomainConfiguration.ConfigurationSunatEndpoints configuration_sunat_endpoints;
        DomainConfiguration.ConfigurationPaths configuration_paths;

        if (_configuration.configuration_sunat_authentication != null) {
            configuration_sunat_authentication = _configuration.configuration_sunat_authentication;
            configuration_sunat_authentication.ChangeGrantType(configuration.configuration_sunat_authentication.grant_type);
            configuration_sunat_authentication.ChangeScope(configuration.configuration_sunat_authentication.scope);
            configuration_sunat_authentication.ChangeClientId(configuration.configuration_sunat_authentication.client_id);
            configuration_sunat_authentication.ChangeClientSecret(configuration.configuration_sunat_authentication.client_secret);
            configuration_sunat_authentication.ChangeUsername(configuration.configuration_sunat_authentication.username);
            configuration_sunat_authentication.ChangePassword(configuration.configuration_sunat_authentication.password);
        } else {
            configuration_sunat_authentication = DomainConfiguration.ConfigurationSunatAuthentication.Create(
                configuration.configuration_sunat_authentication.grant_type,
                configuration.configuration_sunat_authentication.scope,
                configuration.configuration_sunat_authentication.client_id,
                configuration.configuration_sunat_authentication.client_secret,
                configuration.configuration_sunat_authentication.username,
                configuration.configuration_sunat_authentication.password
            );
        }

        if (_configuration.configuration_sunat_endpoints != null) {
            configuration_sunat_endpoints = _configuration.configuration_sunat_endpoints;
            configuration_sunat_endpoints.ChangeDespatchAdviceUrl(configuration.configuration_sunat_endpoints.despatch_advice_url);
        } else {
            configuration_sunat_endpoints = DomainConfiguration.ConfigurationSunatEndpoints.Create(
                configuration.configuration_sunat_endpoints.despatch_advice_url
            );
        }

        if (_configuration.configuration_paths != null) {
            configuration_paths = _configuration.configuration_paths;
            configuration_paths.ChangeCertificate(configuration.configuration_paths.certificate);
            configuration_paths.ChangeCertificatePassword(configuration.configuration_paths.certificate_password);
            configuration_paths.ChangeInput(configuration.configuration_paths.input);
            configuration_paths.ChangeOutput(configuration.configuration_paths.output);
            configuration_paths.ChangeDespatchAdviceTemplate(configuration.configuration_paths.despatch_advice_template);
        } else {
            configuration_paths = DomainConfiguration.ConfigurationPaths.Create(
                configuration.configuration_paths.certificate,
                configuration.configuration_paths.certificate_password,
                configuration.configuration_paths.input,
                configuration.configuration_paths.output,
                configuration.configuration_paths.despatch_advice_template
            );
        }

        _configuration.ChangeId(configuration.id);
        _configuration.ChangePartyIdentification(configuration.party_identification);
        _configuration.ChangePartyName(configuration.party_name);
        _configuration.ChangeRegistrationName(configuration.registration_name);
        _configuration.ChangeAddressTypeCode(configuration.address_type_code);
        _configuration.ChangeCitySubdivisionName(configuration.city_subdivision_name);
        _configuration.ChangeCityName(configuration.city_name);
        _configuration.ChangeCountrySubentity(configuration.country_subentity);
        _configuration.ChangeDistrict(configuration.district);
        _configuration.ChangeAddressLine(configuration.address_line);
        _configuration.ChangeIdentificationCode(configuration.identification_code);
        _configuration.ChangeConfigurationSunatAuthentication(configuration_sunat_authentication);
        _configuration.ChangeConfigurationSunatEndpoints(configuration_sunat_endpoints);
        _configuration.ChangeConfigurationPaths(configuration_paths);

        await configuration_repository.Update(_configuration);
    }
}