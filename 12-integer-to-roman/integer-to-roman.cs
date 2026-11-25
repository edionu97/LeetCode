public class Solution 
{
    // define a list with roman numerals
    private static Dictionary<int, string> RomanNumerals = new()
    {
        [1] = "I",
        [4] = "IV",
        [5] = "V",
        [9] = "IX",
        [10] = "X",
        [40] = "XL",
        [50] = "L",
        [90] = "XC",
        [100] = "C",
        [400] = "CD",
        [500] = "D",
        [900] = "CM",
        [1000] = "M"
    };

    private static int[] Values = {1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1};

    public string IntToRoman(int num) 
    {
        var builder = new StringBuilder();

        // iterate values 
        foreach(var value in Values)
        {
            // greedy approach, compute how many substractions can be performed
            var times = num / value;

            // decrease number
            num -= times * value;

            // append values
            while(times-- > 0)
            {
                builder.Append(RomanNumerals[value]);
            }
        }

        return builder.ToString();
    }
}