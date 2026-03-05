using api.Services.Numeration;
using api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api")]
public class ConverterController(INumberToWordsService converterService) : ControllerBase
{
  [HttpGet("convert")]
  public IActionResult Convert([FromQuery] string amount)
  {
    var result = converterService.ConvertNumberToWords(amount);
    if (string.IsNullOrWhiteSpace(result))
    {
      return BadRequest(new { error = "Invalid amount provided." });
    }

    return Ok(new ConversionResponse { Words = result });
  }
}
