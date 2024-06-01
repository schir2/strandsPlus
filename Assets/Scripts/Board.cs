using UnityEngine;

public class Board : MonoBehaviour
{

    public PuzzleGenerator puzzleGenerator;

    private Row[] rows;
    private int rowIndex;
    private int columnIndex;

    private string word;

    [Header("Tiles")]
    public Tile.State emptyState;
    public Tile.State occupiedState;
    public Tile.State correctState;
    public Tile.State incorrectState;

    private void Awake()
    {
        rows = GetComponentsInChildren<Row>();
    }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {

        char[,] puzzle = puzzleGenerator.GeneratePuzzle(rows.Length, rows[0].tiles.Length);
        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row].tiles.Length; col++)
            {
                rows[row].tiles[col].SetLetter(puzzle[row, col]);
            }
        }

        enabled = true;
    }

    public void TryAgain()
    {
        ClearBoard();

        enabled = true;
    }

    private void Update()
    {
        //TODO Complete This Method
    }

    private void SubmitWord()
    {
        //TODO Complete This Method
    }

    private bool IsValidWord(string word)
    {
        //TODO Complete This Method
        return false;
    }

    private bool HasWon()
    {
        return false;
    }

    private void ClearBoard()
    {
        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row].tiles.Length; col++)
            {
                rows[row].tiles[col].SetLetter('\0');
                rows[row].tiles[col].SetState(emptyState);
            }
        }

        rowIndex = 0;
        columnIndex = 0;
    }

}