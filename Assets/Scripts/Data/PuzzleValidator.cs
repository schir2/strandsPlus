using System.Collections.Generic;

namespace Data
{
    public class PuzzleValidator
    {
        private readonly List<List<string>> puzzleGrid;
        private readonly List<string> correctWords;
        private readonly string spangram;
        private List<List<bool>> markedGrid = new();

        private readonly (int dx, int dy)[] df =
        {
            (dx: 0, dy: -1), // Up
            (dx: 0, dy: 1), // Down
            (dx: -1, dy: 0), // Left
            (dx: 1, dy: 0), // Right
            (dx: 1, dy: -1), // UpRight
            (dx: -1, dy: -1), // UpLeft
            (dx: 1, dy: 1), // DownRight
            (dx: -1, dy: 1) // DownLeft
        };

        private readonly int numCols;
        private readonly int numRows;

        public PuzzleValidator(List<List<string>> puzzleGrid, List<string> correctWords, string spangram)
        {
            this.puzzleGrid = puzzleGrid;
            this.correctWords = correctWords;

            this.spangram = spangram;
            numCols = puzzleGrid.Count;
            numRows = puzzleGrid.Count;
            for (var i = 0; i < numRows; i++)
            {
                markedGrid.Add(new List<bool>(new bool[numCols]));
            }
        }

        public bool ValidatePuzzle()
        {
            /* Strands Validator
             * 1. Test Spangram
             * 2. Check for collisions
             * 3. Cannot Start on already used letter
             * 4. Can reuse already used letter
             * 5. All words found
             * 6. All letters used
             * a b b a
             * a l d o
             * d o t s
             */

            if (!ValidateSpangram())
            {
                return false;
            }

            foreach (var word in correctWords)
            {
                for (var row = 0; row < numRows; row++)
                {
                    for (var col = 0; col < numCols; col++)
                    {
                        {
                            Dfs(row, col, word);
                        }
                    }
                }
            }


            return false;
        }

        private bool ValidateSpangram()
        {
            int startingPosition;
            int endingPosition;

            return false;
        }

        private bool Dfs(int row, int col, string targetWord, string builder = "")
        {
            if (targetWord == "")
            {
                return true;
            }

            if (row < 0 || col < 0 || row >= numRows || col >= numCols ||
                puzzleGrid[row][col] != targetWord[0].ToString())
            {
                // Out of bounds or does not contain letter
                return false;
            }

            foreach (var (dy, dx) in df)
            {
                Dfs(row + dy, col + dx, targetWord[1..], builder += targetWord[0]);
            }

            return true;
        }
    }
}