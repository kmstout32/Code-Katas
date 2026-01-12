namespace FizzBuzz;

public interface IInputValidator
{
    public enum ValidationResult
    {
        Success,
        NoInput,
        InvalidFormat,
        OutOfRange,
        WrongCount
    }

    ValidationResult Validate(string? input, out List<int> numbers);
}
