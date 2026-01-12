namespace FizzBuzz;

public class UserInputProcessor
{
    private readonly IInputValidator _validator;

    public UserInputProcessor(IInputValidator validator)
    {
        _validator = validator;
    }

    public void ProcessUserInput(string? input)
    {
        IInputValidator.ValidationResult validationResult = _validator.Validate(input, out List<int> numbers);

        // Display result based on validation outcome
        switch (validationResult)
        {
            case IInputValidator.ValidationResult.Success:
                Console.WriteLine("\nFizzBuzz Results:");
                foreach (int number in numbers)
                {
                    string result = FizzBuzz.Convert(number);
                    Console.WriteLine(number + " -> " + result);
                }
                break;
            case IInputValidator.ValidationResult.NoInput:
                Console.WriteLine("Error: No input provided.");
                break;
            case IInputValidator.ValidationResult.InvalidFormat:
                Console.WriteLine("Error: Invalid number format detected. Please enter only whole numbers separated by commas (no letters, decimals, or special characters).");
                break;
            case IInputValidator.ValidationResult.OutOfRange:
                Console.WriteLine("Error: Number must be between 1 and 100.");
                break;
            case IInputValidator.ValidationResult.WrongCount:
                Console.WriteLine("Error: You must enter exactly 5 numbers.");
                break;
            default:
                Console.WriteLine($"Error: An unexpected validation error occurred. ValidationResult: {validationResult}");
                break;
        }
    }
}
