using AutoMapper;
using TetraSign.Core.Application.Configuration;
using DomainConfiguration = TetraSign.Core.Domain.Configuration;

namespace TetraSign.Core.Application;

public class Map: Profile
{
    public Map()
    {
        CreateMap<DomainConfiguration.Configuration, ConfigurationDTO>();
        CreateMap<DomainConfiguration.ConfigurationSunatAuthentication, ConfigurationSunatAuthenticationDTO>();
        CreateMap<DomainConfiguration.ConfigurationSunatEndpoints, ConfigurationSunatEndpointsDTO>();
        CreateMap<DomainConfiguration.ConfigurationPaths, ConfigurationPathsDTO>();
        // CreateMap<ConfigurationDTO, DomainConfiguration.Configuration>();
        // Mapper.CreateMap<CartProduct, CartProductDto>();

        // Mapper.CreateMap<Purchase, CheckOutResultDto>()
        //     .ForMember(x => x.PurchaseId, options => options.MapFrom(x => x.Id));

        // Mapper.CreateMap<CreditCard, CreditCardDto>();
        // Mapper.CreateMap<Customer, CustomerDto>();
        // Mapper.CreateMap<Product, ProductDto>();
        // Mapper.CreateMap<CustomerPurchaseHistoryReadModel, CustomerPurchaseHistoryDto>();
        // Mapper.CreateMap<DomainEventRecord, EventDto>();
    }
}
