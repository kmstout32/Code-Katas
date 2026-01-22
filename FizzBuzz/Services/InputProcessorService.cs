using FizzBuzz.Validators;

namespace FizzBuzz.Services;

public class InputProcessorService
{
    private readonly InputValidator _validator;
    private readonly FizzBuzzConverterService _converter;


    public InputProcessorService(InputValidator validator, FizzBuzzConverterService converter)
    {
        _validator = validator;
        _converter = converter;
    }

    public virtual void ProcessUserInput(string? input)
    {
        InputValidator.ValidationResult validationResult = _validator.Validate(input, out List<int> numbers);

        // Display result based on validation outcome
        switch (validationResult)
        {
            case InputValidator.ValidationResult.Success:
                Console.WriteLine("\nFizzBuzz Results:");
                foreach (int number in numbers)
                {
                    string result = _converter.Convert(number);
                    Console.WriteLine(number + " -> " + result);
                }
                break;
            case InputValidator.ValidationResult.NoInput:
                Console.WriteLine("Error: No input provided.");
                break;
            case InputValidator.ValidationResult.InvalidFormat:
                Console.WriteLine("Error: Invalid number format detected. Please enter only whole numbers separated by commas (no letters, decimals, or special characters).");
                break;
            case InputValidator.ValidationResult.OutOfRange:
                Console.WriteLine("Error: Number must be between 1 and 100.");
                break;
            case InputValidator.ValidationResult.WrongCount:
                Console.WriteLine("Error: You must enter exactly 5 numbers.");
                break;
            default:
                Console.WriteLine($"Error: An unexpected validation error occurred. ValidationResult: {validationResult}");
                break;
        }
    }
}
