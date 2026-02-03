using Microsoft.AspNetCore.Mvc;
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
    private FizzBuzzController _controller;

    [SetUp]
    public void Setup()
    {
        _mockConverterService = new Mock<FizzBuzzConverterService>();
        _mockValidator = new Mock<InputValidator>();
        _controller = new FizzBuzzController(_mockConverterService.Object, _mockValidator.Object);
    }

    [Test]
    public void Convert_ValidNumber_ReturnsOkResult()
    {
        int number = 15;
        string expectedResult = "FizzBuzz";
        _mockValidator.Setup(v => v.ValidateSingle(number))
            .Returns(InputValidator.ValidationResult.Success);
        _mockConverterService.Setup(c => c.Convert(number))
            .Returns(expectedResult);

        var result = _controller.Convert(number);

        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        var response = okResult.Value as FizzBuzzResponse;
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Number, Is.EqualTo(number));
        Assert.That(response.Result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Convert_NumberBelowRange_ReturnsBadRequest()
    {
        int number = 0;
        _mockValidator.Setup(v => v.ValidateSingle(number))
            .Returns(InputValidator.ValidationResult.OutOfRange);

        var result = _controller.Convert(number);

        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        var errorResponse = badRequestResult.Value as ErrorResponse;
        Assert.That(errorResponse, Is.Not.Null);
        Assert.That(errorResponse.Error, Is.Not.Empty);
    }

    [Test]
    public void Convert_NumberAboveRange_ReturnsBadRequest()
    {
        int number = 101;
        _mockValidator.Setup(v => v.ValidateSingle(number))
            .Returns(InputValidator.ValidationResult.OutOfRange);

        var result = _controller.Convert(number);

        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        var errorResponse = badRequestResult.Value as ErrorResponse;
        Assert.That(errorResponse, Is.Not.Null);
        Assert.That(errorResponse.Error, Is.Not.Empty);
    }

    [Test]
    public void Convert_CallsValidatorWithCorrectNumber()
    {
        int number = 42;
        _mockValidator.Setup(v => v.ValidateSingle(number))
            .Returns(InputValidator.ValidationResult.Success);
        _mockConverterService.Setup(c => c.Convert(number))
            .Returns("42");

        _controller.Convert(number);

        _mockValidator.Verify(v => v.ValidateSingle(number), Times.Once);
    }

    [Test]
    public void Convert_CallsConverterWithCorrectNumber()
    {
        int number = 42;
        _mockValidator.Setup(v => v.ValidateSingle(number))
            .Returns(InputValidator.ValidationResult.Success);
        _mockConverterService.Setup(c => c.Convert(number))
            .Returns("42");

        _controller.Convert(number);

        _mockConverterService.Verify(c => c.Convert(number), Times.Once);
    }

    [Test]
    public void ConvertBatch_ValidNumbers_ReturnsOkResultWithResults()
    {
        var request = new FizzBuzzBatchRequest { Numbers = new[] { 1, 3, 5, 15, 30 } };
        var validatedNumbers = new List<int> { 1, 3, 5, 15, 30 };
        var convertedResults = new List<string> { "1", "Fizz", "Buzz", "FizzBuzz", "FizzBuzz" };

        _mockValidator.Setup(v => v.Validate(It.IsAny<int[]>(), out validatedNumbers))
            .Returns(InputValidator.ValidationResult.Success);
        _mockConverterService.Setup(c => c.ConvertBatch(It.IsAny<List<int>>()))
            .Returns(convertedResults);

        var result = _controller.ConvertBatch(request);

        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        var response = okResult.Value as FizzBuzzBatchResponse;
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Results, Has.Count.EqualTo(5));
        Assert.That(response.Results[0].Number, Is.EqualTo(1));
        Assert.That(response.Results[0].Result, Is.EqualTo("1"));
    }

    [Test]
    public void ConvertBatch_NullRequest_ReturnsBadRequest()
    {
        var emptyList = new List<int>();
        _mockValidator.Setup(v => v.Validate(It.IsAny<int[]>(), out emptyList))
            .Returns(InputValidator.ValidationResult.NoInput);

        var result = _controller.ConvertBatch(null);

        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        var errorResponse = badRequestResult.Value as ErrorResponse;
        Assert.That(errorResponse, Is.Not.Null);
        Assert.That(errorResponse.Error, Is.Not.Empty);
    }

    [Test]
    public void ConvertBatch_EmptyArray_ReturnsBadRequest()
    {
        var request = new FizzBuzzBatchRequest { Numbers = Array.Empty<int>() };
        var emptyList = new List<int>();
        _mockValidator.Setup(v => v.Validate(It.IsAny<int[]>(), out emptyList))
            .Returns(InputValidator.ValidationResult.NoInput);

        var result = _controller.ConvertBatch(request);

        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void ConvertBatch_WrongCount_ReturnsBadRequest()
    {
        var request = new FizzBuzzBatchRequest { Numbers = new[] { 1, 2, 3 } };
        var emptyList = new List<int>();
        _mockValidator.Setup(v => v.Validate(It.IsAny<int[]>(), out emptyList))
            .Returns(InputValidator.ValidationResult.WrongCount);

        var result = _controller.ConvertBatch(request);

        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        var errorResponse = badRequestResult.Value as ErrorResponse;
        Assert.That(errorResponse, Is.Not.Null);
        Assert.That(errorResponse.Error, Does.Contain("5"));
    }

    [Test]
    public void ConvertBatch_NumbersOutOfRange_ReturnsBadRequest()
    {
        var request = new FizzBuzzBatchRequest { Numbers = new[] { 1, 2, 3, 4, 150 } };
        var emptyList = new List<int>();
        _mockValidator.Setup(v => v.Validate(It.IsAny<int[]>(), out emptyList))
            .Returns(InputValidator.ValidationResult.OutOfRange);

        var result = _controller.ConvertBatch(request);

        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        var errorResponse = badRequestResult.Value as ErrorResponse;
        Assert.That(errorResponse, Is.Not.Null);
        Assert.That(errorResponse.Error, Does.Contain("1").And.Contains("100"));
    }

    [Test]
    public void ConvertBatch_CallsValidatorWithCorrectArray()
    {
        var request = new FizzBuzzBatchRequest { Numbers = new[] { 1, 2, 3, 4, 5 } };
        var validatedNumbers = new List<int> { 1, 2, 3, 4, 5 };
        _mockValidator.Setup(v => v.Validate(It.IsAny<int[]>(), out validatedNumbers))
            .Returns(InputValidator.ValidationResult.Success);
        _mockConverterService.Setup(c => c.ConvertBatch(It.IsAny<List<int>>()))
            .Returns(new List<string> { "1", "2", "Fizz", "4", "Buzz" });

        _controller.ConvertBatch(request);

        _mockValidator.Verify(v => v.Validate(
            It.Is<int[]>(arr => arr.SequenceEqual(request.Numbers)),
            out validatedNumbers),
            Times.Once);
    }

    [Test]
    public void ConvertBatch_CallsConverterBatchWithValidatedNumbers()
    {
        var request = new FizzBuzzBatchRequest { Numbers = new[] { 1, 2, 3, 4, 5 } };
        var validatedNumbers = new List<int> { 1, 2, 3, 4, 5 };
        _mockValidator.Setup(v => v.Validate(It.IsAny<int[]>(), out validatedNumbers))
            .Returns(InputValidator.ValidationResult.Success);
        _mockConverterService.Setup(c => c.ConvertBatch(It.IsAny<List<int>>()))
            .Returns(new List<string> { "1", "2", "Fizz", "4", "Buzz" });

        _controller.ConvertBatch(request);

        _mockConverterService.Verify(c => c.ConvertBatch(
            It.Is<List<int>>(list => list.SequenceEqual(validatedNumbers))),
            Times.Once);
    }
}
