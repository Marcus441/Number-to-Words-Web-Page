namespace api.DTOs;

/// <summary>
/// Represents the data transfer object returned upon a successful currency-to-words conversion.
/// </summary>
public class ConversionResponse
{
    /// <summary>
    /// Gets or sets the formal English word representation of the converted numerical value.
    /// </summary>
    /// <example>ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS</example>
    public string Words { get; set; } = string.Empty;
}
