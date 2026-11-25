public class Solution 
{
    public bool IsValidSudoku(char[][] board) 
    {
        // create an array for each col, row and zone
        Dictionary<int, ISet<char>> rows = new(), cols = new(), zones = new();

        for(var i = 0; i < 9; ++i)
        {
            rows[i] = new HashSet<char>();
            cols[i] = new HashSet<char>();
            zones[i] = new HashSet<char>();
        }

        // start traversing the matrix
        for(var i = 0; i < 9; ++i)
        {
            for(var j = 0; j < 9; ++j)
            {
                // ignore empty spaces
                if(board[i][j] == '.')
                {
                    continue;
                }
                
                var c = board[i][j];
                var zone = 3 * (i / 3) + j / 3;

                //row unicity violated
                if(rows[i].Contains(c) || cols[j].Contains(c) || zones[zone].Contains(c))
                {
                    return false;
                }

                rows[i].Add(c);
                cols[j].Add(c);
                zones[zone].Add(c);
            }
        }

        return true;
    }
}