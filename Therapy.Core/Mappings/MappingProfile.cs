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
            CreateMap<Media, MediaDto>();
            CreateMap<ExerciseDto, Exercise>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<MediaDto, Media>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<IEnumerable<ExerciseDto>, IEnumerable<Exercise>>();
        }
    }
}