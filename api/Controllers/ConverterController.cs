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
    return Ok(new ConversionResponse { Words = result });
  }
}
