using api.Services.Numeration;
using api.Services.Validation.NumberToWordsValidation;
namespace tests.Services;

public class NumberToWordsServiceTests
{
    private readonly NumberToWordsService _service;

    public NumberToWordsServiceTests()
    {
        var validator = new NumberToWordsValidator();
        _service = new NumberToWordsService(validator);
    }

    [Theory]
    //general use
    [InlineData("1", "ONE DOLLAR AND ZERO CENTS")]
    [InlineData("21", "TWENTY-ONE DOLLARS AND ZERO CENTS")]
    [InlineData("21.01", "TWENTY-ONE DOLLARS AND ONE CENT")]
    [InlineData("101", "ONE HUNDRED AND ONE DOLLARS AND ZERO CENTS")]
    [InlineData("123.45", "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS")]
    [InlineData("13221.01", "THIRTEEN THOUSAND TWO HUNDRED AND TWENTY-ONE DOLLARS AND ONE CENT")]
    //boundary values
    [InlineData("0", "ZERO DOLLARS AND ZERO CENTS")]
    [InlineData("0.01", "ZERO DOLLARS AND ONE CENT")]
    [InlineData("0.99", "ZERO DOLLARS AND NINETY-NINE CENTS")]
    [InlineData("999999999.99", "NINE HUNDRED AND NINETY-NINE MILLION NINE HUNDRED AND NINETY-NINE THOUSAND NINE HUNDRED AND NINETY-NINE DOLLARS AND NINETY-NINE CENTS")]

    //formatting variations
    [InlineData("  100  ", "ONE HUNDRED DOLLARS AND ZERO CENTS")]
    [InlineData("000123", "ONE HUNDRED AND TWENTY-THREE DOLLARS AND ZERO CENTS")]

    //edge case: invalid characters 
    [InlineData("abc", "")]
    [InlineData("12.34.56", "")]
    [InlineData("-15", "")]
    public void ConvertNumberToWords_ShouldReturnExpectedWords(string input, string expected)
    {
        var result = _service.ConvertNumberToWords(input);

        Assert.Equal(expected, result);
    }
}
