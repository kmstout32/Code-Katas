namespace FizzBuzz;

public class FizzBuzz
{
    public static void Main(string[] args)
    {
        UserInputProcessor.ProcessUserInput();
    }

    public static string Convert(int number)
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

    // for 1 to 100 print the result of Convert
    public static void RunFizzBuzz()
    {
        for (int i = 1; i <= 100; i++)
        {

            string result = Convert(i);
            Console.WriteLine(result);
        }
    }
}
