using AutoMapper;
using Netfirebase.Api.Models.Domain;
using Netfirebase.Api.Vms;

namespace Netfirebase.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductVm>();
    }
}
