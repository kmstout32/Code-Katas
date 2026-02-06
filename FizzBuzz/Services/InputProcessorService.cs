using FizzBuzz.Models;
using FizzBuzz.Validators;
using Microsoft.Extensions.Logging;

namespace FizzBuzz.Services;

public class InputProcessorService
{
    private readonly InputValidator _validator;
    private readonly FizzBuzzConverterService _converter;
    private readonly ILogger<InputProcessorService> _logger;

    public InputProcessorService(InputValidator validator, FizzBuzzConverterService converter, ILogger<InputProcessorService> logger)
    {
        _validator = validator;
        _converter = converter;
        _logger = logger;
    }

    public virtual FizzBuzzModel ProcessSingleNumber(int number)
    {
        _logger.LogDebug("Processing single number: {Number}", number);

        FizzBuzzModel model = new FizzBuzzModel();
        model.ValidationResult = _validator.ValidateSingle(number);

        if (model.IsSuccess)
        {
            model.Numbers.Add(number);
            model.ConvertedResults.Add(_converter.Convert(number));
        }
        else
        {
            _logger.LogWarning("Single number validation failed for {Number}: {ValidationResult}",
                number, model.ValidationResult);
        }

        return model;
    }

    public virtual FizzBuzzModel ProcessBatch(int[]? numbers)
    {
        _logger.LogDebug("Processing batch of {Count} numbers", numbers?.Length ?? 0);

        string input = numbers != null && numbers.Length > 0
            ? string.Join(",", numbers)
            : string.Empty;
        return ProcessNumberString(input);
    }

    public virtual FizzBuzzModel ProcessNumberString(string? input)
    {
        _logger.LogDebug("Processing number string: {Input}", input ?? "(null)");

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
        else
        {
            _logger.LogWarning("Validation failed: {ValidationResult}", model.ValidationResult);
        }

        return model;
    }
}
