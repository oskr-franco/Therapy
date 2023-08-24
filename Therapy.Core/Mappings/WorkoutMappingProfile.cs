using AutoMapper;
using Therapy.Domain.DTOs.Workout;
using Therapy.Domain.DTOs.WorkoutExercise;
using Therapy.Domain.Entities;

namespace Therapy.Core.Mappings {
  public class WorkoutMappingProfile : Profile {
    public WorkoutMappingProfile() {
      CreateMap<WorkoutCreateDTO, Workout>();
      CreateMap<WorkoutExerciseCreateDTO, WorkoutExercise>();
      CreateMap<Workout, WorkoutDTO>();
      CreateMap<WorkoutExercise, WorkoutExerciseDTO>();
      CreateMap<WorkoutUpdateDTO, Workout>()
              .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
      CreateMap<WorkoutExerciseUpdateDTO, WorkoutExercise>();
    }
  }
}