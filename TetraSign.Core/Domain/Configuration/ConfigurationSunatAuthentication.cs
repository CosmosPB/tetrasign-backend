namespace TetraSign.Core.Domain.Configuration;

public class ConfigurationSunatAuthentication {

    public string? grant_type { get; protected set; }
    public string? scope { get; protected set; }
    public string? client_id { get; protected set; }
    public string? client_secret { get; protected set; }
    public string? username { get; protected set; }
    public string? password { get; protected set; }

    public static ConfigurationSunatAuthentication Create(
        string grant_type,
        string scope,
        string client_id,
        string client_secret,
        string username,
        string password
    ) {
        ConfigurationSunatAuthentication configuration_sunat_authentication = new() {
            grant_type = grant_type,
            scope = scope,
            client_id = client_id,
            client_secret = client_secret,
            username = username,
            password = password
        };
        
        return configuration_sunat_authentication;
    }

    public virtual void ChangeGrantType(string? grant_type) => this.grant_type = grant_type;
    public virtual void ChangeScope(string? scope) => this.scope = scope;
    public virtual void ChangeClientId(string? client_id) => this.client_id = client_id;
    public virtual void ChangeClientSecret(string? client_secret) => this.client_secret = client_secret;
    public virtual void ChangeUsername(string? username) => this.username = username;
    public virtual void ChangePassword(string? password) => this.password = password;
}