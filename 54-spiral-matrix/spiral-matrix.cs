public class Solution 
{
    public IList<int> SpiralOrder(int[][] matrix) 
    {
        var list = new List<int>();

        int m = matrix[0].Length, n = matrix.Length;

        int left = 0, right = m - 1, up = 0, down = n - 1;
        do
        {
            // right
            for(var j = left; j <= right; ++j)
            {
                list.Add(matrix[up][j]);
            }

            // down
            for(var i = up + 1; i <= down; ++i)
            {
                list.Add(matrix[i][right]);
            }

            // left
            if(up != down)
            {
                for(var j = right - 1; j >= left; --j)
                {
                    list.Add(matrix[down][j]);
                }
            }
           
            // up
            if(left != right)
            {
                for(var i = down - 1; i > up; --i)
                {
                    list.Add(matrix[i][left]);
                }
            }
    
            ++left; --right; ++up; --down;
        }while(left <= right && up <= down);
        
        return list;
    }
}