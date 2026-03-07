namespace api.Services.Validation;

public class NumberToWordsValidator : IInputValidator
{
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
