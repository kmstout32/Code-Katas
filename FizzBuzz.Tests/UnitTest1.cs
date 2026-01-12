namespace FizzBuzz.Tests;

public class FizzBuzzTests
{
    //divisible by 3 only. should return "Fizz"
    [TestCase(3, "Fizz")]
    [TestCase(9, "Fizz")]
    //divisible by 5 only. should return "Buzz"
    [TestCase(5, "Buzz")]
    [TestCase(10, "Buzz")]
    //divisible by both 3 and 5. should return "FizzBuzz"
    [TestCase(15, "FizzBuzz")]
    [TestCase(30, "FizzBuzz")]
    //cases not divisible by 3 or 5. should return the number as a string
    [TestCase(1, "1")]
    [TestCase(7, "7")]
    [Test]
    public void Convert_ReturnsExpectedResult(int input, string expected)
    {
        // Act
        string result = FizzBuzz.Convert(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}

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
        // Arrange
        string input = "1,2,3,4,5";

        // Act
        var result = _validator.Validate(input, out List<int> numbers);

        // Assert
        Assert.That(result, Is.EqualTo(IInputValidator.ValidationResult.Success));
        Assert.That(numbers, Is.EqualTo(new List<int> { 1, 2, 3, 4, 5 }));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    [Test]
    public void Validate_ReturnsNoInput_ForNullOrEmptyInput(string? input)
    {
        // Act
        var result = _validator.Validate(input, out List<int> numbers);

        // Assert
        Assert.That(result, Is.EqualTo(IInputValidator.ValidationResult.NoInput));
        Assert.That(numbers, Is.Empty);
    }

    [TestCase("abc,2,3,4,5")]
    [TestCase("1,2.5,3,4,5")]
    [TestCase("1,two,3,4,5")]
    [Test]
    public void Validate_ReturnsInvalidFormat_ForNonNumericInput(string input)
    {
        // Act
        var result = _validator.Validate(input, out List<int> numbers);

        // Assert
        Assert.That(result, Is.EqualTo(IInputValidator.ValidationResult.InvalidFormat));
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
        Assert.That(result, Is.EqualTo(IInputValidator.ValidationResult.OutOfRange));
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
        Assert.That(result, Is.EqualTo(IInputValidator.ValidationResult.WrongCount));
    }

    [Test]
    public void Validate_ChecksRangeBeforeCount()
    {
        // Arrange - 4 numbers, one out of range
        string input = "1,2,3,150";

        // Act
        var result = _validator.Validate(input, out List<int> numbers);

        // Assert - should return OutOfRange, not WrongCount
        Assert.That(result, Is.EqualTo(IInputValidator.ValidationResult.OutOfRange));
    }
}

