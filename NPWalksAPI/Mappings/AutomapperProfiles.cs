using AutoMapper;
using NPWalksAPI.Models.Domain;
using NPWalksAPI.Models.DTO;

namespace NPWalksAPI.Mappings
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequest, Region>().ReverseMap();
            CreateMap<UpdateRegionDto, Region>().ReverseMap();
        }
    }
}
