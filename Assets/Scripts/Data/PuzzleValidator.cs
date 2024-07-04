using System;
using System.Collections.Generic;
using System.Linq;

public class CollisionException : Exception
{
    public CollisionException(string message) : base(message)
    {
    }
}

public class InvalidSpangramException : Exception
{
    public InvalidSpangramException(string message) : base(message)
    {
    }
}

public class CannotFindWordException : Exception
{
    public CannotFindWordException() : base("Word cannot be found")
    {
    }
}

public class InvalidTotalWordLengthException : Exception
{
    public InvalidTotalWordLengthException() : base("The sum of all word lengths does not equal the total grid size")
    {
    }
}

namespace Data
{
    public class PuzzleValidator
    {
        private readonly List<List<char>> grid;
        private readonly List<string> correctWords;
        private Dictionary<string, List<(int, int)>> wordPaths = new();
        private Dictionary<string, List<(int, int)>> sortedWordPaths = new();

        private HashSet<(int, int)> markedGrid = new();

        private bool CollisionFound = false;
        private readonly string spangram;

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

        private readonly int cols;
        private readonly int rows;

        public PuzzleValidator(List<List<char>> grid, List<string> correctWords, string spangram)
        {
            this.grid = grid;
            this.correctWords = correctWords;

            this.spangram = spangram;
            rows = grid.Count;
            cols = grid[0].Count;
        }

        private (int row, int col)[] SortPath((int row, int col)[] path)
        {
            Array.Sort(path, (p1, p2) =>
            {
                int comparison = p1.row.CompareTo(p2.row);
                return comparison == 0 ? p1.col.CompareTo(p2.col) : comparison;
            });
            return path;
        }

        public bool FindWord(string word)
        {
            var wordFound = false;
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    if (word[0] != grid[row][col]) continue;
                    if (Dfs(row, col, word, new List<(int, int)>(), ""))
                    {
                        wordFound = true;
                    }
                }
            }

            if (!wordFound) throw new CannotFindWordException();
            foreach (var coords in wordPaths[word])
            {
                markedGrid.Add(coords);
                return true;
            }

            throw new CannotFindWordException();
        }


        private bool Dfs(int row, int col, string targetWord, List<(int, int)> path, string builder)
        {
            if (string.IsNullOrEmpty(targetWord))
            {
                if (sortedWordPaths.ContainsKey(builder))
                {
                    path.Sort();
                    if (!sortedWordPaths[builder].SequenceEqual(path))
                    {
                        throw new CollisionException(string.Join(",", path));
                    }
                }

                wordPaths[builder] = path;
                path.Sort();
                sortedWordPaths[builder] = path;
                return true;
            }

            if (markedGrid.Contains((row, col)) || row < 0 || row >= rows || col < 0 || col >= cols ||
                path.Contains((row, col)) || grid[row][col] != targetWord[0])
            {
                return false;
            }

            var result = false;
            foreach (var (drow, dcol) in df)
            {
                result = result || Dfs(row + drow, col + dcol, targetWord[1..],
                    new List<(int, int)>(path) { (row, col) }, builder + grid[row][col]);
            }

            return result;
        }

        public void ValidateTotalWordLength()
        {
            var totalLength = correctWords.Sum(word => word.Length) + spangram.Length;
            var gridLength = rows * cols;
            if (totalLength != gridLength)
            {
                throw new InvalidTotalWordLengthException();
            }
        }

        public void ValidatePuzzle()
        {
            ValidateTotalWordLength();
            ValidateSpangram();

            foreach (var word in correctWords)
            {
                FindWord(word);
            }
        }

        public void ValidateSpangram()
        {
            try
            {
                FindWord(spangram);
            }
            catch (CannotFindWordException)
            {
                throw new InvalidSpangramException("Spangram word was not found");
            }

            try
            {
                if (!WordTouchesOppositeSides(wordPaths[spangram]))
                {
                    throw new InvalidSpangramException("Spangram word does not touch opposite sides");
                }
            }
            catch (KeyNotFoundException)

            {
                throw new InvalidSpangramException("Spangram word does not touch opposite sides");
            }
        }

        public bool WordTouchesOppositeSides(List<(int, int)> path)
        {
            var first = path.First();
            var last = path.Last();

            var touchesOppositeSides =
                ((first.Item1 == 0 && last.Item1 == rows - 1) || (first.Item1 == rows - 1 && last.Item1 == 0)) ||
                ((first.Item2 == 0 && last.Item2 == cols - 1) || (first.Item2 == cols - 1 && last.Item2 == 0));

            return touchesOppositeSides;
        }
    }
}