using FizzBuzz.Models;
using FizzBuzz.Services;
using FizzBuzz.Validators;
using Moq;  

namespace FizzBuzz.Tests;

public class MockInputProcessorService
{
    private Mock<InputValidator> _mockValidator;
    private Mock<FizzBuzzConverterService> _mockFizzBuzzConverterService;
    private InputProcessorService _processor;

    [SetUp]
    public void Setup()
    {   
        _mockValidator = new Mock<InputValidator>();
        _mockFizzBuzzConverterService = new Mock<FizzBuzzConverterService>();

        _processor = new InputProcessorService(
            _mockValidator.Object,
            _mockFizzBuzzConverterService.Object
        );
    }
    [Test]
    public void InputProcessorService_Succeeds_When_Vaildation_Succeeds()
    {
        List<int> expectedNumbers = new List<int> { 1, 2, 3 };
        _mockValidator
            .Setup(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers))
            .Returns(InputValidator.ValidationResult.Success);

        _mockFizzBuzzConverterService
            .Setup(converter => converter.Convert(It.IsAny<int>()))
            .Returns((int n) => n.ToString());

        FizzBuzzModel result = _processor.ProcessNumberString("any input");

        _mockValidator
            .Verify(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers), Times.Once);
        _mockFizzBuzzConverterService
            .Verify(convertService => convertService.Convert(It.IsAny<int>()), Times.Exactly(3));

        Assert.That(result.ValidationResult, Is.EqualTo(InputValidator.ValidationResult.Success));
        Assert.That(result.ConvertedResults, Has.Count.EqualTo(3));
        Assert.That(result.IsSuccess, Is.True);
    }

    [TestCase(InputValidator.ValidationResult.NoInput, null)]
    [TestCase(InputValidator.ValidationResult.InvalidFormat, "abc,5,15,7,20")]
    [TestCase(InputValidator.ValidationResult.OutOfRange, "3,5,150,7,20")]
    [TestCase(InputValidator.ValidationResult.WrongCount, "3,5,15")]
    [Test]
    public void InputProcessorService_Returns_ValidationError(
        InputValidator.ValidationResult expectedValidationResult,
        string? input)
    {
        List<int> expectedNumbers = new List<int>();
        _mockValidator
            .Setup(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers))
            .Returns(expectedValidationResult);

            FizzBuzzModel result = _processor.ProcessNumberString(input);

        _mockValidator
            .Verify(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers), Times.Once);
        _mockFizzBuzzConverterService.Verify(convertService => convertService.Convert(It.IsAny<int>()), Times.Never);

        Assert.That(result.ValidationResult, Is.EqualTo(expectedValidationResult));
        Assert.That(result.ConvertedResults, Is.Empty);
        Assert.That(result.IsSuccess, Is.False);
    }
}