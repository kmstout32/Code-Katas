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

        _processor.ProcessUserInput("any input");

        _mockValidator
            .Verify(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers), Times.Once);
        _mockFizzBuzzConverterService
            .Verify(convertService => convertService.Convert(It.IsAny<int>()), Times.AtLeastOnce);
    }

    [Test]
    public void InputProcessorService_Returns_NoInput()
    {
        List<int> expectedNumbers = new List<int>();
        _mockValidator
            .Setup(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers))
            .Returns(InputValidator.ValidationResult.NoInput);

        _processor.ProcessUserInput(null);

        _mockValidator
            .Verify(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers), Times.Once);
        _mockFizzBuzzConverterService.Verify(convertService => convertService.Convert(It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void InputProcessorService_Returns_InvalidFormat()
    {
        List<int> expectedNumbers = new List<int>();
        _mockValidator
            .Setup(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers))
            .Returns(InputValidator.ValidationResult.InvalidFormat);

        _processor.ProcessUserInput("abc,5,15,7,20");

        _mockValidator
            .Verify(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers), Times.Once);
        _mockFizzBuzzConverterService.Verify(convertService => convertService.Convert(It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void InputProcessorService_Returns_OutOfRange()
    {
        List<int> expectedNumbers = new List<int>();
        _mockValidator
            .Setup(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers))
            .Returns(InputValidator.ValidationResult.OutOfRange);

        _processor.ProcessUserInput("3,5,150,7,20");

        _mockValidator
            .Verify(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers), Times.Once);
        _mockFizzBuzzConverterService.Verify(convertService => convertService.Convert(It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void InputProcessorService_Returns_WrongCount()
    {
        List<int> expectedNumbers = new List<int>();
        _mockValidator
            .Setup(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers))
            .Returns(InputValidator.ValidationResult.WrongCount);

        _processor.ProcessUserInput("3,5,15");

        _mockValidator
            .Verify(validator => validator.Validate(It.IsAny<string>(), out expectedNumbers), Times.Once);
        _mockFizzBuzzConverterService.Verify(convertService => convertService.Convert(It.IsAny<int>()), Times.Never);
    }
}