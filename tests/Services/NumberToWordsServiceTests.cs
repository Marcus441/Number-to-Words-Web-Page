using Xunit;
using api.Services.Numeration;

namespace tests.Services;

public class NumberToWordsServiceTests
{
  private readonly NumberToWordsService _service;

  public NumberToWordsServiceTests()
  {
    _service = new NumberToWordsService();
  }

  [Theory]
  [InlineData("1", "ONE DOLLAR AND ZERO CENTS")]
  [InlineData("21", "TWENTY-ONE DOLLARS AND ZERO CENTS")]
  [InlineData("101", "ONE HUNDRED AND ONE DOLLARS AND ZERO CENTS")]
  [InlineData("13221.01", "THIRTEEN THOUSAND TWO HUNDRED AND TWENTY-ONE DOLLARS AND ONE CENT")]
  public void ConvertNumberToWords_ShouldReturnExpectedWords(string input, string expected)
  {
    var result = _service.ConvertNumberToWords(input);

    Assert.Equal(expected, result);
  }
}
