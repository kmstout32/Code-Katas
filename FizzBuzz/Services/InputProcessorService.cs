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

    public virtual FizzBuzzModel ProcessSingleNumber(int number)
    {
        FizzBuzzModel model = new FizzBuzzModel();
        model.ValidationResult = _validator.ValidateSingle(number);

        if (model.IsSuccess)
        {
            model.Numbers.Add(number);
            model.ConvertedResults.Add(_converter.Convert(number));
        }

        return model;
    }

    public virtual FizzBuzzModel ProcessBatch(int[]? numbers)
    {
        string input = numbers != null && numbers.Length > 0
            ? string.Join(",", numbers)
            : string.Empty;
        return ProcessNumberString(input);
    }

    public virtual FizzBuzzModel ProcessNumberString(string? input)
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
