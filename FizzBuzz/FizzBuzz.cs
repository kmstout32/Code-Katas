using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FizzBuzz;

public class FizzBuzz
{
    //CHALLLENGE: Implement the FizzBuzz logic as described below:
    //FOR number from 1 to 100:
    //IF(number is divisible by 3 and 5) THEN
    //    PRINT "FizzBuzz"
    // ELSE IF(number is divisible by 3) THEN
    //    PRINT "Fizz"
    // ELSE IF(number is divisible by 5) THEN
    //     PRINT "Buzz"
    // ELSE
    // PRINT number
    public static string Convert(int number)
    {
        bool divisibleBy3 = number % 3 == 0;
        bool divisibleBy5 = number % 5 == 0;

        string result = "";
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

        if (result == "")
        {
            if (number >= 1 && number <= 100)
            {
                result = number.ToString();
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Number must be between 1 and 100");
            }
        }

        return result;
    }

    // for 1 to 100
    public static void RunFizzBuzz()
    {
        for (int i = 1; i <= 100; i++)
        {
        
            string result = Convert(i);
            Console.WriteLine(result);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FizzBuzz.RunFizzBuzz();
        }
    }

}
