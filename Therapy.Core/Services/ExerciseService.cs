using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Therapy.Domain.DTOs;
using Therapy.Domain.Entities;
using Therapy.Infrastructure.Repositories;
using Therapy.Domain.Exceptions;

namespace Therapy.Core.Services {
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

        public async Task<ExerciseDto> GetByIdAsync(int id)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(id, include: x => x.Include(e => e.Media));

            return _mapper.Map<ExerciseDto>(exercise);
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllAsync()
        {
            var exercises = await _exerciseRepository.GetAllAsync(include: e => e.Include(x => x.Media));
            return _mapper.Map<IEnumerable<ExerciseDto>>(exercises);
        }

        public async Task<ExerciseDto> AddAsync(ExerciseDto exercise)
        {
            var exerciseDb = _mapper.Map<Exercise>(exercise);
            await _exerciseRepository.AddAsync(exerciseDb);
            return _mapper.Map<ExerciseDto>(exerciseDb);
        }

        public async Task UpdateAsync(int id, ExerciseDto exercise)
        {
            if(id != exercise.Id) {
                throw new ValidationException("Exercise ID does not match");
            }
            var existingExercise = await this.GetByIdAsync(id);
             if (existingExercise == null)
            {
                throw new NotFoundException(nameof(Exercise), id);
            }
            await _exerciseRepository.UpdateAsync(_mapper.Map<Exercise>(exercise));
        }

        public async Task DeleteAsync(int id)
        {
            await _exerciseRepository.DeleteAsync(id);
        }
    }
}