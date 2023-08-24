
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Therapy.Domain.DTOs.Workout;
using Therapy.Domain.Entities;
using Therapy.Domain.Exceptions;
using Therapy.Infrastructure.Repositories;

namespace Therapy.Core.Services.Workouts {
    public class WorkoutService : IWorkoutService
    {
        private readonly IRepository<Workout> _workoutRepository;
        private readonly IMapper _mapper;

        public WorkoutService(IRepository<Workout> workoutRepository, IMapper mapper)
        {
            _workoutRepository = workoutRepository;
            _mapper = mapper;
        }

        public async Task<WorkoutDTO> GetByIdAsync(int id)
        {
            var workout = await _workoutRepository.GetByIdAsync(
                id,
                include: 
                    x => x.Include(e => e.WorkoutExercises)
                            .ThenInclude(we => we.Exercise)
                            .ThenInclude(e => e.Media)
            );
            return _mapper.Map<WorkoutDTO>(workout);
        }

        public async Task<IEnumerable<WorkoutDTO>> GetAllAsync()
        {
            var workouts = await _workoutRepository.GetAllAsync(include: e => e.Include(x => x.WorkoutExercises));
            return _mapper.Map<IEnumerable<WorkoutDTO>>(workouts);
        }

        public async Task<WorkoutDTO> AddAsync(WorkoutCreateDTO workout)
        {
           var updatedWorkout = await _workoutRepository.AddAsync(_mapper.Map<Workout>(workout));
           return _mapper.Map<WorkoutDTO>(updatedWorkout);
        }

        public async Task DeleteAsync(int id)
        {
            await _workoutRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(int id, WorkoutUpdateDTO workout)
        {
            if(id != workout.Id) {
                throw new ValidationException("Exercise ID does not match");
            }
            var existingWorkout = await _workoutRepository.GetByIdAsync(id, include: x=> x.Include(e => e.WorkoutExercises));
             if (existingWorkout == null)
            {
                throw new NotFoundException(nameof(Exercise), id);
            }
            _mapper.Map(workout, existingWorkout);
            await _workoutRepository.UpdateAsync(existingWorkout);
        }
    }
}