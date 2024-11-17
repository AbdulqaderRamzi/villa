using AutoMapper;
using Villa.Domain.Entities;
using Villa.Domain.Entities.Dtos.Villa;
using Villa.Domain.Entities.Dtos.VIllaNumber;

namespace Villa.Api;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        // Src -> Dist
        CreateMap<VillaModel, VillaDto>().ReverseMap();
        CreateMap<VillaModel, VillaCreateDto>().ReverseMap();
        CreateMap<VillaModel, VillaUpdateDto>().ReverseMap();

        CreateMap<VillaNumber, VillaNumberDto>().ReverseMap();
        CreateMap<VillaNumber, VillaNumberCreateDto>().ReverseMap();
        CreateMap<VillaNumber, VillaNumberUpdateDto>().ReverseMap();
    }
}
