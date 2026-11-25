public class Solution 
{
    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        var stations = cost.Length;

        var set = new HashSet<int>();

        // iterate throgh all stations
        for(var i = 0; i < stations; ++i)
        {
            // start with empty tank
            int tank = gas[i], current = i;

            if(set.Contains(i))
            {
                continue;
            }

            do
            {
                set.Add(current);

                // if no more fuel, we cannot travel to next station
                if(tank - cost[current] < 0)
                {
                    break;
                }

                // compute next station
                var next = (current + 1) % stations;

                // decrease tank value
                tank = tank - cost[current] + gas[next];

                // nex station is the actual one
                if(next == i)
                {
                    return i;
                }

                current = next;
            }
            while(tank >= 0 & current < stations);
        }

        return -1;
    }
}