using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Therapy.Domain.DTOs.Exercise;
using Therapy.Domain.Entities;
using Therapy.Infrastructure.Repositories;
using Therapy.Domain.Exceptions;

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

        public async Task<IEnumerable<ExerciseDTO>> GetAllAsync()
        {
            var exercises = await _exerciseRepository.GetAllAsync(include: e => e.Include(x => x.Media));
            return _mapper.Map<IEnumerable<ExerciseDTO>>(exercises);
        }

        public async Task<ExerciseDTO> AddAsync(ExerciseCreateDTO exercise)
        {
            var exerciseDb = _mapper.Map<Exercise>(exercise);
            var updatedExercise =await _exerciseRepository.AddAsync(exerciseDb);
            return _mapper.Map<ExerciseDTO>(updatedExercise);
        }

        public async Task UpdateAsync(int id, ExerciseUpdateDTO exercise)
        {
            if(id != exercise.Id) {
                throw new ValidationException("Exercise ID does not match");
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