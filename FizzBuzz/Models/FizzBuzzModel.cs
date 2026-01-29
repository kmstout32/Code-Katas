using FizzBuzz.Validators;

namespace FizzBuzz.Models
{
    public class FizzBuzzModel
    {
        public InputValidator.ValidationResult ValidationResult { get; set; }
        public List<int> Numbers { get; set; }
        public List<string> ConvertedResults { get; set; }
        public bool IsSuccess => ValidationResult == InputValidator.ValidationResult.Success;

        public FizzBuzzModel()
        {
            Numbers = new List<int>();
            ConvertedResults = new List<string>();
        }

        public string GetErrorMessage()
        {
            return ValidationResult switch
            {
                InputValidator.ValidationResult.NoInput => " This is my model . Error: No input provided.",
                InputValidator.ValidationResult.InvalidFormat => " This is my model Error: Invalid number format detected. Please enter only whole numbers separated by commas (no letters, decimals, or special characters).",
                InputValidator.ValidationResult.OutOfRange => "This is my model Error: Number must be between 1 and 100.",
                InputValidator.ValidationResult.WrongCount => "This is my model Error: You must enter exactly 5 numbers.",
                InputValidator.ValidationResult.Success => string.Empty,
                _ => $"Error: An unexpected validation error occurred. ValidationResult: {ValidationResult}"
            };
        }
    }
}
