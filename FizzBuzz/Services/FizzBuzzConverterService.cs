namespace FizzBuzz.Services;

public class FizzBuzzConverterService
{
    public virtual string Convert(int number)
    {
        bool divisibleBy3 = number % 3 == 0;
        bool divisibleBy5 = number % 5 == 0;

        string result;
        if (divisibleBy3 && divisibleBy5)
        {
            result = "FizzBuzz";
        }
        else if (divisibleBy3)
        {
            result = "Fizz";
        }
        else if (divisibleBy5)
        {
            result = "Buzz";
        }
        else
        {
            result = number.ToString();
        }

        return result;
    }

    public virtual List<string> ConvertBatch(List<int> numbers)
    {
        return numbers.Select(n => Convert(n)).ToList();
    }
}
