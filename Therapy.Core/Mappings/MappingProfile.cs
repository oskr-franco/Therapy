using AutoMapper;
using Therapy.Domain.DTOs;
using Therapy.Domain.Entities;

namespace Therapy.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseMedia, ExerciseMediaDto>();
            CreateMap<ExerciseDto, Exercise>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ExerciseMediaDto, ExerciseMedia>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}