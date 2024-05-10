
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Therapy.Core.Services.AuthAccessor;
using Therapy.Core.Services.Exercises;
using Therapy.Core.Services.Workouts;
using Therapy.Domain.DTOs.Exercise;
using Therapy.Domain.DTOs.Workout;
using Therapy.Domain.Models;

/// <summary>
/// Controller for managing user data.
/// </summary>
public class UserController: ApiController {
  private readonly IExerciseService _exerciseService;
  private readonly IWorkoutService _workoutService;
  private readonly IAuthAccessorService _authService;
  /// <summary>
  /// Initializes a new instance of the <see cref="UserController"/> class.
  /// </summary>
  /// <param name="workoutService"></param>
  /// <param name="exerciseService"></param>
  /// <param name="authService"></param>
  public UserController(IWorkoutService workoutService,IExerciseService exerciseService, IAuthAccessorService authService) {
      _exerciseService = exerciseService;
      _workoutService = workoutService;
      _authService = authService;
  }

  /// <summary>
  /// Gets all exercises by user based on token.
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  [HttpGet("Exercise")]
  [ProducesResponseType(typeof(PaginationResponse<ExerciseDTO>), StatusCodes.Status200OK)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> GetExercises([FromQuery] PaginationFilter filter) {
    var userId = _authService.GetId();
    if (userId == null) {
      return Unauthorized();
    }
    var exercises = await _exerciseService.GetByUserIdAsync((int)userId, filter);
    return Ok(exercises);
  }
  

  /// <summary>
  /// Gets all workouts by user based on token.
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  [HttpGet("Workout")]
  [ProducesResponseType(typeof(PaginationResponse<WorkoutDTO>), StatusCodes.Status200OK)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> GetWorkouts([FromQuery] PaginationFilter filter) {
    var userId = _authService.GetId();
    if (userId == null) {
      return Unauthorized();
    }
    var workouts = await _workoutService.GetByUserIdAsync((int)userId, filter);
    return Ok(workouts);
  }
}