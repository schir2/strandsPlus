using System.Collections.Generic;

public class PuzzleData
{
    public string theme;
    public string spangram;
    public List<string> correctWords;
    public Dictionary<string, List<List<int>>> wordPositions;
    public List<List<string>> puzzleGrid;



    public int PuzzleWordsCount()
    {
        return correctWords.Count + 1;
    }
}