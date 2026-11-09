using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            //CreateMap<Region, RegionDto>()
            //    .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name)) //explicity mappings if property name differ in region and dtos
            //    .ReverseMap();

            //Mapping for Regions
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            //Mapping for Walks
            CreateMap<Walk, AddWalkRequestDto>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();

            //Mapping fro Difficulty
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();



        }
    }
}
