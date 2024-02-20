namespace TetraSign.Core.Application.Configuration;

public record ConfigurationPathsDTO(
    string certificate,
    string certificate_password,
    string input,
    string output,
    string despatch_advice_template
);