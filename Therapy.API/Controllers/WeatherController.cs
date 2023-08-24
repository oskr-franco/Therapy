using Microsoft.AspNetCore.Mvc;

public class WeatherForecastController : ApiController
{
    [HttpGet]
    public IActionResult Get()
    {
      var myString = "Hello World";
      return Ok(myString);
    }
}