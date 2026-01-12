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

