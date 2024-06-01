using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{
    public char[,] GeneratePuzzle(int rows, int cols)
    {
        // Placeholder puzzle for testing
        return new char[,]
        {
            { 's', 'h', 'g', 'g', 's', 'a' },
            { 'a', 'h', 'e', 'm', 'e', 'l' },
            { 'b', 'r', 'e', 'q', 't', 'a' },
            { 'a', 's', 'a', 'u', 'e', 'o' },
            { 'u', 's', 'k', 'f', 'i', 'h' },
            { 'a', 'g', 'e', 'a', 's', 'c' },
            { 'l', 'f', 'n', 'o', 'c', 't' },
            { 'e', 'f', 'a', 'w', 'a', 'b' }
        };
    }
}
