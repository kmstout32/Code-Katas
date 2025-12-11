namespace FizzBuzz.Tests;

public class FizzBuzzTests
{
    // Numbers divisible by 3 only
    [TestCase(3)]
    [TestCase(6)]
    [TestCase(9)]
    [TestCase(12)]
    [TestCase(99)]
    // Numbers divisible by 5 only
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(20)]
    [TestCase(25)]
    [TestCase(100)]
    // Numbers divisible by both 3 and 5
    [TestCase(15)]
    [TestCase(30)]
    [TestCase(45)]
    [TestCase(60)]
    [TestCase(90)]
    // Numbers not divisible by 3 or 5
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(4)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(11)]
    [TestCase(97)]
    // Edge cases
    [TestCase(0)]
    [TestCase(100)]
    [TestCase(-3)]
    [TestCase(-5)]
    [TestCase(-1)]
    [TestCase(101)]
    [Test]
    public void Convert_ReturnsExpectedResult(int input)
    {
        // Arrange - compute expected value using two-boolean approach
        bool isDivisibleBy3 = input % 3;
        bool isDivisibleBy5 = input % 5;

        string expected = "";
        if (isDivisibleBy3) expected += "Fizz";
        if (isDivisibleBy5) expected += "Buzz";
        if (expected == "") expected = input.ToString();

        string result = FizzBuzz.Convert(input);

        Assert.That(result, Is.EqualTo(expected));
    }
}
