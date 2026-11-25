public class Solution 
{
    /**
     * <summary>
     * Lays <paramref name="s"/> out in a zigzag across <paramref name="numRows"/> rows and
     * returns those rows read back top to bottom.
     * </summary>
     * <remarks>
     * Characters run down the rows to the bottom, then diagonally back up to the top, and the
     * pattern repeats. That grid is mostly empty, so rather than materialising it each row keeps
     * its own buffer and every character is appended to whichever row the walk currently sits on.
     * The walk is a single signed step that inverts whenever the next row would fall outside the
     * grid, so a turn costs one extra loop pass but never consumes a character. The rows are then
     * folded into the first buffer so no separate builder is needed for the result.
     * </remarks>
     * <param name="s">The string to lay out. May be empty.</param>
     * <param name="numRows">The number of rows to zigzag across. Must be at least 1.</param>
     * <returns>The rows of the zigzag layout, concatenated top to bottom.</returns>
     */
    public string Convert(string s, int numRows)
    {
        // Handle edge case
        if (numRows == 1 || numRows >= s.Length)
        {
            return s;
        }

        // Build an array of StringBuilder objects to hold the characters for each row
        StringBuilder[] rows = new StringBuilder[numRows];
        for (int i = 0; i < numRows; i++)
        {
            rows[i] = new StringBuilder();
        }
        
        // Initialize variables to track the current index, direction, and row
        int idx = 0, direction = 1, row = -1;
        while (idx < s.Length)
        {
            // Handle the case in which we're outside the top/bottom of the matrix, we move zigzag downwards/upwards
            if (row + direction < 0 || row + direction >= numRows)
            {
                // Change direction
                direction = -direction;
                continue;
            }

            rows[row += direction].Append(s[idx++]);
        }

        // Build the final result by concatenating the strings from each row
        for (row = 1; row < numRows; row++)
        {
            rows[0].Append(rows[row]);
        }

        return rows[0].ToString();
    }
}