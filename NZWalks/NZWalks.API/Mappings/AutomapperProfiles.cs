using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionDto, Region>();
            CreateMap<UpdateRegionDto, Region>();
            CreateMap<AddWalkDto, Walk>();
            CreateMap<Walk, WalkDto>();
        }
    }
}
