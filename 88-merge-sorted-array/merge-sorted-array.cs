public class Solution 
{
    public void Merge(int[] nums1, int m, int[] nums2, int n) {
        
        // copy m values in another array O(m)
        int[] nums1Copy = new int[m];
        for(int i = 0; i < m; ++i)
        {
            nums1Copy[i] = nums1[i];
        }
        
        // perform merge sort 
        int num1Idx = 0, num2Idx = 0, index = 0;
        while(num1Idx < m && num2Idx < n)
        {
            // take from first array if minimum is in the first one
            if(nums1Copy[num1Idx] <= nums2[num2Idx])
            {
                nums1[index++] = nums1Copy[num1Idx++];
                continue;
            }
            
            nums1[index++] = nums2[num2Idx++];
        }
        
        // put the leftovers
        while(num1Idx < m)
        {
            nums1[index++] = nums1Copy[num1Idx++];
        }
        
        while(num2Idx < n)
        {
            nums1[index++] = nums2[num2Idx++];
        }
    }
}