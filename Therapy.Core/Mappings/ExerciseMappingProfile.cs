using AutoMapper;
using Therapy.Domain.DTOs.Exercise;
using Therapy.Domain.DTOs.Media;
using Therapy.Domain.Entities;

namespace Therapy.Core.Mappings
{
    public class ExerciseMappingProfile : Profile
    {
        public ExerciseMappingProfile()
        {
            CreateMap<ExerciseCreateDTO, Exercise>();
            CreateMap<MediaCreateDTO, Media>();
            CreateMap<Exercise, ExerciseDTO>();
            CreateMap<Media, MediaDTO>();
            CreateMap<ExerciseUpdateDTO, Exercise>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<MediaUpdateDTO, Media>();
            
        }
    }
}