using Microsoft.AspNetCore.Mvc;
using FizzBuzz.Services;
using FizzBuzz.Validators;
using FizzBuzz.Models;
using FizzBuzz.Api.Models;

namespace FizzBuzz.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FizzBuzzController : ControllerBase
{
    private readonly FizzBuzzConverterService _converterService;
    private readonly InputValidator _inputValidator;
    private readonly ILogger<FizzBuzzController> _logger;

    public FizzBuzzController(
        FizzBuzzConverterService converterService,
        InputValidator inputValidator,
        ILogger<FizzBuzzController> logger)
    {
        _converterService = converterService;
        _inputValidator = inputValidator;
        _logger = logger;
    }

    [HttpGet("{number}")]
    public ActionResult<FizzBuzzResponse> Convert(int number)
    {
        var validationResult = _inputValidator.ValidateSingle(number);

        if (validationResult != InputValidator.ValidationResult.Success)
        {
            _logger.LogWarning("Validation failed for single number: {Number}, Result: {ValidationResult}",
                number, validationResult);
            var model = new FizzBuzzModel { ValidationResult = validationResult };
            return BadRequest(new ErrorResponse { Error = model.GetErrorMessage() });
        }

        string result = _converterService.Convert(number);
        return Ok(new FizzBuzzResponse { Number = number, Result = result });
    }

    [HttpPost]
    public ActionResult<FizzBuzzBatchResponse> ConvertBatch([FromBody] FizzBuzzBatchRequest request)
    {
        var validationResult = _inputValidator.Validate(request?.Numbers, out List<int> validatedNumbers);

        if (validationResult != InputValidator.ValidationResult.Success)
        {
            _logger.LogWarning("Validation failed for batch request: Result: {ValidationResult}, Count: {Count}",
                validationResult, request?.Numbers?.Length ?? 0);
            var model = new FizzBuzzModel { ValidationResult = validationResult };
            return BadRequest(new ErrorResponse { Error = model.GetErrorMessage() });
        }

        var convertedResults = _converterService.ConvertBatch(validatedNumbers);
        var results = validatedNumbers.Zip(convertedResults, (num, result) => new FizzBuzzResult
        {
            Number = num,
            Result = result,
            Error = null
        }).ToList();

        return Ok(new FizzBuzzBatchResponse { Results = results });
    }
}
