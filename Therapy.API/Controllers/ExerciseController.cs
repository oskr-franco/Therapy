using Microsoft.AspNetCore.Mvc;
using Therapy.Core.Services;
using Therapy.Domain.DTOs;

[ApiController]
[Route("[controller]")]
public class ExerciseController : ControllerBase
{
    private readonly IExerciseService _exerciseService;

    public ExerciseController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var exercises = await _exerciseService.GetAllAsync();
        return Ok(exercises);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get()
    {
        var exercise = await _exerciseService.GetByIdAsync(1);
        if (exercise == null)
        {
            return NotFound();
        }
        return Ok(exercise);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ExerciseDto exercise)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _exerciseService.AddAsync(exercise);
        return CreatedAtAction(nameof(Get), new { id = exercise.Id }, exercise);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ExerciseDto exercise)
    {
        if (id != exercise.Id)
        {
            return BadRequest();
        }

        var existingExercise = await _exerciseService.GetByIdAsync(id);
        if (existingExercise == null)
        {
            return NotFound();
        }

        await _exerciseService.UpdateAsync(exercise);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var exercise = await _exerciseService.GetByIdAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }

        await _exerciseService.DeleteAsync(exercise);
        return NoContent();
    }
}
