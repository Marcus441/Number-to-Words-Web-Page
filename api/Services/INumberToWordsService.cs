namespace api.Services.Numeration;

public interface INumberToWordsService
{
    /// <summary>
    /// Converts a numeric string (Ex: "123.45") into currency words.
    /// </summary>
    string ConvertNumberToWords(string amount);
}
