namespace api.Utils;

/// <summary>
/// Class responsible for managing environment variables
/// </summary>
class EnvLoader
{
    /// <summary>
    /// Reads a local environment file and sets the parsed key-value pairs
    /// as process-level environment variables.
    /// </summary>
    /// <param name="filepath">The absolute or relative path to the .env file.</param>
    /// <exception cref="FileNotFoundException">Thrown when the specified file does not exist.</exception>
    /// <remarks>
    /// This method supports basic .env syntax, including:
    /// - Ignoring empty lines and comments starting with '#'.
    /// - Handling inline comments.
    /// - Stripping optional quotes around values.
    /// </remarks>
    public static void Load(string filepath)
    {
        if (!File.Exists(filepath))
            throw new FileNotFoundException($"env file at {filepath} not found");

        foreach (string line in File.ReadAllLines(filepath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
                continue;
            var expression = line.Split('=', 2);
            if (expression.Length != 2)
                continue;
            var key = expression[0].Trim();
            var rawValue = expression[1].Trim();

            if (rawValue.Contains('#'))
                rawValue = rawValue.Split('#')[0].Trim();

            string value = rawValue.Trim('"', '\'');

            Environment.SetEnvironmentVariable(key, value);
        }
    }
}

