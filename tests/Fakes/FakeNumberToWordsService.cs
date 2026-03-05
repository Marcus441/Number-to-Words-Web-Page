using api.Services.Numeration;
namespace tests.Fakes.Numeration;

public class FakeNumberToWordsService : INumberToWordsService
{
  public string WordsToReturn { get; set; } = "DEFAULT WORD";

  public string ConvertNumberToWords(string amount)
  {
    return WordsToReturn;
  }
}

