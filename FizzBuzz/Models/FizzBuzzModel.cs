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
                InputValidator.ValidationResult.NoInput => "Error: No input provided.",
                InputValidator.ValidationResult.InvalidFormat => "Error: Invalid number format detected. Please enter only whole numbers separated by commas (no letters, decimals, or special characters).",
                InputValidator.ValidationResult.OutOfRange => "Error: Number must be between 1 and 100.",
                InputValidator.ValidationResult.WrongCount => "Error: You must enter exactly 5 numbers.",
                InputValidator.ValidationResult.Success => string.Empty,
                _ => $"Error: An unexpected validation error occurred. ValidationResult: {ValidationResult}"
            };
        }
    }
}
