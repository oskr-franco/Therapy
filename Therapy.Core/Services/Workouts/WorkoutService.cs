
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Therapy.Core.Extensions;
using Therapy.Domain.DTOs.Workout;
using Therapy.Domain.Entities;
using Therapy.Domain.Exceptions;
using Therapy.Domain.Models;
using Therapy.Infrastructure.Repositories;
using Therapy.Core.Extensions.WorkoutExercises;

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
            workout.WorkoutExercises.Order();
            return _mapper.Map<WorkoutDTO>(workout);
        }

        public async Task<PaginationResponse<WorkoutDTO>> GetAllAsync(WorkoutPaginationFilter filter)
        {
            var workoutsQuery = _workoutRepository.AsQueryableIncludeByFilter(filter);
            var earliestDate = _workoutRepository.AsQueryable().Min(e => (DateTime?)e.CreatedAt);
            var latestDate = _workoutRepository.AsQueryable().Max(e => (DateTime?)e.CreatedAt);
            var workouts =
                    await workoutsQuery
                    .Paginate(
                      filter,
                      (search) => e => e.Name.Contains(search)
                    )
                    .ToPaginationResponse<Workout, WorkoutDTO>(_mapper, earliestDate, latestDate);
            return workouts;
        }

        public async Task<WorkoutDTO> AddAsync(WorkoutCreateDTO workoutDTO)
        {
           var workout = _mapper.Map<Workout>(workoutDTO);
           workout.WorkoutExercises.SetOrder();
           var updatedWorkout = await _workoutRepository.AddAsync(workout);
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
            existingWorkout.WorkoutExercises.SetOrder();
            await _workoutRepository.UpdateAsync(existingWorkout);
        }
    }
}