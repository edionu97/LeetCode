public class Solution 
{
    public  int ScheduleCourse(int[][] courses)
    {
        // Ensure that courses are ordered by deadline
        // O(nlogn)
        Array.Sort(courses, (int[] a, int[] b) => a[1].CompareTo(b[1]));

        PriorityQueue<int, int> coursesQueue = new(new MaxHeapComparer());

        for(int i = 0, endDay = 0; i < courses.Length; ++i)
        {
            // Extract the course and duration for course i
            int durationI = courses[i][0];
            int deadlineI = courses[i][1];

            // Add the current course in the courses heap
            coursesQueue.Enqueue(i, durationI);
            endDay += durationI;

            // Currently we cannot accomodate course i because will exceed deadline => we need to make space
            if(endDay > deadlineI)
            {
                int courseIdx = coursesQueue.Dequeue();

                // Get the course with the highest duration and remove it from the list
                // We make enough space to accomodate current course, or if the current course is the highest one, we remove it
                endDay -= courses[courseIdx][0];
            }
        }

        return coursesQueue.Count;
    }

    private class MaxHeapComparer : IComparer<int>
    {
        public int Compare(int x, int y) => y.CompareTo(x);
    }
}