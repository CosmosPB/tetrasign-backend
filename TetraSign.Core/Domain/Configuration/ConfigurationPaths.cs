namespace TetraSign.Core.Domain.Configuration;

public class ConfigurationPaths {

    public string? certificate { get; protected set; }
    public string? certificate_password { get; protected set; }
    public string? input { get; protected set; }
    public string? output { get; protected set; }
    public string? despatch_advice_template { get; protected set; }

    public static ConfigurationPaths Create(
        string certificate,
        string certificate_password,
        string input,
        string output,
        string despatch_advice_template
    ) {
        ConfigurationPaths configuration_paths = new() {
            certificate = certificate,
            certificate_password = certificate_password,
            input = input,
            output = output,
            despatch_advice_template = despatch_advice_template,
        };
        
        return configuration_paths;
    }

    public virtual void ChangeCertificate(string? certificate) => this.certificate = certificate;
    public virtual void ChangeCertificatePassword(string? certificate_password) => this.certificate_password = certificate_password;
    public virtual void ChangeInput(string? input) => this.input = input;
    public virtual void ChangeOutput(string? output) => this.output = output;
    public virtual void ChangeDespatchAdviceTemplate(string? despatch_advice_template) => this.despatch_advice_template = despatch_advice_template;
}