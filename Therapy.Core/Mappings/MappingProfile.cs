using AutoMapper;
using Therapy.Domain.DTOs;
using Therapy.Domain.Entities;

namespace Therapy.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, ExerciseDTO>();
            CreateMap<Media, MediaDTO>();
            CreateMap<ExerciseDTO, Exercise>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<MediaDTO, Media>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<IEnumerable<ExerciseDTO>, IEnumerable<Exercise>>();
        }
    }
}