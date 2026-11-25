public class Solution 
{
    // up, down, left, right, top left, bottom left, top right, bottom right
    private static int[] Dx = {-1,1,0,0,-1,1,-1,1};
    private static int[] Dy = {0,0,-1,1,-1,-1,1,1};
    
    public void GameOfLife(int[][] board) 
    {
        // compute matrix sizez
        int m = board.Length, n = board.FirstOrDefault()?.Length ?? 0;
        
        // codify opeeations 0 -> 0 = -2, 0 -> 1 = -3, 1 -> 0 = 2, 1 -> 1 -> 3
        for(var i = 0; i < m; ++i)
        {
            for(var j = 0; j < n; ++j)
            {
                var cell = board[i][j];
                
                // get number of alive 
                var noAlive = GetAliveNeighbors(i, j, board);
                
                // handle dead cell
                if(cell == 0)
                {
                    // apply condition 4.
                    board[i][j] = -2;
                    if(noAlive == 3)
                    {
                        board[i][j] = -3;
                    }
                    continue;
                }
                
                // handle alive cell
                if(cell == 1)
                {
                     // apply the other conditions
                    board[i][j] = 2;
                    if(noAlive == 2 || noAlive == 3)
                    {
                        board[i][j] = 3;
                    }
                }
            }
        }
        
        for(var i = 0; i < m; ++i)
        {
            for(var j = 0; j < n; ++j)
            {
                board[i][j] = Math.Abs(board[i][j]) % 2;
            } 
        }
        
    }
    
    private static int GetAliveNeighbors(int x, int y, int[][] matrix)
    {
        // compute matrix sizez
        int m = matrix.Length, n = matrix.FirstOrDefault()?.Length ?? 0;
        
        // compute no of alive neighbors
        var alive = 0;
        for(var k = 0; k < 8; ++k)
        {
            var i = x + Dx[k];
            var j = y + Dy[k];
            
            // not valid move
            if(i < 0 || i >= m)
            {
                continue;
            }
            
            // not valid move
            if(j < 0 || j >= n)
            {
                continue;
            }
            
            if(matrix[i][j] > 0)
            {
                ++alive;
            }
        }
        
        return alive;
    }
}