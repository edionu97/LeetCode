public class Solution 
{
    private static readonly int[] IntMaxPrefixes = [2, 21, 214, 2147, 21474, 214748, 2147483, 21474836, 214748364, 2147483647];
    private static readonly int[] IntMinPrefixes = [-2, -21, -214, -2147, -21474, -214748, -2147483, -21474836, -214748364, -2147483648];

    /// <summary>
    /// Returns the digits of <paramref name="x"/> in reverse order, or 0 if that reversal will not
    /// fit in an <see cref="int"/>.
    /// </summary>
    /// <remarks>
    /// Only a 10-digit input can overflow, since nine digits reverse to at most 999999999, so
    /// shorter values take an unchecked loop.
    /// <para>
    /// For 10-digit values the result is validated as it is built. After k steps the accumulator
    /// holds the leading k digits of the answer, and two numbers with the same digit count order
    /// the same way their prefixes do, so the accumulator can be compared against the leading k
    /// digits of int.MaxValue, or int.MinValue when the input is negative. Bailing out the moment a
    /// prefix is exceeded means the accumulator is never allowed to reach a value that would wrap,
    /// so no widening to a larger type is needed.
    /// </para>
    /// </remarks>
    /// <param name="x">The value to reverse.</param>
    /// <returns>The reversed value, or 0 when it falls outside the int range.</returns>
    public int Reverse(int x)
    {
        int rev = 0;

        // Handle the case when the number of digits for x is less than the number of digits for int.MaxValue or int.MinValue.
        if (x is > -1000000000 and < 1000000000)
        {
            while (x != 0)
            {
                rev = x % 10 + rev * 10;
                x /= 10;
            }

            return rev;
        }

        bool isNegative = x < 0;
        int[] arrayToCompare = isNegative ? IntMinPrefixes : IntMaxPrefixes;

        // In case the number of digits for x is equal to the number of digits for int.MaxValue or int.MinValue, we need to check for overflow at each step.
        for (int index = 0; x != 0; x /= 10, ++index)
        {
            rev = x % 10 + rev * 10;

            if (isNegative ? rev < arrayToCompare[index] : rev > arrayToCompare[index])
            {
                return 0;
            }
        }

        return rev;
    }
}