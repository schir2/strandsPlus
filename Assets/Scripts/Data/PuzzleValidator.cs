using System.Collections.Generic;

namespace Data
{
    public class PuzzleValidator
    {
        public static bool ValidatePuzzle(List<List<string>> puzzleGrid, List<string> correctWords, string spangram)
        {
            var numCols = puzzleGrid.Count;
            var numRows = puzzleGrid.Count;

            for (var row = 0; row < numRows; row++)
            {
                for (var col = 0; col < numCols; col++)
                {
                    {
                        Dfs(puzzleGrid, row, col);
                    }
                }
            }

            return false;
        }

        private static void Dfs(List<List<string>> puzzleGrid, int row, int col)
        {
        }
    }
}