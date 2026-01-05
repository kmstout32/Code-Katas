namespace FizzBuzz;

public class UserInputProcessor
{
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

        if (string.IsNullOrWhiteSpace(nullableInput))   // Check: empty or just whitespace
        {
            Console.WriteLine("Error: No input provided.");
            return;
        }

        // Parse user input string into a list of integers
        List<int> numbers = new List<int>();

        try
        {
            string[] usersNumbers = nullableInput.Split(','); // Split input into an array

            if (usersNumbers.Length != 5) // check for exactly 5 numbers
            {
                Console.WriteLine("Error: You must enter exactly 5 numbers.");
                return;
            }
            foreach (string usersNumber in usersNumbers) // Parse each user Number and add to the list
            {
                string trimmedNumber = usersNumber.Trim();
                int number = int.Parse(trimmedNumber);

                // FIRST: Validate number is in the range 1-100
                if (number < 1 || number > 100)
                {
                    Console.WriteLine("Error: Number must be between 1 and 100.");
                    return;
                }

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
            string result = FizzBuzz.Convert(number);
            Console.WriteLine(number + " -> " + result);
        }
    }
}
