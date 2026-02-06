using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FizzBuzz.Api.Controllers;
using FizzBuzz.Api.Models;
using FizzBuzz.Services;
using FizzBuzz.Models;
using FizzBuzz.Validators;

namespace FizzBuzz.Api.Tests.UnitTests.ControllerTests;

[TestFixture]
public class FizzBuzzControllerTests
{
    private Mock<InputProcessorService> _mockProcessorService;
    private Mock<ILogger<FizzBuzzController>> _mockLogger;
    private FizzBuzzController _controller;

    [SetUp]
    public void Setup()
    {
        _mockProcessorService = new Mock<InputProcessorService>(null!, null!, null!);
        _mockLogger = new Mock<ILogger<FizzBuzzController>>();
        _controller = new FizzBuzzController(_mockProcessorService.Object, _mockLogger.Object);
    }

    [Test]
    public void Convert_ValidNumber_ReturnsOkResult()
    {
        var model = new FizzBuzzModel
        {
            ValidationResult = InputValidator.ValidationResult.Success,
            Numbers = new List<int> { 15 },
            ConvertedResults = new List<string> { "FizzBuzz" }
        };
        _mockProcessorService.Setup(p => p.ProcessSingleNumber(15)).Returns(model);

        var response = (_controller.Convert(15).Result as OkObjectResult)!.Value as FizzBuzzResponse;

        Assert.That(response!.Number, Is.EqualTo(15));
        Assert.That(response.Result, Is.EqualTo("FizzBuzz"));
    }

    [TestCase(0)]
    [TestCase(101)]
    [Test]
    public void Convert_InvalidNumber_Returns_BadRequest(int number)
    {
        var model = new FizzBuzzModel
        {
            ValidationResult = InputValidator.ValidationResult.OutOfRange
        };
        _mockProcessorService.Setup(p => p.ProcessSingleNumber(number)).Returns(model);

        Assert.That(_controller.Convert(number).Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void ConvertBatch_ValidNumbers_Returns_OkResult()
    {
        var model = new FizzBuzzModel
        {
            ValidationResult = InputValidator.ValidationResult.Success,
            Numbers = new List<int> { 1, 3, 5, 15, 30 },
            ConvertedResults = new List<string> { "1", "Fizz", "Buzz", "FizzBuzz", "FizzBuzz" }
        };
        _mockProcessorService.Setup(p => p.ProcessBatch(It.IsAny<int[]>())).Returns(model);

        var response = (_controller.ConvertBatch(new FizzBuzzBatchRequest { Numbers = new[] { 1, 3, 5, 15, 30 } })
            .Result as OkObjectResult)!.Value as FizzBuzzBatchResponse;

        Assert.That(response!.Results, Has.Count.EqualTo(5));
        Assert.That(response.Results[0].Result, Is.EqualTo("1"));
    }

    [Test]
    public void ConvertBatch_NullRequest_Returns_BadRequest()
    {
        var model = new FizzBuzzModel
        {
            ValidationResult = InputValidator.ValidationResult.NoInput
        };
        _mockProcessorService.Setup(p => p.ProcessBatch(It.IsAny<int[]>())).Returns(model);

        Assert.That(_controller.ConvertBatch(null!).Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void ConvertBatch_EmptyArray_Returns_BadRequest()
    {
        var model = new FizzBuzzModel
        {
            ValidationResult = InputValidator.ValidationResult.NoInput
        };
        _mockProcessorService.Setup(p => p.ProcessBatch(It.IsAny<int[]>())).Returns(model);

        Assert.That(_controller.ConvertBatch(new FizzBuzzBatchRequest { Numbers = Array.Empty<int>() })
            .Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void ConvertBatch_WrongCount_Returns_BadRequest()
    {
        var model = new FizzBuzzModel
        {
            ValidationResult = InputValidator.ValidationResult.WrongCount
        };
        _mockProcessorService.Setup(p => p.ProcessBatch(It.IsAny<int[]>())).Returns(model);

        Assert.That(_controller.ConvertBatch(new FizzBuzzBatchRequest { Numbers = new[] { 1, 2, 3 } })
            .Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void ConvertBatch_OutOfRange_Returns_BadRequest()
    {
        var model = new FizzBuzzModel
        {
            ValidationResult = InputValidator.ValidationResult.OutOfRange
        };
        _mockProcessorService.Setup(p => p.ProcessBatch(It.IsAny<int[]>())).Returns(model);

        Assert.That(_controller.ConvertBatch(new FizzBuzzBatchRequest { Numbers = new[] { 1, 2, 3, 4, 150 } })
            .Result, Is.InstanceOf<BadRequestObjectResult>());
    }
}
