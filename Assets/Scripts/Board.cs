using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject rowPrefab;
    public GameObject tilePrefab;
    public TextMeshProUGUI themeValueText;
    public TextMeshProUGUI gameProgressText;
    public TextMeshProUGUI gameStatusText;
    public TextMeshProUGUI hintButtonText;
    public Button hintButton;
    public Timer timer;
    public Puzzle puzzle;
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
    }

    private void Start()
    {
    }

    public void SetupBoard(Puzzle puzzle)
    {
        themeValueText.text = puzzle.data.theme;

        for (int row = 0; row < numRows; row++)
        {
            GameObject rowGO = Instantiate(rowPrefab, transform);
            rowGO.transform.localPosition = new Vector3(0, -row, 0);
            Row rowComponent = rowGO.GetComponent<Row>();
            rowComponent.tiles = new Tile[numCols];
            rows[row] = rowComponent;
            for (int col = 0; col < numCols; col++)
            {
                rowComponent.tiles[col] = Tile.CreateTile(tilePrefab, rowGO.transform, row, col, puzzle.data.puzzleGrid[row][col]);
            }
        }

        timer.StartTimer();

    }


    private void Update()
    {

        UpdateHintText();
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

        Puzzle.GuessResult wordEvaluation = puzzle.Guess(word);

        ClearSelection();

        if (puzzle.state.wordsGuessed.Contains(word))
        {
            return;
        }

        if (Puzzle.GuessResult.Spangram == wordEvaluation)
        {
            hints += 1;
            puzzle.state.wordsGuessed.Add(word);
            MarkSpangramWord();
        }

        else if (Puzzle.GuessResult.Correct == wordEvaluation)
        {

            hints += 1;
            puzzle.state.wordsGuessed.Add(word);
            MarkCorrectWord();
        }
        else if (Puzzle.GuessResult.Valid == wordEvaluation)
        {

            hints += 1;
            puzzle.state.wordsGuessed.Add(word);
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

    private void UpdateHintText()
    {
        hintButtonText.text = $"Hints {puzzle.state.hints}";
    }

    private void UpdateGameProgressText()
    {
        gameProgressText.text = $"{puzzle.state.PuzzleWordsFound().ToString()} of {puzzle.data.PuzzleWordsCount().ToString()} words found";
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