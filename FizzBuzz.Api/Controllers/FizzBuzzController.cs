using Microsoft.AspNetCore.Mvc;
using FizzBuzz.Services;
using FizzBuzz.Models;
using FizzBuzz.Api.Models;

namespace FizzBuzz.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FizzBuzzController : ControllerBase
{
    private readonly InputProcessorService _processorService;

    public FizzBuzzController(InputProcessorService processorService)
    {
        _processorService = processorService;
    }

    [HttpGet("{number}")]
    public ActionResult<FizzBuzzResponse> Convert(int number)
    {
        var model = _processorService.ProcessSingleNumber(number);

        if (!model.IsSuccess)
        {
            return BadRequest(new ErrorResponse { Error = model.GetErrorMessage() });
        }

        return Ok(new FizzBuzzResponse { Number = number, Result = model.ConvertedResults[0] });
    }

    [HttpPost]
    public ActionResult<FizzBuzzBatchResponse> ConvertBatch([FromBody] FizzBuzzBatchRequest request)
    {
        var model = _processorService.ProcessBatch(request?.Numbers);

        if (!model.IsSuccess)
        {
            return BadRequest(new ErrorResponse { Error = model.GetErrorMessage() });
        }

        var results = model.Numbers.Zip(model.ConvertedResults, (num, result) => new FizzBuzzResult
        {
            Number = num,
            Result = result
        }).ToList();

        return Ok(new FizzBuzzBatchResponse { Results = results });
    }
}
