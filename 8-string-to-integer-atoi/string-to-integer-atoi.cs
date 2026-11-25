public class Solution 
{
    public int MyAtoi(string s)
    {
        // Ignore leading whitespace
        ReadOnlySpan<char> str = s.AsSpan().TrimStart(' ');

        // Check if the string is empty after trimming
        if (str.IsEmpty)
        {
            return 0;
        }

        // Get the number metadata (start index, end index, and sign), if possible
        NumberMetadata? metadata = GetNumberMetadata(str);
        if (metadata is null)
        {
            return 0;
        }

        (int startIndex, int endIndex, int sign) = metadata.Value;

        // Handle the case where the number is too large or too small to fit in an int
        if (endIndex - startIndex + 1 > 10)
        {
            return sign > 0 ? int.MaxValue : int.MinValue;
        }

        long nr = 0;
        for (int i = startIndex; i <= endIndex; i++)
        {
            nr = nr * 10 + (str[i] - '0');

            if (sign < 0 && -nr < int.MinValue)
            {
                return int.MinValue;
            }

            if (sign > 0 && nr > int.MaxValue)
            {
                return int.MaxValue;
            }
        }

        return (int)(nr * sign);

        static NumberMetadata? GetNumberMetadata(ReadOnlySpan<char> str)
        {
            int? startIndex = null;

            int endIndex = str[0] == '-' || str[0] == '+' ? 0 : -1;
            while (++endIndex < str.Length)
            {
                // Stop when we encounter a non-digit character
                if (!char.IsDigit(str[endIndex]))
                {
                    --endIndex;
                    break;
                }

                if (endIndex - startIndex + 1 > 10)
                {
                    break;
                }

                // Set the start index when we encounter the first non-zero digit
                if (startIndex is null && str[endIndex] != '0')
                {
                    startIndex = endIndex;
                }
            }

            if (startIndex is null)
            {
                return null;
            }

            return new NumberMetadata(startIndex.Value, Math.Min(endIndex, str.Length - 1), str[0] == '-' ? -1 : 1);
        }
    }

    private readonly record struct NumberMetadata(int StartIndex, int EndIndex, int Sign);
}