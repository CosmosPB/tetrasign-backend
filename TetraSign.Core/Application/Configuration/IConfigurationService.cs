using TetraSign.Core.Helpers;

namespace TetraSign.Core.Application.Configuration;

public interface IConfigurationService {

    Task<ConfigurationDTO> FindById(string id);
    Task<ConfigurationDTO> Add(ConfigurationDTO configuration);
}