public class Solution 
{
    public IList<string> GenerateParenthesis(int n)
    {
        // List to store all valid combinations of parentheses
        IList<string> results = [];

        // Generate all valid combinations of parentheses using backtracking, initially starting with 0 open and 0 closed brackets, and an empty sequence
        GenerateParenthesisInternal(0, 0, new StringBuilder());

        return results;

        /*
        * Helper function to generate all valid combinations of parentheses using backtracking.
        * The function takes the current number of open and closed brackets, as well as the current sequence of brackets being built.
        * And smartly decides whether to add an open or closed bracket based on the current state of the sequence.
        */
        void GenerateParenthesisInternal(int openBrackets, int closedBrackets, StringBuilder sequence)
        {
            // If the number of open and closed brackets is equal to n, we have a valid combination of parentheses, so we add it to the results list and return.
            if (openBrackets == n && closedBrackets == n)
            {
                results.Add(sequence.ToString());
                return;
            }

            // If the number of open brackets is less than n, we can add an open bracket and continue generating the sequence.
            if (n - openBrackets > 0)
            {
                GenerateParenthesisInternal(openBrackets + 1, closedBrackets, sequence.Append('('));
                sequence.Length--;
            }

            // If the number of closed brackets is less than the number of open brackets, we can add a closed bracket and continue generating the sequence.
            if (closedBrackets + 1 <= openBrackets)
            {
                GenerateParenthesisInternal(openBrackets, closedBrackets + 1, sequence.Append(')'));
                sequence.Length--;
            }
        }
    }
}