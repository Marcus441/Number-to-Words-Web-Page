namespace api.Services.Numeration;

/// <summary>
/// Defines the contract for converting numerical currency values into their formal English word equivalents.
/// </summary>
public interface INumberToWordsService
{
    /// <summary>
    /// Converts a numeric string into its formal currency word representation.
    /// </summary>
    /// <param name="amount">The numerical string to convert (e.g., "123.45" or "100"). Supports values with up to two decimal places.</param>
    /// <returns>
    /// A string representing the currency in formal words (e.g., "ONE HUNDRED DOLLARS AND ZERO CENTS").
    /// Returns an empty string or null if the input format is invalid.
    /// </returns>
    string ConvertNumberToWords(string amount);
}
