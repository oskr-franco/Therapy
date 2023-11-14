using AutoMapper;
using Therapy.Domain.DTOs.Media;
using Therapy.Domain.DTOs.Workout;
using Therapy.Domain.DTOs.WorkoutExercise;
using Therapy.Domain.Entities;

namespace Therapy.Core.Mappings {
  public class WorkoutMappingProfile : Profile {
    public WorkoutMappingProfile() {
      CreateMap<WorkoutCreateDTO, Workout>();
      CreateMap<WorkoutExerciseCreateDTO, WorkoutExercise>();
      CreateMap<Workout, WorkoutDTO>();
      CreateMap<WorkoutExercise, WorkoutExerciseDTO>()
            .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.Exercise != null ? src.Exercise.Id : src.ExerciseId))
        // Map Exercise properties using AfterMap
        .AfterMap((src, dest, context) =>
        {
            if (src == null || src.Exercise == null) return;
            dest.Name = src.Exercise.Name;
            dest.Description = src.Exercise.Description;
            dest.Instructions = src.Exercise.Instructions;
            if (src.Exercise.Media != null)
            {
                dest.Media = context.Mapper.Map<ICollection<MediaDTO>>(src.Exercise.Media);
            }
        });

      CreateMap<WorkoutUpdateDTO, Workout>()
              .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
      CreateMap<WorkoutExerciseUpdateDTO, WorkoutExercise>();
    }
  }
}