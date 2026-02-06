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

    [TestCase(1)]
    [TestCase(50)]
    [TestCase(100)]
    [Test]
    public void ValidateSingle_ValidNumber_ReturnsSuccess(int number)
    {
        var result = _validator.ValidateSingle(number);

        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.Success));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-100)]
    [Test]
    public void ValidateSingle_BelowRange_ReturnsOutOfRange(int number)
    {
        var result = _validator.ValidateSingle(number);

        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.OutOfRange));
    }

    [TestCase(101)]
    [TestCase(150)]
    [TestCase(1000)]
    [Test]
    public void ValidateSingle_AboveRange_ReturnsOutOfRange(int number)
    {
        var result = _validator.ValidateSingle(number);

        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.OutOfRange));
    }

    [Test]
    public void Validate_WithArrayAsString_ValidNumbers_ReturnsSuccess()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        string input = string.Join(",", numbers);

        var result = _validator.Validate(input, out List<int> validatedNumbers);

        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.Success));
        Assert.That(validatedNumbers, Is.EqualTo(new List<int> { 1, 2, 3, 4, 5 }));
    }

    [Test]
    public void Validate_WithArrayAsString_EmptyString_ReturnsNoInput()
    {
        int[] numbers = Array.Empty<int>();
        string input = string.Join(",", numbers);

        var result = _validator.Validate(input, out List<int> validatedNumbers);

        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.NoInput));
        Assert.That(validatedNumbers, Is.Empty);
    }

    [TestCase(new int[] { 1, 2, 3 })]
    [TestCase(new int[] { 1, 2, 3, 4, 5, 6 })]
    [TestCase(new int[] { 1 })]
    [Test]
    public void Validate_WithArrayAsString_WrongCount_ReturnsWrongCount(int[] numbers)
    {
        string input = string.Join(",", numbers);

        var result = _validator.Validate(input, out List<int> validatedNumbers);

        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.WrongCount));
    }

    [TestCase(new int[] { 0, 2, 3, 4, 5 })]
    [TestCase(new int[] { 1, 2, 3, 4, 101 })]
    [TestCase(new int[] { -1, 2, 3, 4, 5 })]
    [Test]
    public void Validate_WithArrayAsString_OutOfRange_ReturnsOutOfRange(int[] numbers)
    {
        string input = string.Join(",", numbers);

        var result = _validator.Validate(input, out List<int> validatedNumbers);

        Assert.That(result, Is.EqualTo(InputValidator.ValidationResult.OutOfRange));
    }
}
