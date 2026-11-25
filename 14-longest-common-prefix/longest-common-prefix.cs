public class Solution 
{
    public string LongestCommonPrefix(string[] strs) 
    {
        var preffix = new StringBuilder();

        // prefix cannot be larger than the str with min length
        strs = strs.OrderBy(x => x.Length).ToArray();

        for(var j = 0; j < strs[0].Length; ++j)
        {
            var item = strs[0][j];

            // check if differences
            for(var i = 1; i < strs.Length; ++i)
            {
                if(strs[i][j] != item)
                {
                    return preffix.ToString();
                }
            }

            preffix.Append(item);
        }

        return preffix.ToString();
    }
}