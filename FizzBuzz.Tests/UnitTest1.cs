namespace FizzBuzz.Tests;

public class FizzBuzzTests
{
    //Numbers divisible by 3 only. should return "Fizz"
    [TestCase(3, "Fizz")]
    [TestCase(6, "Fizz")]
    [TestCase(9, "Fizz")]
    [TestCase(12, "Fizz")]
    [TestCase(99, "Fizz")]
    // Numbers divisible by 5. should return "Buzz"
    [TestCase(5, "Buzz")]
    [TestCase(10, "Buzz")]
    [TestCase(20, "Buzz")]
    [TestCase(25, "Buzz")]
    [TestCase(100, "Buzz")]
    //Numbers divisible by both 3 and 5.should return "FizzBuzz"
    [TestCase(15, "FizzBuzz")]
    [TestCase(30, "FizzBuzz")]
    [TestCase(45, "FizzBuzz")]
    [TestCase(60, "FizzBuzz")]
    [TestCase(90, "FizzBuzz")]
    //Numbers not divisible by 3 or 5. should return the number as a string
    [TestCase(1, "1")]
    [TestCase(2, "2")]
    [TestCase(4, "4")]
    [TestCase(7, "7")]
    [TestCase(8, "8")]
    [TestCase(11, "11")]
    [TestCase(97, "97")]
    //Negative and Edge cases
    [TestCase(0, "FizzBuzz")]
    [TestCase(-15, "FizzBuzz")]
    [TestCase(-3, "Fizz")]
    [TestCase(-5, "Buzz")]
    [TestCase(-1, "-1")]
    [TestCase(1000, "Buzz")]
    [TestCase(999, "Fizz")]
    [TestCase(1001, "1001")]
    [Test]
    public void Convert_ReturnsExpectedResult(int input, string expected)
    {
        // Act
        string result = FizzBuzz.Convert(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
