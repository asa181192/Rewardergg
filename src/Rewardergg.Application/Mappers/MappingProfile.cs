using AutoMapper;
using Rewardergg.Application.DTOs;
using Rewardergg.Domain;

namespace Rewardergg.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tournament, TournamentDto>().ReverseMap();
        }
    }
}
