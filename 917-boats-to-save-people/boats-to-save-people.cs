public class Solution 
{
    public int NumRescueBoats(int[] people, int limit)
    {
        Array.Sort(people, (a, b) => b.CompareTo(a));

        int boats = 0;
        for(int i = 0, j = people.Length - 1; i <= j; ++i)
        {
            // Try use a boat for the heaviest person and the lowest one
            if (people[i] + people[j] <= limit)
            {
                ++boats;
                --j;
                continue;
            }

            ++boats;
        }

        return boats;
    }
}