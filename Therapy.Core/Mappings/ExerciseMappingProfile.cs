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
            CreateMap<Exercise, ExerciseDTO>();

            CreateMap<ExerciseCreateDTO, Exercise>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<IEnumerable<ExerciseDTO>, IEnumerable<Exercise>>();
            CreateMap<ExerciseUpdateDTO, Exercise>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Media, MediaDTO>();
            CreateMap<MediaDTO, Media>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<MediaUpdateDTO, Media>();
            CreateMap<MediaCreateDTO, Media>();
        }
    }
}