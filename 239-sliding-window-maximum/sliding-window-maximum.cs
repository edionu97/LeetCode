
public class Solution 
{
    public int[] MaxSlidingWindow(int[] nums, int k)
    {
        if (k == 1)
            return nums;

        int[] result = new int[nums.Length - k + 1];

        // Circular deque
        WindowElement[] deque = new WindowElement[k];
        int head = 0;
        int count = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            // Remove expired elements from the front
            if (count > 0 && deque[head].Index <= i - k)
            {
                head = (head + 1) % k;
                count--;
            }

            // Remove dominated elements from the back
            while (count > 0)
            {
                int lastIndex = (head + count - 1) % k;

                if (deque[lastIndex].Value >= nums[i])
                    break;

                count--;
            }

            // Add current element at the back
            int insertIndex = (head + count) % k;
            deque[insertIndex] = new WindowElement(nums[i], i);
            count++;

            // Record maximum once the first window is complete
            if (i >= k - 1)
            {
                result[i - k + 1] = deque[head].Value;
            }
        }

        return result;
    }

    private readonly record struct WindowElement(int Value, int Index);

}