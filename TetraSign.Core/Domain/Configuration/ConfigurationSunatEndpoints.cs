namespace TetraSign.Core.Domain.Configuration;

public class ConfigurationSunatEndpoints {

    public string despatch_advice_url { get; protected set; }

    public static ConfigurationSunatEndpoints Create(
        string despatch_advice_url
    ) {
        ConfigurationSunatEndpoints configuration_sunat_endpoints = new() {
            despatch_advice_url = despatch_advice_url
        };
        
        return configuration_sunat_endpoints;
    }

    public virtual void ChangeDespatchAdviceUrl(string despatch_advice_url) => this.despatch_advice_url = despatch_advice_url;
}