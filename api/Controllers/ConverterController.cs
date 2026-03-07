using api.Services.Numeration;
using api.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    /// <summary>
    /// Controller for the main conversion endpoint
    /// </summary>
    [ApiController]
    [Route("api")]
    public class ConverterController(INumberToWordsService converterService) : ControllerBase
    {

        /// <summary>
        /// Processes a numerical string and returns its representation in currency words.
        /// </summary>
        /// <param name="amount">The numerical string to convert. Supports up to 2 decimal places.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a <see cref="ConversionResponse"/>
        /// if successful, or a 400 Bad Request if the input format is invalid.
        /// </returns>
        /// <response code="200">Returns the converted word string.</response>
        /// <response code="400">If the amount is null, empty, or not a valid numerical currency format.</response>
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
}
