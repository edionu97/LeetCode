public class Solution 
{
    private static int[] Dx = {-1, 1, 0, 0};
    private static int[] Dy = {0, 0, -1, 1};

    public int NumIslands(char[][] grid) 
    {
        int nrIslands = 0;

        for(var i = 0; i < grid.Length; ++i)
        {
            for(var j = 0; j < grid[i].Length; ++j)
            {
                if(grid[i][j] != '1')
                {
                    continue;
                }

                ++nrIslands;
                TraverseIsland(grid, i, j);
            }
        }

        return nrIslands;
    }

    private static void TraverseIsland(char[][] grid, int x, int y)
    {
        // compute grid dimmensions
        int m = grid.Length, n = grid[0].Length;

        // start traverse the island
        Queue<Point> points = new(){ new(x, y) };
        while(points.Any())
        {
            // get the current coordinate
            var (i, j) = points.Dequeue();

            grid[i][j] = '0';

            for(var k = 0; k < Dx.Length; ++k)
            {
                var point = new Point(i + Dx[k], j + Dy[k]);

                // navigate as long as we are in the board
                if(!(point.X >= 0 && point.X < m &&  point.Y >= 0 && point.Y < n))
                {
                    continue;
                }

                // navigate only on soil
                if(grid[point.X][point.Y] != '1')
                {
                    continue;
                }
                
                grid[point.X][point.Y] = '0';
                points.Enqueue(point);
            }
        }   
    }

    private record Point(int X, int Y);
}

internal static class QueueExtensions
{
    public static void Add<T>(this Queue<T> queue, T element) => queue.Enqueue(element);
}