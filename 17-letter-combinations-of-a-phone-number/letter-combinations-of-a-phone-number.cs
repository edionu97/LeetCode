public class Solution 
{
    private static readonly Dictionary<char, string> PhoneMap = new()
    {
        ['2'] = "abc",
        ['3'] = "def",
        ['4'] = "ghi",
        ['5'] = "jkl",
        ['6'] = "mno",
        ['7'] = "pqrs",
        ['8'] = "tuv",
        ['9'] = "wxyz"
    };

    public IList<string> LetterCombinations(string digits)
    {
        // Handle the case where the input string is empty or null, returning an empty list of combinations.
        if (string.IsNullOrEmpty(digits))
        {
            return [];
        }

        // Initialize a list to store the resulting combinations and start the recursive backtracking process.
        List<string> results = [];
        FindAllLetterCombinationsInternal(0, new StringBuilder());

        return results;

        /*
        * Helper function to find all letter combinations recursively.
        * Uses backtracking to explore all possible combinations of letters for the given digits.
        */
        void FindAllLetterCombinationsInternal(int index, StringBuilder sb)
        {
            // When no digits are left, we have a complete combination
            if (index == digits.Length)
            {
                results.Add(sb.ToString());
                return;
            }

            // Iterate through each letter corresponding to the first digit
            foreach (char letter in PhoneMap[digits[index]])
            {
                // Find new combinations for the remaining digits and append the current letter
                FindAllLetterCombinationsInternal(index + 1, sb.Append(letter));

                // Remove the last letter to backtrack for the next iteration
                sb.Length--;
            }
        }
    }
}