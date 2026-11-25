public class Solution 
{
    public IList<IList<int>> ThreeSum(int[] nums) 
    {
        // order nums ascending
        nums = nums.OrderBy(x => x).ToArray();
        
        HashSet<string> used = new();
        
        // find two sum
        List<IList<int>> res = new();
        for(var i = 0; i < nums.Length; ++i)
        {
            // compute all the possible sollutions O(n)
            foreach(var item in FindElementsHavingTwoSum(nums, -nums[i], i + 1))
            {
                // get j and k indices
                int j = item[0], k = item[1];
                
                var pattern = $"{nums[i]},{nums[j]},{nums[k]}";
                
                //ignore used pattern
                if(used.Contains(pattern))
                {
                    continue;
                }  
                used.Add(pattern);
                
                // add touple in result list
                res.Add(new List<int>{ nums[i], nums[j], nums[k] });
            }
         
            // other equal next values are the same so skip those value
            while(i + 1 < nums.Length && nums[i + 1] == nums[i])
            {
               ++i;
            }
        }
        
        return res;
    }
    
    public static IEnumerable<int[]> FindElementsHavingTwoSum(int[] numbers, int twoSum, int left)
    {
        int right = numbers.Length - 1;

        // try see if can element 
        while(left < right && left >= 0 && right < numbers.Length)
        {
            // compute edge sum
            var sum = numbers[left] + numbers[right];
            
            // stop condition
            if(sum == twoSum)
            {
                yield return new int[]{ left, right };
            }

            if(sum > twoSum)
            {
                --right;
                continue;
            }

            ++left; 
        }
    }
}