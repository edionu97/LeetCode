public class Solution 
{
    public int HIndex(int[] citations)     
    {
        // order citations descending
        citations = citations.OrderBy(x => x).ToArray();
        
        // compute the h value, initially assume h = length(citations)
        for(var hIdx = citations.Length; hIdx >= 1; --hIdx)
        {
            // compute the areay idx
            var idx = citations.Length - hIdx;
            
            // h index found
            if(citations[idx] >= hIdx)
            {
                return hIdx;
            }
        }
        
        return 0;
    }
}