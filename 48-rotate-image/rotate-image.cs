public class Solution 
{
    public void Rotate(int[][] matrix)
    {
        TransposeMatrix(matrix);
        
        foreach(var line in matrix)
        {
            Array.Reverse(line);
        }
    }
    
    private void TransposeMatrix(int[][] matrix)
    {
        int m = matrix.Length;
       
        // first half
        for(var i = 0; i < m; ++i)
        {
            for(var j = i + 1; j < m; ++j)
            {
                (matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j]);
            }
        }       
    }
}