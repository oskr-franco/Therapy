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
        }
    }
}