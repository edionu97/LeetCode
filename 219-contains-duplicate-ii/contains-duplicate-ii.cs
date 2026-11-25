public class Solution 
{
    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        // if this happens => i == j => contradiction
        if(k == 0)
        {
            return false;
        }
        
        Dictionary<int, Data> seqLength = new();
        var kMin = int.MaxValue;
        for(var i = 0; i < nums.Length && kMin > k; ++i)
        {
            // if first occurence keep its possition and asume oo length
            if(!seqLength.ContainsKey(nums[i]))
            {
                seqLength[nums[i]] = new Data(i);
                continue;
            }
            
            // deconstruct touple
            var (p, size) = seqLength[nums[i]];
            
            var kDif = i - p;
            
            kMin = Math.Min(kMin, kDif);
            
            // update keept data
            seqLength[nums[i]] = new Data(i, Math.Min(size, kDif));
        }
        
        return kMin <= k;
    }
    
    private record Data(int Position, int Size = int.MaxValue);
}