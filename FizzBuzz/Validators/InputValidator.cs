namespace FizzBuzz.Validators;

public class InputValidator
{
    public enum ValidationResult
    {
        Success,
        NoInput,
        InvalidFormat,
        OutOfRange,
        WrongCount
    }

    public virtual ValidationResult Validate(string? input, out List<int> numbers)
    {
        numbers = new List<int>();

        if (input == null || string.IsNullOrWhiteSpace(input))
        {
            return ValidationResult.NoInput;
        }

        string[] numberStrings = input.Split(',');

        if (numberStrings.Length != 5)
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
            if (number < 1 || number > 100)
            {
                return ValidationResult.OutOfRange;
            }
        }

        return ValidationResult.Success;
    }
}
