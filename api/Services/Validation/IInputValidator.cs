namespace api.Services.Validation;

/// <summary>
/// Defines a contract for validating and parsing raw string inputs into numerical currency values.
/// </summary>
public interface IInputValidator
{
    /// <summary>
    /// Attempts to validate whether a string is a valid numerical currency amount and parses it.
    /// </summary>
    /// <param name="input">The raw string input to validate (e.g., "123.45").</param>
    /// <param name="amount">
    /// When this method returns, contains the parsed <see cref="decimal"/> value if validation succeeded,
    /// or zero if validation failed.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the input is a valid numerical format;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    bool TryValidate(string input, out decimal amount);
}
