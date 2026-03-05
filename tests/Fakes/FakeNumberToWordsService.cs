using api.Services.Numeration;
namespace test.api.fake.Numeration;

public class FakeNumberToWordsService : INumberToWordsService
{
  public string WordsToReturn { get; set; } = "DEFAULT WORD";

  public string ConvertNumberToWords(string amount)
  {
    return WordsToReturn;
  }
}

