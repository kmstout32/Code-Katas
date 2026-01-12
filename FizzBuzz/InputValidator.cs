namespace FizzBuzz;

public class InputValidator : IInputValidator
{
    public IInputValidator.ValidationResult Validate(string? input, out List<int> numbers)
    {
        numbers = new List<int>();

        // Guard: null or empty input
        if (input == null || string.IsNullOrWhiteSpace(input))
        {
            return IInputValidator.ValidationResult.NoInput;
        }

        string[] numberStrings = input.Split(',');

        // Parse all numbers
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
            return IInputValidator.ValidationResult.InvalidFormat;
        }

        // FIRST: Check all numbers are in range 1-100
        foreach (int number in numbers)
        {
            if (number < 1 || number > 100)
            {
                return IInputValidator.ValidationResult.OutOfRange;
            }
        }

        // Check count is exactly 5
        if (numbers.Count != 5)
        {
            return IInputValidator.ValidationResult.WrongCount;
        }

        return IInputValidator.ValidationResult.Success;
    }
}
