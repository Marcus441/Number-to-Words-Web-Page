namespace api.Services.Validation;

public interface IInputValidator
{
    bool TryValidate(string input, out decimal amount);
}

