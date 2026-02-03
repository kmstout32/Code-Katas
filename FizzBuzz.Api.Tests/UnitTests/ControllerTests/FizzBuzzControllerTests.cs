using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FizzBuzz.Api.Controllers;
using FizzBuzz.Api.Models;
using FizzBuzz.Services;
using FizzBuzz.Validators;

namespace FizzBuzz.Api.Tests.UnitTests.ControllerTests;

[TestFixture]
public class FizzBuzzControllerTests
{
    private Mock<FizzBuzzConverterService> _mockConverterService;
    private Mock<InputValidator> _mockValidator;
    private Mock<ILogger<FizzBuzzController>> _mockLogger;
    private FizzBuzzController _controller;

    [SetUp]
    public void Setup()
    {
        _mockConverterService = new Mock<FizzBuzzConverterService>();
        _mockValidator = new Mock<InputValidator>();
        _mockLogger = new Mock<ILogger<FizzBuzzController>>();
        _controller = new FizzBuzzController(
            _mockConverterService.Object,
            _mockValidator.Object,
            _mockLogger.Object);
    }

    [Test]
    public void Convert_ValidNumber_ReturnsOkResult()
    {
        _mockValidator.Setup(v => v.ValidateSingle(15))
            .Returns(InputValidator.ValidationResult.Success);
        _mockConverterService.Setup(c => c.Convert(15)).Returns("FizzBuzz");

        var response = (_controller.Convert(15).Result as OkObjectResult)!.Value as FizzBuzzResponse;

        Assert.That(response!.Number, Is.EqualTo(15));
        Assert.That(response.Result, Is.EqualTo("FizzBuzz"));
    }

    [TestCase(0)]
    [TestCase(101)]
    [Test]
    public void Convert_InvalidNumber_Returns_BadRequest(int number)
    {
        _mockValidator.Setup(v => v.ValidateSingle(number))
            .Returns(InputValidator.ValidationResult.OutOfRange);

        Assert.That(_controller.Convert(number).Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void ConvertBatch_ValidNumbers_Returns_OkResult()
    {
        var validatedNumbers = new List<int> { 1, 3, 5, 15, 30 };
        _mockValidator.Setup(v => v.Validate(It.IsAny<int[]>(), out validatedNumbers))
            .Returns(InputValidator.ValidationResult.Success);
        _mockConverterService.Setup(c => c.ConvertBatch(It.IsAny<List<int>>()))
            .Returns(new List<string> { "1", "Fizz", "Buzz", "FizzBuzz", "FizzBuzz" });

        var response = (_controller.ConvertBatch(new FizzBuzzBatchRequest { Numbers = new[] { 1, 3, 5, 15, 30 } })
            .Result as OkObjectResult)!.Value as FizzBuzzBatchResponse;

        Assert.That(response!.Results, Has.Count.EqualTo(5));
        Assert.That(response.Results[0].Result, Is.EqualTo("1"));
    }

    [Test]
    public void ConvertBatch_NullRequest_Returns_BadRequest()
    {
        var emptyList = new List<int>();
        _mockValidator.Setup(v => v.Validate(It.IsAny<int[]>(), out emptyList))
            .Returns(InputValidator.ValidationResult.NoInput);

        Assert.That(_controller.ConvertBatch(null).Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void ConvertBatch_EmptyArray_Returns_BadRequest()
    {
        var emptyList = new List<int>();
        _mockValidator.Setup(v => v.Validate(It.IsAny<int[]>(), out emptyList))
            .Returns(InputValidator.ValidationResult.NoInput);

        Assert.That(_controller.ConvertBatch(new FizzBuzzBatchRequest { Numbers = Array.Empty<int>() })
            .Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void ConvertBatch_WrongCount_Returns_BadRequest()
    {
        var emptyList = new List<int>();
        _mockValidator.Setup(v => v.Validate(It.IsAny<int[]>(), out emptyList))
            .Returns(InputValidator.ValidationResult.WrongCount);

        Assert.That(_controller.ConvertBatch(new FizzBuzzBatchRequest { Numbers = new[] { 1, 2, 3 } })
            .Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void ConvertBatch_OutOfRange_Returns_BadRequest()
    {
        var emptyList = new List<int>();
        _mockValidator.Setup(v => v.Validate(It.IsAny<int[]>(), out emptyList))
            .Returns(InputValidator.ValidationResult.OutOfRange);

        Assert.That(_controller.ConvertBatch(new FizzBuzzBatchRequest { Numbers = new[] { 1, 2, 3, 4, 150 } })
            .Result, Is.InstanceOf<BadRequestObjectResult>());
    }
}
