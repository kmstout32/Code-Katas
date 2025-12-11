namespace FizzBuzz.Tests;

public class FizzBuzzTests
{
    // Numbers divisible by 3 only. should return "Fizz"
    [TestCase(3, "Fizz")]
    [TestCase(9, "Fizz")]
    // Numbers divisible by 5 only. should return "Buzz"
    [TestCase(5, "Buzz")]
    [TestCase(10, "Buzz")]
    // Numbers divisible by both 3 and 5. should return "FizzBuzz"
    [TestCase(15, "FizzBuzz")]
    [TestCase(30, "FizzBuzz")]
    // Numbers not divisible by 3 or 5. should return the number as a string
    [TestCase(1, "1")]
    [TestCase(7, "7")]
    // Edge cases
    [TestCase(0, "FizzBuzz")]
    [TestCase(-1, "-1")]
    [TestCase(101, "101")]
    [Test]
    public void Convert_ReturnsExpectedResult(int input, string expected)
    {
        // Act
        string result = FizzBuzz.Convert(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
