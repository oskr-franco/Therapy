using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Therapy.Domain.DTOs;
using Therapy.Domain.Entities;
using Therapy.Infrastructure.Repositories;

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

        public async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return await _exerciseRepository.GetAllAsync();
        }

        public async Task<ExerciseDto> AddAsync(ExerciseDto exercise)
        {
            var exerciseDb = _mapper.Map<Exercise>(exercise);
            await _exerciseRepository.AddAsync(exerciseDb);
            return _mapper.Map<ExerciseDto>(exerciseDb);
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