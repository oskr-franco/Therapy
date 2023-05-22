using AutoMapper;
using Therapy.Domain.DTOs;
using Therapy.Domain.Entities;
using Therapy.Infrastructure.Repositories;

namespace Therapy.Core.Services {
    public class ExerciseService : IExerciseService
    {
        private readonly IRepository<Exercise> _exerciseRepository;
        private readonly IMapper _mapper;

        public ExerciseService(IRepository<Exercise> exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<ExerciseDto> GetByIdAsync(int id)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(id);
            return _mapper.Map<ExerciseDto>(exercise);
        }

        public async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return await _exerciseRepository.GetAllAsync();
        }

        public async Task AddAsync(ExerciseDto exercise)
        {
            await _exerciseRepository.AddAsync(_mapper.Map<Exercise>(exercise));
        }

        public async Task UpdateAsync(ExerciseDto exercise)
        {
            await _exerciseRepository.UpdateAsync(_mapper.Map<Exercise>(exercise));
        }

        public async Task DeleteAsync(int id)
        {
            await _exerciseRepository.DeleteAsync(id);
        }

        public async Task DeleteAsync(ExerciseDto exercise)
        {
            await _exerciseRepository.DeleteAsync(_mapper.Map<Exercise>(exercise));
        }
    }
}