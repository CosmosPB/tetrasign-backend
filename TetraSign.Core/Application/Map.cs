using AutoMapper;
using TetraSign.Core.Application.Configuration;
using TetraSign.Core.Application.Documents;
using TetraSign.Core.Application.Documents.ThirdPartyDocuments;
using TetraSign.Core.Domain.Documents.ThirdPartyDocuments;
using DomainConfiguration = TetraSign.Core.Domain.Configuration;
using DomainDocuments = TetraSign.Core.Domain.Documents;

namespace TetraSign.Core.Application;

public class Map: Profile
{
    public Map()
    {
        CreateMap<DomainConfiguration.Configuration, ConfigurationDTO>();
        CreateMap<DomainConfiguration.ConfigurationSunatAuthentication, ConfigurationSunatAuthenticationDTO>();
        CreateMap<DomainConfiguration.ConfigurationSunatEndpoints, ConfigurationSunatEndpointsDTO>();
        CreateMap<DomainConfiguration.ConfigurationPaths, ConfigurationPathsDTO>();
        CreateMap<DomainDocuments.Document<DespatchAdvice>, DocumentDTO<DespatchAdviceDTO>>();
        CreateMap<DespatchAdvice, DespatchAdviceDTO>();
        CreateMap<DespatchAdviceLine, DespatchAdviceLineDTO>();
        CreateMap<ThirdPartyDocument, ThirdPartyDocumentDTO>();
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
