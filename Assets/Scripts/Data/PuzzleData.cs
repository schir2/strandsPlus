using System.Collections.Generic;

namespace Data
{
    public class 
        
        PuzzleData
    {
        public string id;
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
}