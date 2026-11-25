public class Solution 
{
    public void SetZeroes(int[][] matrix) 
    {
        // get the total length
        int m = matrix.Length, n = matrix.FirstOrDefault()?.Length ?? 0;
        
        // because we do the markings in place initially we need to see if there is a 0 on first row / column
        var zeroOnFirstRow= matrix[0].Any(x => x == 0);
        
        var zeroOnFirstCol = matrix.Any(x => x[0] == 0);
        
        // mark row and cols which contain a 0
        for(var i = 0; i < m; ++i)
        {
            for(var j = 0; j < n; ++j)
            {
                if(matrix[i][j] != 0)
                {
                    continue;
                }
                
                matrix[0][j] = matrix[i][0] = 0;
            }
        }
        
    
        // mark all the elements with 0
        for(var i = 1; i < m; ++i)
        {
            for(var j = 1; j < n; ++j)
            {
                // curent element should not be changed since there is no zero element on that lind
                if(matrix[i][0] != 0 && matrix[0][j] != 0)
                {
                    continue;
                }

                matrix[i][j] = 0;
            }
        }
        
        if(zeroOnFirstRow)
        {
            for(var j = 0; j < n; ++j)
            {
                matrix[0][j] = 0;
            }
        }
        
        if(zeroOnFirstCol)
        {
            for(var i = 0; i < m; ++i)
            {
                matrix[i][0] = 0;
            }
        }
    }
}