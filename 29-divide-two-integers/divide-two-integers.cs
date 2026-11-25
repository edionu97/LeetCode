public class Solution 
{
    public int Divide(int dividend, int divisor)
    {
        // Handle the case where the dividend is less than divisor (meaning from digit count)
        if (dividend == 0 || GetNumberOfDigits(dividend) < GetNumberOfDigits(divisor))
        {
            return 0;
        }

        // Handle cases when both dividend and divisor is either int.MinValue or int.MaxValue
        switch (dividend)
        {
            case int.MaxValue when divisor == int.MinValue:
            {
                return 0;
            }

            case int.MinValue when divisor == int.MaxValue:
            {
                return -1;
            }
        }

        // Handle cases where numbers are equal (x == x)
        if (divisor == dividend)
        {
            return 1;
        }

        // Handle the cases where the dividend is the two's complement of the divisor or vice versa (x == -x)
        if (divisor == TwoComplement(dividend) || TwoComplement(divisor) == dividend)
        {
            return -1;
        }

        // Handle cases where the divisor is 1 or -1
        if (divisor is -1 or 1)
        {
            return divisor switch
            {
                // Overflow case
                -1 when dividend == int.MinValue => int.MaxValue,

                // Normal case
                _ => divisor < 0 ? -dividend : dividend
            };
        }

        bool isPositive = (divisor > 0 && dividend > 0) || (divisor < 0 && dividend < 0);
        return isPositive ? DivisionInternal(dividend, divisor) : -DivisionInternal(dividend, divisor);

        /*
        * Helper function to get the number of digits in a number.
        */
        static int GetNumberOfDigits(int number)
        {
            // Handle negative numbers
            if (number < 0)
            {
                return number switch
                {
                    > -10 => 1,
                    > -100 => 2,
                    > -1000 => 3,
                    > -10000 => 4,
                    > -100000 => 5,
                    > -1000000 => 6,
                    > -10000000 => 7,
                    > -100000000 => 8,
                    > -1000000000 => 9,
                    >= int.MinValue => 10,
                };

            }

            // Handle positive numbers
            return number switch
            {
                < 10 => 1,
                < 100 => 2,
                < 1000 => 3,
                < 10000 => 4,
                < 100000 => 5,
                < 1000000 => 6,
                < 10000000 => 7,
                < 100000000 => 8,
                < 1000000000 => 9,
                <= int.MaxValue => 10
            };
        }

        static int TwoComplement(int number) => ~number + 1;

        /*
        * dividend = divisor * quotient + remainder
        * The quotient can be expressed as a sum of powers of two: 2^n + 2^m + 2^k + ... + 2^p.
        * So:
        * dividend = divisor * (2^n + 2^m + 2^k + ... + 2^p) + remainder
        *          = divisor * 2^n + divisor * 2^m + divisor * 2^k + ... + divisor * 2^p + remainder.
        * Approach: repeatedly find the largest power of two such that divisor * 2^n <= dividend,
        * subtract that value from dividend, and continue until dividend < divisor.
        */
        static int DivisionInternal(int dividend, int divisor)
        {
            // Do a trick by converting both dividend and divisor to negative numbers, since the range of negative numbers is larger than positive numbers in two's complement representation.
            divisor = divisor > 0 ? -divisor : divisor;
            dividend = dividend > 0 ? -dividend : dividend;

            // Both numbers are negative so again we need to compare as we've compare |dividend| with |divisor| 
            // So that will fail only if divident is greater than divisor (-2 > -3)
            if(dividend > divisor)
            {
                return 0;
            }

            int quotient = 0;
            for (int i = 31, part = 0; i >= 0; --i)
            {
                // Skip this i if divisor << i (divisor * 2^i) would overflow.
                // int.MinValue >> i == -2^(31 - i), the smallest value that can be shifted left by i safely.
                // It checks divisor because divisor is the value being shifted, not dividend.
                if (divisor < int.MinValue >> i)
                {
                    continue;
                }
                
                // Compute divisor * 2^i using left shift operator
                part = divisor << i;

                // We can only subtract part from dividend if dividend is greater than or equal to part (both are negative numbers)
                if (dividend > part)
                {
                    continue;
                }

                dividend -= part; quotient |= 1 << i;
            }

            return quotient;
        }
    }
}