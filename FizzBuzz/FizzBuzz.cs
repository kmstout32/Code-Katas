namespace FizzBuzz;

public class FizzBuzz
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Please enter 5 numbers separated by commas between 1-100 (e.g., 3,15,7,20,5):");
        string? input = Console.ReadLine();

        UserInputProcessor.ProcessUserInput(input);
    }

    public static string Convert(int number)
    {
        // Validate input range 
        if (number < 1 || number > 100) // Why validate here before checking in UserInputProcessor. Because this method can be called independently. 
        {
            throw new ArgumentOutOfRangeException(nameof(number), "Number must be between 1 and 100");
        }

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
