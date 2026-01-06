using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FizzBuzz;

public class FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            FizzBuzz.ProcessUserInput();
        }
    }
    public static void ProcessUserInput()// Process user input of 5 comma-separated numbers
    {
        Console.WriteLine("Please enter 5 numbers separated by commas between 1-100 (e.g., 3,15,7,20,5):");

        // ReadLine can return null, so we need to handle that case
        string? nullableInput = Console.ReadLine();
        if (nullableInput == null)
        {
            Console.WriteLine("Error: No input provided.");
            return;
        }
        string input = nullableInput;

        // Check if input is empty or just whitespace
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Error: No input provided.");
            return;
        }

        // Parse user input string into a list of integers
        List<int> numbers = new List<int>();

        try
        {
            string[] usersNumbers = input.Split(','); // Split input into an array

            if (usersNumbers.Length != 5) // check for exactly 5 numbers
            {
                Console.WriteLine("Error: You must enter exactly 5 numbers.");
                return;
            }
            foreach (string usersNumber in usersNumbers) // Parse each user Number and add to the list
            {
                string trimmedNumber = usersNumber.Trim();
                int number = int.Parse(trimmedNumber);
                numbers.Add(number);
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Invalid input. Please enter only valid numbers separated by commas.");
            return;
        }
        catch (OverflowException)
        {
            Console.WriteLine("Error: One or more numbers are too large.");
            return;
        }

        // Process each number through FizzBuzz
        Console.WriteLine("\nFizzBuzz Results:");
        foreach (int number in numbers)
        {
            try
            {
                string result = Convert(number);
                Console.WriteLine(number + " -> " + result);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(number + " -> Error: " + ex.Message);
            }
        }
    }
    public static string Convert(int number)
    {
        // Validate input range FIRST (fail fast to save processing costs)
        if (number < 1 || number > 100)
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
