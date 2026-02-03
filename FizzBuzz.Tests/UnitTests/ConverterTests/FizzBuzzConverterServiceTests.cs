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
    [TestCase(99, "Fizz")]
    [TestCase(100, "Buzz")]
    [Test]
    public void Convert_ReturnsExpectedResult(int input, string expected)
    {

        string result = _converter.Convert(input);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ConvertBatch_ReturnsCorrectResults()
    {
        var numbers = new List<int> { 1, 3, 5, 15, 30 };

        var results = _converter.ConvertBatch(numbers);

        Assert.That(results, Has.Count.EqualTo(5));
        Assert.That(results[0], Is.EqualTo("1"));
        Assert.That(results[1], Is.EqualTo("Fizz"));
        Assert.That(results[2], Is.EqualTo("Buzz"));
        Assert.That(results[3], Is.EqualTo("FizzBuzz"));
        Assert.That(results[4], Is.EqualTo("FizzBuzz"));
    }

    [Test]
    public void ConvertBatch_EmptyList_ReturnsEmptyList()
    {
        var numbers = new List<int>();

        var results = _converter.ConvertBatch(numbers);

        Assert.That(results, Is.Empty);
    }

    [Test]
    public void ConvertBatch_SingleNumber_ReturnsSingleResult()
    {
        var numbers = new List<int> { 15 };

        var results = _converter.ConvertBatch(numbers);

        Assert.That(results, Has.Count.EqualTo(1));
        Assert.That(results[0], Is.EqualTo("FizzBuzz"));
    }
}
