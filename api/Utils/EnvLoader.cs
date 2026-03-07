namespace api.Utils;

class EnvLoader
{

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

