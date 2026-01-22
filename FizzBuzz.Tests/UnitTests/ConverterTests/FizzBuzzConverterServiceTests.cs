using FizzBuzz.Services;

namespace FizzBuzz.Tests.UnitTests.ConverterTests;

public class FizzBuzzConverterServiceTests
{
    private FizzBuzzConverterService _converter;

    [SetUp]
    public void SetUp()
    {
        _converter = new FizzBuzzConverterService();
    }

    [TestCase(3, "Fizz")]
    [TestCase(9, "Fizz")]

    [TestCase(5, "Buzz")]
    [TestCase(10, "Buzz")]
  
    [TestCase(15, "FizzBuzz")]
    [TestCase(30, "FizzBuzz")]

    [TestCase(1, "1")]
    [TestCase(7, "7")]
    [Test]
    public void Convert_ReturnsExpectedResult(int input, string expected)
    {
        // Act
        string result = _converter.Convert(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
