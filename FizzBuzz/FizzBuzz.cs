using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FizzBuzz;

public class FizzBuzz
{
    //CHALLLENGE: Implement the FizzBuzz logic as described below:
    //FOR number from 1 to 100:
    //IF(number is divisible by 15) THEN
    //    PRINT "FizzBuzz"
    // ELSE IF(number is divisible by 3) THEN
    //    PRINT "Fizz"
    // ELSE IF(number is divisible by 5) THEN
    //     PRINT "Buzz"
    // ELSE
    // PRINT number
    public static string Convert(int number)
    {
        bool isDivisibleBy3 = number % 3 == 0;
        bool isDivisibleBy5 = number % 5 == 0;

        string result = "";

        if (isDivisibleBy3)
            result += "Fizz";

        if (isDivisibleBy5)
            result += "Buzz";

        if (result == "")
            result = number.ToString();

        return result;
    }

    // for 1 to 100
    public static void RunFizzBuzz()
    {
        for (int i = 1; i <= 100; i++)
        {
            // Reused Convert method for clean code
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
