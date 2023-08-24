using Microsoft.AspNetCore.Mvc;
using Therapy.Core.Services.Exercises;
using Therapy.Domain.DTOs.Exercise;
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
    public async Task<IActionResult> GetAll()
    {
        var exercises = await _exerciseService.GetAllAsync();
        return Ok(exercises);
    }

    /// <summary>
    /// Gets an exercise by ID.
    /// </summary>
    /// <param name="id">The ID of the exercise to retrieve.</param>
    /// <returns>An exercise.</returns>
    /// <response code="200">Returns the exercise.</response>
    /// <response code="404">If the exercise is not found.</response>
    [HttpGet("{id}")]
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
    /// Creates an exercise.
    /// </summary>
    /// <param name="exercise">The exercise to create.</param>
    /// <returns>A newly created exercise.</returns>
    /// <response code="201">Returns the newly created exercise.</response>
    /// <response code="400">If the exercise is invalid.</response>
    /// <response code="409">If the exercise already exists.</response>
    /// <response code="500">If there was an internal server error.</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ExerciseCreateDTO exercise)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await _exerciseService.AddAsync(exercise);
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }

    /// <summary>
    /// Updates an exercise.
    /// </summary>
    /// <param name="id">The ID of the exercise to update.</param>
    /// <param name="exercise">The updated exercise.</param>
    /// <returns>An updated exercise.</returns>
    /// <response code="204">Returns no content.</response>
    /// <response code="400">If the exercise is invalid.</response>
    /// <response code="404">If the exercise is not found.</response>
    /// <response code="500">If there was an internal server error.</response>
    [HttpPut("{id}")]
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
    public async Task<IActionResult> Delete(int id)
    {
        await _exerciseService.DeleteAsync(id);
        return NoContent();
    }
}
