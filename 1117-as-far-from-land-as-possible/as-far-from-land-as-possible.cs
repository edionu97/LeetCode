public class Solution 
{
    private static readonly int[] Dx = { -1, 1, 0, 0 };
    private static readonly int[] Dy = { 0, 0, -1, 1 };

    public int MaxDistance(int[][] grid)
    {
        Queue<Point> queue = [];

        // Iterate the matrix to find all the land cells
        for (int i = 0; i < grid.Length; ++i)
        {
            for (int j = 0; j < grid[i].Length; ++j)
            {
                if (grid[i][j] == 1)
                {
                    queue.Enqueue(new Point(i, j, 0));
                }
            }
        }

        // If no items in queue => there is only watter
        // If all the items are in queue => only land
        if (queue.Count == 0 || queue.Count == grid.Length * grid.Length)
        {
            return -1;
        }


        int furthest = int.MinValue;

        // Start bfs from all the points sequentially in distance order
        while (queue.Count > 0)
        {
            // Get the current point
            Point p = queue.Dequeue();

            // Compute the furtherst distance so far
            furthest = Math.Max(furthest, p.Distance);

            // Use those forth directions to try move to another point
            for (int i = 0; i < Dx.Length; ++i)
            {
                int newX = p.X + Dx[i];
                int newY = p.Y + Dy[i];

                // Ignore invalid moves
                if (!(newX >= 0 && newY >= 0 && newX < grid.Length && newY < grid.Length && grid[newX][newY] == 0))
                {
                    continue;
                }

                // Mark the matrix since we've arrived from land to that watter
                grid[newX][newY] = 2;

                // Add the new point in queue
                queue.Enqueue(new Point(newX, newY, 1 + p.Distance));
            }
        }

        return furthest;
    }

    private record struct Point(int X, int Y, int Distance);
}