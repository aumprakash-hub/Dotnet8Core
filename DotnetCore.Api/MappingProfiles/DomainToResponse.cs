using AutoMapper;
using DotnetCore.Entities.DbSet;
using DotnetCore.Entities.Dtos.Responses;

namespace DotnetCore.Api.MappingProfiles;

public class DomainToResponse: Profile
{
    public DomainToResponse()
    {
        CreateMap<Achievement, DriverAchievementResponse>()
            .ForMember(dest => dest.Wins, 
                opt => opt.MapFrom(src => src.RaceWins));
        
        CreateMap<Driver, GetDriverResponse>()
            .ForMember(dest => dest.DriverId, 
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, 
                opt => opt.MapFrom(src => $"{src.FirstName}  {src.LastName}"));

    }
}