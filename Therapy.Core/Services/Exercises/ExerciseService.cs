using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Therapy.Domain.DTOs.Exercise;
using Therapy.Domain.Entities;
using Therapy.Domain.Exceptions;
using Therapy.Domain.Models;
using Therapy.Infrastructure.Repositories;
using Therapy.Core.Extensions;

namespace Therapy.Core.Services.Exercises {
    public class ExerciseService : IExerciseService
    {
        private readonly IRepository<Exercise> _exerciseRepository;
        private readonly IMapper _mapper;

        public ExerciseService(
            IRepository<Exercise> exerciseRepository,
            IMapper mapper
        )
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<ExerciseDTO> GetByIdAsync(int id)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(id, include: x => x.Include(e => e.Media));

            return _mapper.Map<ExerciseDTO>(exercise);
        }

        public async Task<PaginationResponse<ExerciseDTO>> GetAllAsync(PaginationFilter filter)
        {
            
            var exercisesQuery = _exerciseRepository.AsQueryable(include: e => e.Include(x => x.Media));
            var earliestDate = _exerciseRepository.AsQueryable().Min(e => (DateTime?)e.CreatedAt);
            var latestDate = _exerciseRepository.AsQueryable().Max(e => (DateTime?)e.CreatedAt);
            var exercises =
                    await exercisesQuery
                    .Paginate(
                      filter,
                      (search) => e => e.Name.Contains(search) || e.Description.Contains(search) || e.Instructions.Contains(search)
                    )
                    .ToPaginationResponse<Exercise, ExerciseDTO>(_mapper, earliestDate, latestDate);
            return exercises;
        }

        public async Task<PaginationResponse<ExerciseDTO>> GetByUserIdAsync(int userId, PaginationFilter filter)
        {
            var exercisesQuery = _exerciseRepository.AsQueryable(
                include: e => e.Include(x => x.Media)
            ).Where(e => e.CreatedBy == userId);
            var earliestDate = _exerciseRepository.AsQueryable().Where(e => e.CreatedBy == userId).Min(e => (DateTime?)e.CreatedAt);
            var latestDate = _exerciseRepository.AsQueryable().Where(e => e.CreatedBy == userId).Max(e => (DateTime?)e.CreatedAt);
            var exercises =
                    await exercisesQuery
                    .Paginate(
                      filter,
                      (search) => e => e.Name.Contains(search) || e.Description.Contains(search) || e.Instructions.Contains(search)
                    )
                    .ToPaginationResponse<Exercise, ExerciseDTO>(_mapper, earliestDate, latestDate);
            return exercises;
        }

        public async Task<ExerciseDTO> AddAsync(ExerciseCreateDTO exercise)
        {
            var exerciseDb = _mapper.Map<Exercise>(exercise);
            var updatedExercise = await _exerciseRepository.AddAsync(exerciseDb);
            return _mapper.Map<ExerciseDTO>(updatedExercise);
        }

        public async Task UpdateAsync(int id, ExerciseUpdateDTO exercise)
        {
            if(id != exercise.Id) {
                throw new ValidationException(nameof(exercise.Id),"Exercise ID does not match");
            }
            var existingExercise = await _exerciseRepository.GetByIdAsync(id, include: x => x.Include(e => e.Media));
             if (existingExercise == null)
            {
                throw new NotFoundException(nameof(Exercise), id);
            }
            _mapper.Map(exercise, existingExercise);
            await _exerciseRepository.UpdateAsync(existingExercise);
        }

        public async Task DeleteAsync(int id)
        {
            await _exerciseRepository.DeleteAsync(id);
        }
    }
}