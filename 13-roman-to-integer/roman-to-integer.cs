public class Solution
{
    // define a list with roman numerals
    private static Dictionary<string, int> RomanNumerals = new()
    {
        ["I"] = 1,
        ["IV"] = 4,
        ["V"] = 5,
        ["IX"] = 9,
        ["X"] = 10,
        ["XL"] = 40,
        ["L"] = 50,
        ["XC"] = 90,
        ["C"] = 100,
        ["CD"] = 400,
        ["D"] = 500,
        ["CM"] = 900,
        ["M"] = 1000
    };

    public int RomanToInt(string s) 
    {
        var number = 0;

        // iterate string characters
        for(var i = 0; i < s.Length; ++i)
        {
           if(i + 1 < s.Length)
           {
              var key = $"{s[i]}{s[i + 1]}";

              if(RomanNumerals.ContainsKey(key))
              {
                 number += RomanNumerals[key];
                 ++i;
                 continue;
              }

           }

           number += RomanNumerals[$"{s[i]}"];
        }

        return number;
    }
}