using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TetraSign.Core.Domain.Configuration;

public class Configuration: IAggregateRoot {

    [BsonId]
    [BsonElement("Id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public virtual string? id { get; protected set; }
    public virtual string? party_identification { get; protected set; }
    public virtual string? party_name { get; protected set; }
    public virtual string? registration_name { get; protected set; }
    public virtual string? address_type_code { get; protected set; }
    public virtual string? city_subdivision_name { get; protected set; }
    public virtual string? city_name { get; protected set; }
    public virtual string? country_subentity { get; protected set; }
    public virtual string? country_subentity_code { get; protected set; }
    public virtual string? district { get; protected set; }
    public virtual string? address_line { get; protected set; }
    public virtual string? identification_code { get; protected set; }
    public virtual ConfigurationSunatAuthentication? configuration_sunat_authentication { get; protected set; }
    public virtual ConfigurationSunatEndpoints? configuration_sunat_endpoints { get; protected set; }
    public virtual ConfigurationPaths? configuration_paths { get; protected set; }

    public static Configuration Create(
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
        ConfigurationSunatAuthentication configuration_sunat_authentication,
        ConfigurationSunatEndpoints configuration_sunat_endpoints,
        ConfigurationPaths configuration_paths
    ) {
        Configuration configuration = new() {
            party_identification = party_identification,
            party_name = party_name,
            registration_name = registration_name,
            address_type_code = address_type_code,
            city_subdivision_name = city_subdivision_name,
            city_name = city_name,
            country_subentity = country_subentity,
            country_subentity_code = country_subentity_code,
            district = district,
            address_line = address_line,
            identification_code = identification_code,
            configuration_sunat_authentication = configuration_sunat_authentication,
            configuration_sunat_endpoints = configuration_sunat_endpoints,
            configuration_paths = configuration_paths
        };
        
        return configuration;
    }

    public virtual void ChangeId(string? id) => this.id = id;
    public virtual void ChangePartyIdentification(string? party_identification) => this.party_identification = party_identification;
    public virtual void ChangePartyName(string? party_name) => this.party_name = party_name;
    public virtual void ChangeRegistrationName(string? registration_name) => this.registration_name = registration_name;
    public virtual void ChangeAddressTypeCode(string? address_type_code) => this.address_type_code = address_type_code;
    public virtual void ChangeCitySubdivisionName(string? city_subdivision_name) => this.city_subdivision_name = city_subdivision_name;
    public virtual void ChangeCityName(string? city_name) => this.city_name = city_name;
    public virtual void ChangeCountrySubentity(string? country_subentity) => this.country_subentity = country_subentity;
    public virtual void ChangeCountrySubentityCode(string? country_subentity_code) => this.country_subentity_code = country_subentity_code;
    public virtual void ChangeDistrict(string? district) => this.district = district;
    public virtual void ChangeAddressLine(string? address_line) => this.address_line = address_line;
    public virtual void ChangeIdentificationCode(string? identification_code) => this.identification_code = identification_code;
    public virtual void ChangeConfigurationSunatAuthentication(ConfigurationSunatAuthentication? configuration_sunat_authentication) => this.configuration_sunat_authentication = configuration_sunat_authentication;
    public virtual void ChangeConfigurationSunatEndpoints(ConfigurationSunatEndpoints? configuration_sunat_endpoints) => this.configuration_sunat_endpoints = configuration_sunat_endpoints;
    public virtual void ChangeConfigurationPaths(ConfigurationPaths? configuration_paths) => this.configuration_paths = configuration_paths;
}