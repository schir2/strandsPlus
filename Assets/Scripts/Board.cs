using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Board : MonoBehaviour
{

    public GameLogic gameLogic;
    public GameObject rowPrefab;
    public GameObject tilePrefab;
    public TextMeshProUGUI themeValueText;
    public TextMeshProUGUI gameProgressText;
    public TextMeshProUGUI gameStatusText;
    public int numRows;
    public int numCols;
    private Row[] rows;
    private List<Tile> selectedTiles = new List<Tile>();
    private HashSet<Tile> selectedTilesSet = new HashSet<Tile>();

    private bool isDragging = false;
    private int rowIndex;
    private int columnIndex;

    private int hints = 0;

    private void Awake()
    {
        rows = new Row[numRows];
        GameObject themeValueTextObject = GameObject.Find("ThemeValueText");
        if (themeValueTextObject != null)
        {
            themeValueText = themeValueTextObject.GetComponent<TextMeshProUGUI>();
        }
        GameObject gameProgressTextObject = GameObject.Find("GameProgressText");
        if (themeValueTextObject != null)
        {
            gameProgressText = gameProgressTextObject.GetComponent<TextMeshProUGUI>();
        }

        GameObject gameStatusTextObject = GameObject.Find("GameStatusText");
        if (gameStatusTextObject != null)
        {
            gameStatusText = gameStatusTextObject.GetComponent<TextMeshProUGUI>();
        }

        UpdateGameProgressText();
    }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {

        char[,] puzzle = gameLogic.GeneratePuzzle(numRows, numCols);
        themeValueText.text = gameLogic.theme;

        for (int row = 0; row < numRows; row++)
        {
            GameObject rowGO = Instantiate(rowPrefab, transform);
            rowGO.transform.localPosition = new Vector3(0, -row, 0);
            Row rowComponent = rowGO.GetComponent<Row>();
            rowComponent.tiles = new Tile[numCols];
            rows[row] = rowComponent;
            for (int col = 0; col < numCols; col++)
            {
                GameObject tileGO = Instantiate(tilePrefab, rowGO.transform);
                tileGO.transform.localPosition = new Vector3(col, 0, 0);
                Tile tile = tileGO.GetComponent<Tile>();
                tile.SetLetter(puzzle[row, col]);
                tile.rowIndex = row;
                tile.colIndex = col;

                rowComponent.tiles[col] = tile;
            }
        }

    }


    private void Update()
    {
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
        if (isDragging)
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

        GameLogic.WordEvaluation wordEvaluation = gameLogic.Guess(word);

        ClearSelection();

        if (gameLogic.wordsGuessed.Contains(word))
        {
            return;
        }

        if (GameLogic.WordEvaluation.Spangram == wordEvaluation)
        {
            hints += 1;
            gameLogic.wordsGuessed.Add(word);
            MarkSpangramWord();
        }

        else if (GameLogic.WordEvaluation.Correct == wordEvaluation)
        {

            hints += 1;
            gameLogic.wordsGuessed.Add(word);
            MarkCorrectWord();
        }
        else if (GameLogic.WordEvaluation.Valid == wordEvaluation)
        {

            hints += 1;
            gameLogic.wordsGuessed.Add(word);
        }

        UpdateGameProgressText();
    }

    private void UpdateGameStatusText()
    {
        string word = "";
        foreach (Tile tile in selectedTiles)
        {
            word += tile.GetLetter();
        }
        gameStatusText.text = word;
    }

    private void UpdateGameProgressText()
    {
        gameProgressText.text = $"{gameLogic.PuzzleWordsFound().ToString()} of {gameLogic.PuzzleWordsCount().ToString()} words found";
    }

    private void MarkSpangramWord()
    {
        foreach (Tile tile in selectedTiles)
        {
            tile.SetSpangramState();
        }
    }

    private void ClearSelection()
    {
        foreach (Tile tile in selectedTiles)
        {
            tile.SetEmptyState();
        }
    }

    private void MarkCorrectWord()
    {
        foreach (Tile tile in selectedTiles)
        {
            tile.SetCorrectState();
        }
    }



    private void AddTileToSelection(Tile tile)
    {
        if (selectedTiles.Contains(tile))
        {
            int tileIndex = selectedTiles.IndexOf(tile);

            for (int i = selectedTiles.Count - 1; i > tileIndex; i--)
            {
                selectedTiles[i].SetEmptyState();
                selectedTiles.RemoveAt(i);
            }
            UpdateGameStatusText();

            return;

        }
        if (selectedTiles.Count > 0)
        {
            Tile lastTile = selectedTiles[selectedTiles.Count - 1];
            if (!IsAdjacent(lastTile, tile))
            {
                return;
            }
        }
        selectedTiles.Add(tile);
        UpdateGameStatusText();
        tile.SetSelectedState();
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