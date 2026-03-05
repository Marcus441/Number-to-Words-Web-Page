using api.Services.Validation;

namespace api.Services.Numeration;

public class NumberToWordsService(IInputValidator validator) : INumberToWordsService
{
  private static readonly string[] Units = ["", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE"];
  private static readonly string[] Teens = ["ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"];
  private static readonly string[] Tens = ["", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"];
  private static readonly string[] Scales = ["", "THOUSAND", "MILLION", "BILLION", "TRILLION"];
  private readonly IInputValidator _validator = validator;

  public string ConvertNumberToWords(string input)
  {
    if (!_validator.TryValidate(input, out decimal parsedAmount))
    {
      return string.Empty;
    }
    string sanitizedInput = input.Trim();

    if (parsedAmount == 0) return "ZERO DOLLARS AND ZERO CENTS";

    var parts = sanitizedInput.Split('.');
    string rawDollars = parts[0];
    string rawCents = parts.Length > 1 ? parts[1].PadRight(2, '0')[0..2] : "00";

    List<int> triplets = [];
    List<string> resultParts = [];

    for (int i = rawDollars.Length; i > 0; i -= 3)
    {
      int start = Math.Max(0, i - 3);
      int length = i - start;
      triplets.Add(int.Parse(rawDollars.Substring(start, length)));
    }

    for (int i = 0; i < triplets.Count; ++i)
    {
      int value = triplets[i];

      if (value > 0)
      {
        string words = ProcessTriplet(value);
        string scale = Scales[i];
        string groupResult = $"{words} {scale}".Trim();

        // This handles the edge case of "ONE HUNDRED MILLION AND FIVE THOUSAND" 
        // (made sure to include the extra AND when trailing numbers are less than 100) 
        if (i < triplets.Count - 1 && value < 100 && !groupResult.StartsWith("AND"))
        {
          groupResult = "AND " + groupResult;
        }

        resultParts.Insert(0, groupResult);
      }
    }

    string dollarWords = resultParts.Count > 0 ? string.Join(" ", resultParts) : "ZERO";

    int centsValue = int.Parse(rawCents);
    string centWords = centsValue > 0 ? ProcessTriplet(centsValue) : "ZERO";

    bool isExactlyOne = rawDollars.TrimStart('0') == "1"; //only trim the string to avoid potentially expensive cast? 

    string dollarLabel = isExactlyOne ? "DOLLAR" : "DOLLARS";

    string centLabel = (centsValue == 1) ? "CENT" : "CENTS";

    return $"{dollarWords} {dollarLabel} AND {centWords} {centLabel}";
  }

  private static string ProcessTriplet(int number)
  {
    string words = "";
    int hundreds = number / 100;
    int remainder = number % 100;

    if (hundreds > 0)
    {
      words += Units[hundreds] + " HUNDRED";

      if (remainder > 0) words += " AND ";
    }
    if (remainder > 10 && remainder < 20)
    {
      words += Teens[remainder - 11];
    }
    else
    {
      int tens = remainder / 10;
      int units = remainder % 10;

      if (tens >= 1)
      {
        words += Tens[tens];
        if (units > 0) words += "-" + Units[units];
      }
      else if (units > 0)
      {
        words += Units[units];
      }
    }
    return words;
  }
}
