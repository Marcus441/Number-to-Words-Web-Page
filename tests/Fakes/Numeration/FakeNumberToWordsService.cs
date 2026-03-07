namespace tests.Fakes.Numeration;

using api.Services.Numeration;

public class FakeNumberToWordsService : INumberToWordsService
{
    public string WordsToReturn { get; set; } = "DEFAULT WORD";

    public string ConvertNumberToWords(string amount) => WordsToReturn;
}
