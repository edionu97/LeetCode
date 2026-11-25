public class Solution 
{
    public IList<IList<int>> SubsetsWithDup(int[] nums)
    {
        // Sort the array to ensure items are always in same order
        // 1,2 and 2,1 sets are the same
        Array.Sort(nums);

        // Declare the list in which we store the results
        IList<IList<int>> result = [];

        // Do the backtracking to populate the result
        Backtrack(0, []);

        // Return the result
        return result;

        void Backtrack(int start, IList<int> sol)
        {
            // Add the result in list
            result.Add(sol.ToList());

            // Start exploring indices
            for (int i = start; i < nums.Length; ++i)
            {
                // Add an item in the list and explore next moves
                sol.Add(nums[i]);
                Backtrack(i + 1, sol);
                sol.RemoveAt(sol.Count - 1);

                // Remove the item(s) at level i which are the same with the value we've choosen since we'll obtain duplicates
                while(i + 1 < nums.Length && nums[i] == nums[i + 1])
                {
                    ++i;
                }
            }
        }
    }
}