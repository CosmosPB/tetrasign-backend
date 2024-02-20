namespace TetraSign.Core.Application.Configuration;

public record ConfigurationSunatAuthenticationDTO(
    string grant_type,
    string scope,
    string client_id,
    string client_secret,
    string username,
    string password
);