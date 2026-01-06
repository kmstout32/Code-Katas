# FizzBuzz Pseudocode Explanation

## The Refactored Approach

```
FUNCTION Convert(number):
    // STEP 1: Check divisibility and store results in booleans
    SET isDivisibleBy3 = (number can be divided evenly by 3)
    SET isDivisibleBy5 = (number can be divided evenly by 5)

    // STEP 2: Build the result string based on the booleans
    SET result = empty string

    IF isDivisibleBy3 is TRUE:
        ADD "Fizz" to result

    IF isDivisibleBy5 is TRUE:
        ADD "Buzz" to result

    // STEP 3: If result is still empty, use the number itself
    IF result is empty:
        SET result = number as string

    // STEP 4: Return the final result (only one return!)
    RETURN result
END FUNCTION
```

## Why This Works - Step-by-Step Examples

### Example 1: Number = 9 (divisible by 3 only)
```
1. isDivisibleBy3 = TRUE  (9 ÷ 3 = 3, no remainder)
   isDivisibleBy5 = FALSE (9 ÷ 5 = 1 remainder 4)

2. result = ""

3. isDivisibleBy3 is TRUE → result = "" + "Fizz" = "Fizz"

4. isDivisibleBy5 is FALSE → skip this

5. result is NOT empty → skip this

6. RETURN "Fizz" ✓
```

### Example 2: Number = 10 (divisible by 5 only)
```
1. isDivisibleBy3 = FALSE (10 ÷ 3 = 3 remainder 1)
   isDivisibleBy5 = TRUE  (10 ÷ 5 = 2, no remainder)

2. result = ""

3. isDivisibleBy3 is FALSE → skip this

4. isDivisibleBy5 is TRUE → result = "" + "Buzz" = "Buzz"

5. result is NOT empty → skip this

6. RETURN "Buzz" ✓
```

### Example 3: Number = 15 (divisible by BOTH 3 and 5)
```
1. isDivisibleBy3 = TRUE  (15 ÷ 3 = 5, no remainder)
   isDivisibleBy5 = TRUE  (15 ÷ 5 = 3, no remainder)

2. result = ""

3. isDivisibleBy3 is TRUE → result = "" + "Fizz" = "Fizz"

4. isDivisibleBy5 is TRUE → result = "Fizz" + "Buzz" = "FizzBuzz"

5. result is NOT empty → skip this

6. RETURN "FizzBuzz" ✓
```

### Example 4: Number = 7 (not divisible by 3 or 5)
```
1. isDivisibleBy3 = FALSE (7 ÷ 3 = 2 remainder 1)
   isDivisibleBy5 = FALSE (7 ÷ 5 = 1 remainder 2)

2. result = ""

3. isDivisibleBy3 is FALSE → skip this

4. isDivisibleBy5 is FALSE → skip this

5. result IS empty → result = "7"

6. RETURN "7" ✓
```

## The Magic: Why No Check for 15?

**Old approach needed to check 15 first:**
```
IF divisible by 15 THEN return "FizzBuzz"
ELSE IF divisible by 3 THEN return "Fizz"
ELSE IF divisible by 5 THEN return "Buzz"
```

**New approach is smarter:**
- If divisible by **both** 3 and 5, **both** booleans are TRUE
- Both IF statements execute, building "Fizz" + "Buzz" = "FizzBuzz"
- No need to explicitly check for 15!

## Visual Flow Chart

```
START
  ↓
Check: divisible by 3? → Set boolean
Check: divisible by 5? → Set boolean
  ↓
result = ""
  ↓
Is isDivisibleBy3 TRUE? → Yes: Add "Fizz"
  ↓
Is isDivisibleBy5 TRUE? → Yes: Add "Buzz"
  ↓
Is result empty? → Yes: result = number as string
  ↓
Return result
  ↓
END
```

## Key Concepts for New Developers

1. **Boolean variables** - Store TRUE/FALSE values
2. **String concatenation** (+=) - Adds text together
3. **Independent checks** - Both IF statements can execute (not else-if)
4. **Single return** - Cleaner code, only one exit point
5. **Building strings** - Start empty, add pieces conditionally

## Benefits of This Approach

✅ Uses two booleans to store divisibility checks
✅ Single return statement at the end
✅ No need to check for divisibility by 15 explicitly
✅ More maintainable - easy to add more rules (e.g., divisible by 7)
✅ Clearer logic flow - build result step by step
