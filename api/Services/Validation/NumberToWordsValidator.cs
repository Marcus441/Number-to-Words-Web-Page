using api.Services.Validation;

/// <summary>
/// Implementation of the input validator ensuring currency values are within a supported
/// range and follow valid numerical formats.
/// </summary>
public class NumberToWordsValidator : IInputValidator
{
    /// <summary>
    /// Validates the input string against currency format rules and a maximum limit of 999,999,999.99.
    /// </summary>
    public bool TryValidate(string input, out decimal amount)
    {
        if (!decimal.TryParse(input, System.Globalization.NumberStyles.Any, null, out amount))
            return false;

        if (amount < 0)
            return false;

        // prevent overflow
        if (amount > 999_999_999.99m)
            return false;

        return true;
    }
}
