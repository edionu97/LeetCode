public class Solution 
{
    public int Candy(int[] ratings) 
    {
        var m = ratings.Length;

        // if i is greater than i - 1 it must have a bigger rank than i - 1
        var greaterThanPrev = new int[m];
        for(var i = 1; i < m; ++i)
        {
            if(ratings[i] > ratings[i - 1])
            {
                greaterThanPrev[i] = greaterThanPrev[i - 1] + 1;
            }
        }

        // if i is greater than i + 1 it must have a bigger rank than i + 1
        var greaterThanNext = new int[m];
        for(var i = m - 2; i >= 0; --i)
        {
            if(ratings[i] > ratings[i + 1])
            {
                greaterThanNext[i] = greaterThanNext[i + 1] + 1;
            }
        }

        // total number of candies are m + the condition which satisifies both terms (left and right)
        var candies = m;
        for(var i = 0; i < m; ++i)
        {
            candies += Math.Max(greaterThanNext[i],greaterThanPrev[i]);
        }

        return candies;
    }
}