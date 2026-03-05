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

    (string rawDollars, string rawCents) = SplitDollarsAndCents(sanitizedInput);


    List<int> triplets = ConvertToTripletsList(rawDollars);

    List<string> wordsList = BuildWordsListFromTriplets(triplets);


    string dollarsInWords = BuildDollarsString(wordsList, rawDollars);
    string centsInWords = BuildCentsString(rawCents);


    return $"{dollarsInWords} AND {centsInWords}";
  }

  private static string ProcessTriplet(int number)
  {
    string words = "";
    int hundreds = number / 100;
    int remainder = number % 100;

    if (hundreds > 0)
    {
      words += Units[hundreds] + " HUNDRED";

      if (remainder > 0)
        words += " AND ";
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
  private static (string rawDollars, string rawCents) SplitDollarsAndCents(string input)
  {
    var parts = input.Split('.');
    string rawDollars = parts[0];
    string rawCents = parts.Length > 1 ? parts[1].PadRight(2, '0')[0..2] : "00";
    return (rawDollars, rawCents);
  }

  private static List<int> ConvertToTripletsList(string rawIntegerString)
  {
    List<int> triplets = [];

    // build the string by moving left to right from the integer stream 
    // with a sliding window of 3
    for (int i = rawIntegerString.Length; i > 0; i -= 3)
    {
      int start = Math.Max(0, i - 3);
      int length = i - start;
      triplets.Add(int.Parse(rawIntegerString.Substring(start, length)));
    }
    return triplets;
  }

  private static List<string> BuildWordsListFromTriplets(List<int> tripletsList)
  {

    List<string> outputWordsList = [];
    for (int i = 0; i < tripletsList.Count; ++i)
    {
      int value = tripletsList[i];

      if (value > 0)
      {
        string words = ProcessTriplet(value);
        string scale = Scales[i];
        string groupResult = $"{words} {scale}".Trim();

        bool isTrailingSmallAmount = i < tripletsList.Count - 1 && value < 100;
        bool alreadyHasAnd = groupResult.StartsWith("AND");

        if (isTrailingSmallAmount && !alreadyHasAnd)
          groupResult = "AND " + groupResult;

        outputWordsList.Insert(0, groupResult);
      }
    }
    return outputWordsList;
  }



  private static string BuildDollarsString(List<string> wordsList, string rawDollars)
  {
    bool isExactlyOne = rawDollars.TrimStart('0') == "1"; //only trim the string to avoid potentially expensive cast? 
    string dollarWords = wordsList.Count > 0 ? string.Join(" ", wordsList) : "ZERO";
    string dollarLabel = isExactlyOne ? "DOLLAR" : "DOLLARS";
    return $"{dollarWords} {dollarLabel}";
  }

  private static string BuildCentsString(string rawCents)
  {

    int centsValue = int.Parse(rawCents);
    string centWords = centsValue > 0 ? ProcessTriplet(centsValue) : "ZERO";
    string centLabel = (centsValue == 1) ? "CENT" : "CENTS";
    return $"{centWords} {centLabel}";
  }


}
