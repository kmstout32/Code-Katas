namespace FizzBuzz.Tests;

public class FizzBuzzTests
{
    [Test]
    public void Convert_NumberDivisibleBy3_ReturnsFizz()
    {
        Assert.That(FizzBuzz.Convert(3), Is.EqualTo("Fizz"));
        Assert.That(FizzBuzz.Convert(6), Is.EqualTo("Fizz"));
        Assert.That(FizzBuzz.Convert(9), Is.EqualTo("Fizz"));
    }

    [Test]
    public void Convert_NumberDivisibleBy5_ReturnsBuzz()
    {
        Assert.That(FizzBuzz.Convert(5), Is.EqualTo("Buzz"));
        Assert.That(FizzBuzz.Convert(10), Is.EqualTo("Buzz"));
        Assert.That(FizzBuzz.Convert(20), Is.EqualTo("Buzz"));
    }

    [Test]
    public void Convert_NumberDivisibleBy15_ReturnsFizzBuzz()
    {
        Assert.That(FizzBuzz.Convert(15), Is.EqualTo("FizzBuzz"));
        Assert.That(FizzBuzz.Convert(30), Is.EqualTo("FizzBuzz"));
        Assert.That(FizzBuzz.Convert(45), Is.EqualTo("FizzBuzz"));
    }

    [Test]
    public void Convert_NumberNotDivisibleBy3Or5_ReturnsNumber()
    {
        Assert.That(FizzBuzz.Convert(1), Is.EqualTo("1"));
        Assert.That(FizzBuzz.Convert(2), Is.EqualTo("2"));
        Assert.That(FizzBuzz.Convert(7), Is.EqualTo("7"));
    }
}
