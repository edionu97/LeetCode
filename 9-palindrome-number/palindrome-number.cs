public class Solution 
{
    /// <summary>
    /// Determines whether the decimal digits of <paramref name="x"/> read the same in both directions.
    /// </summary>
    /// <param name="x">The value to test.</param>
    /// <returns><see langword="true"/> if <paramref name="x"/> is a palindrome; otherwise <see langword="false"/>.</returns>
    public bool IsPalindrome(int x)
    {
        // A leading minus sign has no counterpart at the other end
        if (x < 0)
        {
            return false;
        }

        return x == Reverse(x);

        // Accumulates in long because reversing can exceed int.MaxValue (1000000003 -> 3000000001)
        static long Reverse(int x)
        {
            long rev = 0;

            while (x != 0)
            {
                rev = x % 10 + rev * 10;
                x /= 10;
            }

            return rev;
        }
    }
}