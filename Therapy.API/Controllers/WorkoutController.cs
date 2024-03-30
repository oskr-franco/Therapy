
using Microsoft.AspNetCore.Mvc;
using Therapy.Core.Services.Workouts;
using Therapy.Core.Utils;
using Therapy.Domain.DTOs.Workout;
using Therapy.Domain.Models;

/// <summary>
/// Controller for managing workouts.
/// </summary>
public class WorkoutController: ApiController {
    private readonly IWorkoutService _workoutService;
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkoutController"/> class.
    /// </summary>
    /// <param name="workoutService"></param>
    public WorkoutController(IWorkoutService workoutService) {
        _workoutService = workoutService;
    }
    /// <summary>
    /// Gets all workouts.
    /// </summary>
    /// <param name="filter"> The filter to apply to the workouts.</param>
    /// <returns>A list of workouts</returns>
    /// <response code="200">Returns the list of workouts.</response>
    /// <response code="404">If no workouts are found.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PaginationResponse<WorkoutDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] WorkoutPaginationFilter filter) {
        var workouts = await _workoutService.GetAllAsync(filter);
        if (workouts == null || workouts.Data.Count() == 0) {
            return NotFound();
        }
        return Ok(workouts);
    }
    
    /// <summary>
    /// Gets a workout by ID.
    /// </summary>
    /// <param name="id">The ID of the workout to retrieve.</param>
    /// <returns>A workout.</returns>
    /// <response code="200">Returns the workout.</response>
    /// <response code="404">If the workout is not found.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(WorkoutDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int id) {
        var workout = await _workoutService.GetByIdAsync(id);
        if (workout == null) {
            return NotFound();
        }
        return Ok(workout);
    }
    
    /// <summary>
    /// Gets a workout by Slug.
    /// </summary>
    /// <param name="slug">The Slug of the workout to retrieve.</param>
    /// <returns>A workout.</returns>
    /// <response code="200">Returns the workout.</response>
    /// <response code="404">If the workout is not found.</response>
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(WorkoutDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySlug(string slug) {
        var id = SlugConverter.GetIdFromSlug(slug);
        var workout = await _workoutService.GetByIdAsync(id);
        if (workout == null  || !slug.Equals(workout.Slug)) {
            return NotFound();
        }
        return Ok(workout);
    }

    /// <summary>
    /// Creates a new workout.
    /// </summary>
    /// <param name="workout">The workout to create.</param>
    /// <returns>The created workout.</returns>
    /// <response code="201">Returns the created workout.</response>
    /// <response code="400">If the workout is invalid.</response>
    /// ToDo: <response code="409">If the exercise already exists.</response>
    /// <response code="500">If the workout could not be created.</response>
    [HttpPost]
    [ProducesResponseType(typeof(WorkoutDTO), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(WorkoutCreateDTO workout) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
    
        var createdWorkout = await _workoutService.AddAsync(workout);
        return CreatedAtAction(nameof(Get), new { id = createdWorkout.Id }, createdWorkout);
    }

    /// <summary>
    /// Updates a workout.
    /// </summary>
    /// <param name="id"> The ID of the workout to update.</param>
    /// <param name="workout"> The updated workout.</param>
    /// <returns> No content.</returns>
    /// <response code="204">If the workout was updated successfully.</response>
    /// <response code="400">If the workout is invalid.</response>
    /// <response code="404">If the workout is not found.</response>
    /// <response code="500">If the workout could not be updated.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(int id, WorkoutUpdateDTO workout) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
    
        await _workoutService.UpdateAsync(id, workout);
        return NoContent();
    }

    /// <summary>
    /// Deletes a workout.
    /// </summary>
    /// <param name="id">The ID of the workout to delete.</param>
    /// <returns> No content.</returns>
    /// <response code="204">If the workout was deleted successfully.</response>
    /// <response code="404">If the workout is not found.</response>
    /// <response code="500">If the workout could not be deleted.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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