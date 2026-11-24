public class Solution 
{
    private static readonly int[] Dx = { -1, 1, 0, 0 };
    private static readonly int[] Dy = { 0, 0, -1, 1 };

    public int SwimInWater(int[][] grid)
    {
        // Create a min heap storing 
        PriorityQueue<Point, int> minHeap = new();

        // Firstly we are in left top corner
        minHeap.Enqueue(new Point(0, 0), grid[0][0]);

        bool[,] visited = new bool[grid.Length, grid.Length];
        while (minHeap.TryDequeue(out Point point, out int cost))
        {
            // Deconstruct the point
            var (x, y) = point;

            // If we've marked the cell as visited before, it means that we've already computed the shortest cost
            // associated with that cell, and that's guaranteed by the min heap
            if (visited[x, y] == true)
            {
                continue;
            }

            visited[x, y] = true;

            // Stop if we've reached bottom right corner
            if (x == grid.Length - 1 && y == grid.Length - 1)
            {
                return cost;
            }

            // Make some movements
            for (int i = 0; i < Dx.Length; i++)
            {
                int newX = Dx[i] + x;
                int newY = Dy[i] + y;

                // Matrix overflow
                if (newX < 0 || newY < 0 || newX >= grid.Length || newY >= grid.Length)
                {
                    continue;
                }

                // Ignore alreay visited points
                if (visited[newX, newY])
                {
                    continue;
                }

                // Required time to visit a cell is either the maximum req time up until now or the value at the new coordinates
                // if its greater that the total cost
                minHeap.Enqueue(new Point(newX, newY), Math.Max(cost, grid[newX][newY]));
            }

        }

        return 0;
    }

    private record struct Point(int X, int Y);
}