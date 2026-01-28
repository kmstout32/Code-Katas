# FizzBuzz Kata

Part of the Code Katas collection - a C# implementation of the classic FizzBuzz kata, built with .NET 9.0 and NUnit testing framework.

## Development Journey

This project evolved through four phases of development, each building upon the previous:

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
Extended the program with additional functionality:
- **Interactive console application** - Added user interaction for batch number processing
- **Input validation** - Implemented strict range checking (1-100, rejecting 0, negatives, and values over 100)
- **Error handling** - Comprehensive exception handling for FormatException, OverflowException, and ArgumentOutOfRangeException
- **Clean formatted output** - Stacked results with clear formatting (each result on its own line)
- **Null safety** - Proper handling of nullable Console.ReadLine() return values
- **User guidance** - Clear prompts and error messages for better user experience

### Phase 4: Service Architecture Refactoring
Refactored from monolithic design to service-oriented architecture with separation of concerns:
- **Service Layer Pattern** - Separated business logic into distinct services
- **Dependency Injection** - Constructor injection for loose coupling
- **Single Responsibility** - Each class has one clear purpose
- **Testability** - Virtual methods enable mocking for integration tests
- **Enum-based Validation** - Type-safe error handling with ValidationResult enum
- **Comprehensive Testing** - 30 unit and integration tests with Moq framework

## Current Features

### 1. Service-Oriented Architecture
The application is built using clean architecture principles with three distinct services:

**FizzBuzzConverterService** - Core business logic for number conversion
```csharp
var converter = new FizzBuzzConverterService();
converter.Convert(15);  // Returns "FizzBuzz"
converter.Convert(3);   // Returns "Fizz"
converter.Convert(5);   // Returns "Buzz"
converter.Convert(7);   // Returns "7"
```

**InputValidator** - Multi-layer validation with typed results
```csharp
var validator = new InputValidator();
var result = validator.Validate("1,2,3,4,5", out List<int> numbers);
// Returns: ValidationResult.Success, NoInput, InvalidFormat, OutOfRange, or WrongCount
```

**InputProcessorService** - Orchestrates validation and conversion with user feedback
```csharp
var processor = new InputProcessorService(validator, converter);
processor.ProcessUserInput("3,15,7,20,5");
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

### 3. Comprehensive Input Validation
Multi-layered validation with specific error reporting:
- **Null/Empty Check** - Detects missing input
- **Count Validation** - Requires exactly 5 numbers
- **Format Validation** - Catches non-numeric values (letters, decimals)
- **Range Validation** - Enforces 1-100 range (rejects 0, negatives, >100)
- **Overflow Protection** - Handles numbers too large for int

**Error Handling Examples:**
```
Input: ""
Error: No input provided.

Input: "1,2,3"
Error: You must enter exactly 5 numbers.

Input: "abc,2,3,4,5"
Error: Invalid number format detected. Please enter only whole numbers separated by commas.

Input: "0,2,3,4,150"
Error: Number must be between 1 and 100.
```

## Project Structure

```
FizzBuzzSolution.sln
├── FizzBuzz/                              # Main application
│   ├── Program/
│   │   └── FizzBuzz.cs                    # Entry point with dependency setup
│   ├── Services/
│   │   ├── FizzBuzzConverterService.cs    # Core FizzBuzz conversion logic
│   │   └── InputProcessorService.cs       # Orchestrates validation and conversion
│   └── Validators/
│       └── InputValidator.cs              # Multi-layer input validation
│
└── FizzBuzz.Tests/                        # Test project (NUnit + Moq)
    ├── UnitTests/
    │   ├── ConverterTests/
    │   │   └── FizzBuzzConverterServiceTests.cs    # Tests core conversion logic
    │   └── ValidatorTests/
    │       └── InputValidatorTests.cs              # Tests validation rules
    └── Mocks/
        └── MockInputProcessorService.cs            # Integration tests with mocks
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

**Test Suite: 30 comprehensive tests** covering:

### Unit Tests (20 tests)
**FizzBuzzConverterService (10 tests):**
- Numbers divisible by 3 → "Fizz" (3, 9, 99)
- Numbers divisible by 5 → "Buzz" (5, 10, 100)
- Numbers divisible by both → "FizzBuzz" (15, 30)
- Regular numbers → string representation (1, 7)
- Boundary values (99, 100)

**InputValidator (12 tests):**
- Valid input → Success
- Null/empty/whitespace → NoInput (3 tests)
- Non-numeric values → InvalidFormat (3 tests: letters, decimals, words)
- Out of range → OutOfRange (4 tests: 0, negative, 101, 150)
- Wrong count → WrongCount (3 tests: too few, too many, single number)
- Validation order verification (count checked before range)

### Integration Tests (5 tests)
**InputProcessorService with Mocked Dependencies:**
- Success path → calls converter for each valid number
- NoInput → doesn't call converter
- InvalidFormat → doesn't call converter
- OutOfRange → doesn't call converter
- WrongCount → doesn't call converter

**Code Coverage: 88% lines, 96% branches**

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
