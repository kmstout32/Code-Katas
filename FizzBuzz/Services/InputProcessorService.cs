using FizzBuzz.Models;
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

    public virtual FizzBuzzModel ProcessUserInput(string? input)
    {
        FizzBuzzModel model = new FizzBuzzModel();
        model.ValidationResult = _validator.Validate(input, out List<int> numbers);
        model.Numbers = numbers;

        if (model.IsSuccess)
        {
            foreach (int number in numbers)
            {
                string result = _converter.Convert(number);
                model.ConvertedResults.Add(result);
            }
        }

        return model;
    }
}
