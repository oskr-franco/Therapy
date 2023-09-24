
using Microsoft.AspNetCore.Mvc;
using Therapy.Core.Services.Workouts;
using Therapy.Domain.DTOs.Workout;

public class WorkoutController: ApiController {
    private readonly IWorkoutService _workoutService;
    public WorkoutController(IWorkoutService workoutService) {
        _workoutService = workoutService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var workouts = await _workoutService.GetAllAsync();
        return Ok(workouts);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
        var workout = await _workoutService.GetByIdAsync(id);
        if (workout == null) {
            return NotFound();
        }
        return Ok(workout);
    }

    [HttpPost]
    public async Task<IActionResult> Create(WorkoutCreateDTO workout) {
        var createdWorkout = await _workoutService.AddAsync(workout);
        return CreatedAtAction(nameof(Get), new { id = createdWorkout.Id }, createdWorkout);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, WorkoutUpdateDTO workout) {
        await _workoutService.UpdateAsync(id, workout);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        await _workoutService.DeleteAsync(id);
        return NoContent();
    }

    // [HttpPost("{id}/exercises")]
    // public async Task<IActionResult> AddExercise(int id, WorkoutExerciseCreateDTO workoutExercise) {
    //     var updatedWorkout = await _workoutService.AddExerciseAsync(id, workoutExercise);
    //     return Ok(updatedWorkout);
    // }

    // [HttpDelete("{id}/exercises/{exerciseId}")]
    // public async Task<IActionResult> RemoveExercise(int id, int exerciseId) {
    //     await _workoutService.RemoveExerciseAsync(id, exerciseId);
    //     return NoContent();
    // }

    // [HttpPut("{id}/exercises/{exerciseId}")]
    // public async Task<IActionResult> UpdateExercise(int id, int exerciseId, WorkoutExerciseUpdateDTO workoutExercise) {
    //     await _workoutService.UpdateExerciseAsync(id, exerciseId, workoutExercise);
    //     return NoContent();
    // }
}