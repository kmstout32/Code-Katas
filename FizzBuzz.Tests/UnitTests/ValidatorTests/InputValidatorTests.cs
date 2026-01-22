using FizzBuzz.Validators;

namespace FizzBuzz.Tests.UnitTests.ValidatorTests;

public class InputValidatorTests
{
    private InputValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new InputValidator();
    }

    [Test]
    public void Validate_ReturnsSuccess_ForValidInput()
    {

        string input = "1,2,3,4,5";

        var result = _validator.Validate(input, out List<int> numbers);

        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.Success));
        Assert.That(numbers, Is.EqualTo(new List<int> { 1, 2, 3, 4, 5 }));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    [Test]
    public void Validate_ReturnsNoInput_ForNullOrEmptyInput(string? input)
    {
        var result = _validator.Validate(input, out List<int> numbers);

        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.NoInput));
        Assert.That(numbers, Is.Empty);
    }

    [TestCase("abc,2,3,4,5")]
    [TestCase("1,2.5,3,4,5")]
    [TestCase("1,two,3,4,5")]
    [Test]
    public void Validate_ReturnsInvalidFormat_ForNonNumericInput(string input)
    {
    
        var result = _validator.Validate(input, out List<int> numbers);

        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.InvalidFormat));
    }

    [TestCase("0,2,3,4,5")]       // zero (below range)
    [TestCase("-1,2,3,4,5")]      // negative number
    [TestCase("101,2,3,4,5")]     // above upper boundary
    [TestCase("1,2,3,4,150")]     // last number out of range
    [Test]
    public void Validate_ReturnsOutOfRange_ForEdgeCaseNumbers(string input)
    {
        // Act
        var result = _validator.Validate(input, out List<int> numbers);

        // Assert
        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.OutOfRange));
    }

    [TestCase("1,2,3,4")]         // too few (4 numbers)
    [TestCase("1,2,3,4,5,6")]     // too many (6 numbers)
    [TestCase("1")]               // way too few (1 number)
    [Test]
    public void Validate_ReturnsWrongCount_ForIncorrectNumberCount(string input)
    {
        // Act
        var result = _validator.Validate(input, out List<int> numbers);

        // Assert
        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.WrongCount));
    }

    [Test]
    public void Validate_ChecksCountBeforeRange()
    {
        string input = "1,2,3,150";

        var result = _validator.Validate(input, out List<int> numbers);

        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.WrongCount));
    }
}
