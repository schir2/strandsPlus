using UnityEngine;
using System.Collections.Generic;
using System;

public class Board : MonoBehaviour
{

    public GameLogic gameLogic;
    public GameObject rowPrefab;
    public GameObject tilePrefab;
    public int numRows;
    public int numCols;
    private Row[] rows;
    private List<Tile> selectedTiles = new List<Tile>();

    [Header("Tiles")]
    public Tile.State emptyState;
    public Tile.State selectedState;
    public Tile.State correctState;
    public Tile.State validState;

    private bool isDragging = false;
    private int rowIndex;
    private int columnIndex;

    private string word;

    private void Awake()
    {

        numRows = 8;
        numCols = 6;
        rows = new Row[numRows];
    }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {

        // Generate puzzle
        char[,] puzzle = gameLogic.GeneratePuzzle(numRows, numCols);

        // Instantiate rows and attach tiles
        for (int row = 0; row < numRows; row++)
        {
            // Instantiate row prefab
            GameObject rowGO = Instantiate(rowPrefab, transform);

            // Set row position
            rowGO.transform.localPosition = new Vector3(0, -row, 0);

            // Get Row component from the instantiated row GameObject
            Row rowComponent = rowGO.GetComponent<Row>();

            // Add the row to the rows array
            rows[row] = rowComponent;

            // Instantiate tiles and attach them to the row
            for (int col = 0; col < numCols; col++)
            {
                // Instantiate tile prefab
                GameObject tileGO = Instantiate(tilePrefab, rowGO.transform);

                // Set tile position
                tileGO.transform.localPosition = new Vector3(col, 0, 0);

                // Get Tile component from the instantiated tile GameObject
                Tile tile = tileGO.GetComponent<Tile>();

                // Set letter for the tile
                tile.SetLetter(puzzle[row, col]);

                // Set row and column indices
                tile.rowIndex = row;
                tile.colIndex = col;

                // Add the tile to the corresponding row
                rowComponent.tiles[col] = tile;
            }
        }

    }


    private void Update()
    {
    }

    private bool IsCorrectWord(string word)
    {
        throw new NotImplementedException();
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

    internal void OnTilePointerDown(Tile tile)
    {
        isDragging = true;
        selectedTiles.Clear();
        AddTileToSelection(tile);
    }


    internal void OnTilePointerEnter(Tile tile)
    {
        if (isDragging && !selectedTiles.Contains(tile))
        {
            AddTileToSelection(tile);
        }
    }

    internal void OnTilePointerUp()
    {
        if (isDragging)
        {
            EndSelection();
        }
    }

    private void EndSelection()
    {
        isDragging = false;
        string word = "";
        foreach (Tile tile in selectedTiles)
        {
            word += tile.GetLetter();
        }

        Debug.Log(word);
        Debug.Log(gameLogic.EvaluateWord(word));

    }

    private void AddTileToSelection(Tile tile)
    {
        if (selectedTiles.Count > 0)
        {
            Tile lastTile = selectedTiles[selectedTiles.Count - 1];
            if (!IsAdjacent(lastTile, tile))
            {
                return;
            }
        }
        selectedTiles.Add(tile);
        tile.SetState(selectedState);
    }

    private bool IsAdjacent(Tile lastTile, Tile newTile)
    {
        int lastRow = lastTile.rowIndex;
        int lastCol = lastTile.colIndex;
        int newRow = newTile.rowIndex;
        int newCol = newTile.colIndex;

        return Mathf.Abs(lastRow - newRow) <= 1 && Mathf.Abs(lastCol - newCol) <= 1;
    }

}