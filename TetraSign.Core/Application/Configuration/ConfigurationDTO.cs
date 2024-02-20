namespace TetraSign.Core.Application.Configuration;

public record ConfigurationDTO(
    string id,
    string party_identification,
    string party_name,
    string registration_name,
    string address_type_code,
    string city_subdivision_name,
    string city_name,
    string country_subentity,
    string country_subentity_code,
    string district,
    string address_line,
    string identification_code,
    ConfigurationSunatAuthenticationDTO configuration_sunat_authentication,
    ConfigurationSunatEndpointsDTO configuration_sunat_endpoints,
    ConfigurationPathsDTO configuration_paths
);