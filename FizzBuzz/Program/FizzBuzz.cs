using FizzBuzz.Models;
using FizzBuzz.Services;
using FizzBuzz.Validators;

namespace FizzBuzz.Program;

public class FizzBuzzApp
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Please enter 5 numbers separated by commas between 1-100 (e.g., 3,15,7,20,5):");
        string? input = Console.ReadLine();

        InputValidator validator = new InputValidator();
        FizzBuzzConverterService converter = new FizzBuzzConverterService();
        InputProcessorService processor = new InputProcessorService(validator, converter);

        FizzBuzzModel result = processor.ProcessUserInput(input);

        if (result.IsSuccess)
        {
            Console.WriteLine("\nFizzBuzz Results:");
            for (int i = 0; i < result.Numbers.Count; i++)
            {
                Console.WriteLine($"{result.Numbers[i]} -> {result.ConvertedResults[i]}");
            }
        }
        else
        {
            Console.WriteLine(result.GetErrorMessage());
        }
    }
}
