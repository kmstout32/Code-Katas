namespace FizzBuzz;

public class UserInputProcessor
{
    public enum ValidationResult
    {
        Success,
        NoInput,
        InvalidFormat,
        OutOfRange,
        WrongCount
    }

    public static void ProcessUserInput(string? input)
    {
        ValidationResult validationResult = ValidationResult.Success;
        List<int> numbers = new List<int>();

        // Check: null or empty input
        if (input == null || string.IsNullOrWhiteSpace(input))
        {
            validationResult = ValidationResult.NoInput;
        }
        else
        {
            try
            {
                string[] numberStrings = input.Split(',');

                foreach (string numberString in numberStrings)
                {
                    string trimmedNumber = numberString.Trim();
                    int number = int.Parse(trimmedNumber);

                    // FIRST: Validate number is in the range 1-100
                    if (number < 1 || number > 100)
                    {
                        validationResult = ValidationResult.OutOfRange;
                        break;
                    }

                    numbers.Add(number);
                }

                // Only check count if no range error was found
                if (validationResult == ValidationResult.Success && numberStrings.Length != 5)
                {
                    validationResult = ValidationResult.WrongCount;
                }
            }
            catch (FormatException)
            {
                validationResult = ValidationResult.InvalidFormat;
            }
        }

        // Display result based on validation outcome
        switch (validationResult)
        {
            case ValidationResult.Success:
                Console.WriteLine("\nFizzBuzz Results:");
                foreach (int number in numbers)
                {
                    string result = FizzBuzz.Convert(number);
                    Console.WriteLine(number + " -> " + result);
                }
                break;
            case ValidationResult.NoInput:
                Console.WriteLine("Error: No input provided.");
                break;
            case ValidationResult.InvalidFormat:
                Console.WriteLine("Error: Invalid number format detected. Please enter only whole numbers separated by commas (no letters, decimals, or special characters).");
                break;
            case ValidationResult.OutOfRange:
                Console.WriteLine("Error: Number must be between 1 and 100.");
                break;
            case ValidationResult.WrongCount:
                Console.WriteLine("Error: You must enter exactly 5 numbers.");
                break;
            default:
                Console.WriteLine($"Error: An unexpected validation error occurred. ValidationResult: {validationResult}");
                break;
        }
    }
}
