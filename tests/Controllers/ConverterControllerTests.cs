using Microsoft.AspNetCore.Mvc;
using api.Controllers;
using test.api.fake.Numeration;
using api.DTO;

using Xunit;

public class ConverterControllerTests
{
  [Fact]
  public void Convert_ReturnsOk_WithManualFake()
  {
    // Arrange
    var fakeService = new FakeNumberToWordsService
    {
      WordsToReturn = "ONE DOLLAR"
    };

    var controller = new ConverterController(fakeService);

    // Act
    var result = controller.Convert("1") as OkObjectResult;

    // Assert
    var response = Assert.IsType<ConversionResponse>(result?.Value);

    Assert.Equal("ONE DOLLAR", response.Words);
  }

  [Fact]
  public void Convert_ReturnsBadRequest_WhenInputIsInvalid()
  {
    // Arrange
    // We simulate a scenario where the service cannot process the input
    var fakeService = new FakeNumberToWordsService { WordsToReturn = "" };
    var controller = new ConverterController(fakeService);

    // Act
    var result = controller.Convert("invalid-input") as BadRequestObjectResult;

    // Assert
    Assert.NotNull(result);
    Assert.Equal(400, result.StatusCode);
  }
}
