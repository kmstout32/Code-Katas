# FizzBuzz Kata

Part of the Code Katas collection - a C# implementation of the classic FizzBuzz kata, built with .NET 9.0 and NUnit testing framework.

## Development Journey

This project evolved through three phases of development, each building upon the previous:

### Phase 1: Initial Challenge - Generic FizzBuzz
The first challenge was to create a generic FizzBuzz implementation in the terminal following these rules:
- If a number is divisible by 3, return "Fizz"
- If a number is divisible by 5, return "Buzz"
- If a number is divisible by both 3 and 5, return "FizzBuzz"
- Otherwise, return the number as a string

### Phase 2: Code Review and Refactoring
After completing the initial implementation, I had a code review with my manager. During this session, we refactored the solution to explore different approaches to solving the problem, improving code clarity and maintainability.

**Key Refactoring Decisions:**
- Explored multiple implementation patterns (switch with pattern matching, if-else chains, string concatenation)
- Evaluated trade-offs between different approaches
- Chose the if-else structure for clarity and readability
- Discussed when to use switch cases vs. if-else statements in C#
- Learned that multiple catch blocks are more idiomatic than switch on exception types

### Phase 3: Enhanced Features
The final challenge involved extending the program with additional functionality:
- **Interactive console application** - Added `ProcessUserInput()` method for user interaction
- **Input validation** - Implemented strict range checking (1-100, rejecting 0, negatives, and values over 100)
- **Error handling** - Comprehensive exception handling for FormatException, OverflowException, and ArgumentOutOfRangeException
- **Batch processing** - Created `RunFizzBuzz()` method to automatically print results for 1-100
- **Clean formatted output** - Stacked results with clear formatting (each result on its own line)
- **Null safety** - Proper handling of nullable Console.ReadLine() return values
- **User guidance** - Clear prompts and error messages for better user experience

## Current Features

### 1. Core FizzBuzz Conversion
The `Convert()` method accepts a number (1-100) and returns the FizzBuzz result:

```csharp
FizzBuzz.Convert(15);  // Returns "FizzBuzz"
FizzBuzz.Convert(3);   // Returns "Fizz"
FizzBuzz.Convert(5);   // Returns "Buzz"
FizzBuzz.Convert(7);   // Returns "7"
```

### 2. Interactive Console Application
Run the program to enter 5 numbers and see their FizzBuzz results:

```bash
dotnet run --project FizzBuzz
```

**Example Session:**
```
Please enter 5 numbers separated by commas between 1-100 (e.g., 3,15,7,20,5):
3,15,7,20,5

FizzBuzz Results:
3 -> Fizz
15 -> FizzBuzz
7 -> 7
20 -> Buzz
5 -> Buzz
```

### 3. Input Validation and Error Handling
- Numbers must be between 1 and 100
- Exactly 5 numbers required
- Invalid numbers show error messages while valid ones are processed
- Handles format errors, overflow errors, and out-of-range values

**Example with Invalid Input:**
```
Please enter 5 numbers separated by commas between 1-100 (e.g., 3,15,7,20,5):
0,50,101,-5,25

FizzBuzz Results:
0 -> Error: Number must be between 1 and 100 (Parameter 'number')
50 -> Buzz
101 -> Error: Number must be between 1 and 100 (Parameter 'number')
-5 -> Error: Number must be between 1 and 100 (Parameter 'number')
25 -> Buzz
```

### 4. Batch Processing (1-100)
The `RunFizzBuzz()` method automatically prints FizzBuzz results for numbers 1 through 100.

## Project Structure

```
FizzBuzzSolution.sln
├── FizzBuzz/
│   └── FizzBuzz.cs          # Main implementation with console features
└── FizzBuzz.Tests/
    └── UnitTest1.cs         # NUnit test suite
```

## Requirements

- .NET 9.0 SDK
- Visual Studio 2022 (or compatible IDE)

## Building the Project

```bash
dotnet build FizzBuzzSolution.sln
```

## Running Tests

```bash
dotnet test FizzBuzzSolution.sln
```

All 11 tests cover:
- Numbers divisible by 3 (returns "Fizz")
- Numbers divisible by 5 (returns "Buzz")
- Numbers divisible by both 3 and 5 (returns "FizzBuzz")
- Numbers not divisible by 3 or 5 (returns the number as string)
- Input validation for out-of-range values (0, negative numbers, >100)

## Implementation Details

### Core Algorithm
The `Convert()` method uses a clean if-else structure with boolean flags:

```csharp
public static string Convert(int number)
{
    // Validate input range (fail fast)
    if (number < 1 || number > 100)
    {
        throw new ArgumentOutOfRangeException(nameof(number), "Number must be between 1 and 100");
    }

    // Store divisibility checks
    bool divisibleBy3 = number % 3 == 0;
    bool divisibleBy5 = number % 5 == 0;

    // Determine result using if-else logic
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
```

### Key Design Decisions

**1. Why if-else over switch?**
- More readable for simple boolean conditions
- Explicit checking of both conditions for "FizzBuzz" case
- No need for pattern variable or case enumeration
- Clearer intent for future maintainers

**2. Why reject 0?**
- FizzBuzz challenge traditionally works with positive integers
- Range 1-100 aligns with classic implementation
- Validation ensures consistent behavior

**3. Why process invalid numbers individually?**
- Allows valid numbers to be processed even if some are invalid
- User gets maximum feedback in a single run
- Better user experience than failing on first error

**4. Input Validation Strategy:**
- **Format validation**: Checks for exactly 5 comma-separated values
- **Parse validation**: Catches FormatException and OverflowException
- **Range validation**: Throws ArgumentOutOfRangeException for out-of-bounds values
- **Null safety**: Handles nullable Console.ReadLine() return

### Error Handling Examples

**Invalid Format:**
```
Input: abc,def,ghi,jkl,mno
Output: Error: Invalid input. Please enter only valid numbers separated by commas.
```

**Wrong Number Count:**
```
Input: 1,2,3
Output: Error: You must enter exactly 5 numbers.
```

**Overflow:**
```
Input: 999999999999999999999999,1,2,3,4
Output: Error: One or more numbers are too large.
```

**Mixed Valid/Invalid:**
```
Input: 0,50,101,-5,25
Output:
0 -> Error: Number must be between 1 and 100
50 -> Buzz
101 -> Error: Number must be between 1 and 100
-5 -> Error: Number must be between 1 and 100
25 -> Buzz
```

## Lessons Learned

1. **Multiple approaches exist** - FizzBuzz can be implemented with if-else, switch, or string concatenation
2. **Readability matters** - Choose the approach that's clearest for maintainers
3. **Input validation is critical** - Always validate user input at system boundaries
4. **Fail fast principle** - Validate early to save processing costs
5. **User experience counts** - Clear error messages and formatting improve usability
6. **Testing validates refactoring** - Comprehensive tests ensured correctness through changes
