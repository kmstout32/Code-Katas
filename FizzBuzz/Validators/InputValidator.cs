namespace FizzBuzz.Validators;

public class InputValidator
{
    public const int MIN_VALUE = 1;
    public const int MAX_VALUE = 100;
    public const int REQUIRED_BATCH_SIZE = 5;

    public enum ValidationResult
    {
        Success,
        NoInput,
        InvalidFormat,
        OutOfRange,
        WrongCount
    }

    public virtual ValidationResult ValidateSingle(int number)
    {
        if (number < MIN_VALUE || number > MAX_VALUE)
        {
            return ValidationResult.OutOfRange;
        }

        return ValidationResult.Success;
    }

    public virtual ValidationResult Validate(string? input, out List<int> numbers)
    {
        numbers = new List<int>();

        if (input == null || string.IsNullOrWhiteSpace(input))
        {
            return ValidationResult.NoInput;
        }

        string[] numberStrings = input.Split(',');

        if (numberStrings.Length != REQUIRED_BATCH_SIZE)
        {
            return ValidationResult.WrongCount;
        }

        try
        {
            foreach (string numberString in numberStrings)
            {
                string trimmedNumber = numberString.Trim();
                int number = int.Parse(trimmedNumber);
                numbers.Add(number);
            }
        }
        catch (FormatException)
        {
            return ValidationResult.InvalidFormat;
        }

        foreach (int number in numbers)
        {
            if (number < MIN_VALUE || number > MAX_VALUE)
            {
                return ValidationResult.OutOfRange;
            }
        }

        return ValidationResult.Success;
    }
}
