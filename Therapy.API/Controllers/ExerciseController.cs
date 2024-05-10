using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Therapy.Core.Services.Exercises;
using Therapy.Core.Utils;
using Therapy.Domain.DTOs.Exercise;
using Therapy.Domain.Models;

/// <summary>
/// Controller for managing exercises.
/// </summary>
public class ExerciseController : ApiController
{
    private readonly IExerciseService _exerciseService;
    /// <summary>
    /// Initializes a new instance of the <see cref="ExerciseController"/> class.
    /// </summary>
    /// <param name="exerciseService">The exercise service.</param>
    public ExerciseController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    /// <summary>
    /// Gets all exercises.
    /// </summary>
    /// <returns>A list of all exercises.</returns>
    /// <response code="200">Returns the list of exercises.</response>
    /// <response code="404">If no exercises are found.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PaginationResponse<ExerciseDTO>), StatusCodes.Status200OK)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
    {
        var exercises = await _exerciseService.GetAllAsync(filter);
        if (exercises == null || exercises.Data.Count() == 0)
        {
            return NotFound();
        }
        return Ok(exercises);
    }

    /// <summary>
    /// Gets an exercise by ID.
    /// </summary>
    /// <param name="id">The ID of the exercise to retrieve.</param>
    /// <returns>An exercise.</returns>
    /// <response code="200">Returns the exercise.</response>
    /// <response code="404">If the exercise is not found.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ExerciseDTO), StatusCodes.Status200OK)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get(int id)
    {
        var exercise = await _exerciseService.GetByIdAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }
        return Ok(exercise);
    }

    /// <summary>
    /// Gets an exercise by Slug.
    /// </summary>
    /// <param name="slug">The ID of the exercise to retrieve.</param>
    /// <returns>An exercise.</returns>
    /// <response code="200">Returns the exercise.</response>
    /// <response code="404">If the exercise is not found.</response>
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(ExerciseDTO), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> Get(string slug)
    {
        var id = SlugConverter.GetIdFromSlug(slug);
        var exercise = await _exerciseService.GetByIdAsync(id);

        if (exercise == null || !slug.Equals(exercise.Slug))
        {
            return NotFound();
        }
        return Ok(exercise);
    }

    /// <summary>
    /// Creates an exercise.
    /// </summary>
    /// <param name="exercise">The exercise to create.</param>
    /// <returns>A newly created exercise.</returns>
    /// <response code="201">Returns the newly created exercise.</response>
    /// <response code="400">If the exercise is invalid.</response>
    /// ToDo: <response code="409">If the exercise already exists.</response>
    /// <response code="500">If there was an internal server error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ExerciseDTO), StatusCodes.Status201Created)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] ExerciseCreateDTO exercise)
    {
        var response = await _exerciseService.AddAsync(exercise);
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }

    /// <summary>
    /// Updates an exercise.
    /// </summary>
    /// <param name="id">The ID of the exercise to update.</param>
    /// <param name="exercise">The updated exercise.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the workout was updated successfully.</response>
    /// <response code="400">If the exercise is invalid.</response>
    /// <response code="404">If the exercise is not found.</response>
    /// <response code="500">If there was an internal server error.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] ExerciseUpdateDTO exercise)
    {
        await _exerciseService.UpdateAsync(id, exercise);
        return NoContent();
    }

    /// <summary>
    /// Deletes an exercise.
    /// </summary>
    /// <param name="id">The ID of the exercise to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">Returns no content.</response>
    /// <response code="404">If the exercise is not found.</response>
    /// <response code="500">If there was an internal server error.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _exerciseService.DeleteAsync(id);
        return NoContent();
    }
}
