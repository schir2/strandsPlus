using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public TimerText timer;
    private Puzzle puzzle;
    public int numRows;
    public int numCols;
    private Row[] rows;
    public List<Tuple<Tile, Tile.State>> selectedTiles = new();
    private HashSet<Tile> selectedTilesSet = new();

    private bool isDragging = false;
    private bool isRevealingWord = false;
    private int rowIndex;
    private int columnIndex;


    private static Board Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void OnEnable()
    {
        Tile.OnTilePointerDownEvent += OnTilePointerDown;
        Tile.OnTilePointerEnterEvent += OnTilePointerEnter;
        Tile.OnTilePointerUpEvent += OnTilePointerUp;
    }

    private void OnDisable()
    {
        Tile.OnTilePointerDownEvent -= OnTilePointerDown;
        Tile.OnTilePointerEnterEvent -= OnTilePointerEnter;
        Tile.OnTilePointerUpEvent -= OnTilePointerUp;
    }

    private void Start()
    {
        hintButton.onClick.AddListener(OnHintButtonClicked);
    }

    public void InitializeBoard(Puzzle newPuzzle)
    {
        puzzle = newPuzzle;
        CreateBoardLayout();
        timer.StartTimer();
        // themeValueText.text = puzzle.data.theme;
    }

    private void CreateBoardLayout()

    {
        numRows = puzzle.data.puzzleGrid.Count;
        numCols = puzzle.data.puzzleGrid[0].Count;
        rows = new Row[numRows];
        for (var row = 0; row < puzzle.data.puzzleGrid.Count; row++)
        {
            var rowGo = Instantiate(rowPrefab, transform);
            rowGo.transform.localPosition = new Vector3(0, -row, 0);
            rowGo.AddComponent<Row>();
            Row rowComponent = rowGo.GetComponent<Row>();
            rowComponent.tiles = new Tile[numCols];
            rows[row] = rowComponent;
            for (var col = 0; col < numCols; col++)
            {
                rowComponent.tiles[col] = Tile.CreateTile(tilePrefab, rowGo.transform, row, col,
                    puzzle.data.puzzleGrid[row][col]);
            }
        }
    }

    public bool HasWon()
    {
        return false;
    }

    public void OnTilePointerDown(Tile tile, PointerEventData eventData)
    {
        isDragging = true;
        selectedTiles.Clear();
        selectedTilesSet.Clear();
        AddTileToSelection(tile);
    }


    public void OnTilePointerEnter(Tile tile, PointerEventData eventData)
    {
        if (isDragging)
        {
            AddTileToSelection(tile);
        }
    }

    public void OnTilePointerUp(Tile tile, PointerEventData eventData)
    {
        if (isDragging)
        {
            EndSelection();
        }
    }

    private string ConstructWordFromSelectedTiles()
    {
        string word = "";
        foreach (var (tile, prevousState) in selectedTiles)
        {
            word += tile.GetLetter();
        }

        return word;
    }

    private void EndSelection()
    {
        isDragging = false;
        string word = ConstructWordFromSelectedTiles();
        Puzzle.GuessResult wordEvaluation = puzzle.Guess(word);

        ResetSelection();

        if (puzzle.state.wordsGuessed.Contains(word))
        {
            return;
        }

        if (Puzzle.GuessResult.Spangram == wordEvaluation)
        {
            puzzle.state.hints += 1;
            puzzle.state.wordsGuessed.Add(word);
            MarkSpangramWord();
        }

        else if (Puzzle.GuessResult.Correct == wordEvaluation)
        {
            puzzle.state.hints += 1;
            puzzle.state.wordsGuessed.Add(word);
            MarkCorrectWord();
        }
        else if (Puzzle.GuessResult.Valid == wordEvaluation)
        {
            puzzle.state.hints += 1;
            puzzle.state.wordsGuessed.Add(word);
        }

        UpdateGameProgressText();
    }

    private void UpdateGameStatusText()
    {
        string word = ConstructWordFromSelectedTiles();
        gameStatusText.text = word;
    }

    private void UpdateHintText()
    {
        hintButtonText.text = $"Hints {puzzle.state.hints}";
    }

    private void UpdateGameProgressText()
    {
        gameProgressText.text =
            $"{puzzle.state.PuzzleWordsFound().ToString()} of {puzzle.data.PuzzleWordsCount().ToString()} words found";
    }

    private void MarkSpangramWord()
    {
        foreach (var (tile, previousState) in selectedTiles)
        {
            tile.SetSpangramState();
        }
    }

    private void ResetSelection()
    {
        foreach (var (tile, previousState) in selectedTiles)
        {
            tile.SetState(previousState);
        }
    }

    private void MarkCorrectWord()
    {
        foreach (var (tile, previousState) in selectedTiles)
        {
            tile.SetCorrectState();
        }
    }


    private void AddTileToSelection(Tile tile)
    {
        if (selectedTilesSet.Contains(tile))
        {
            for (int i = selectedTiles.Count - 1; i > 0; i--)
            {
                var (selectedTile, prevousState) = selectedTiles[i];

                if (tile == selectedTile)
                {
                    break;
                }

                selectedTile.SetState(prevousState);
                selectedTiles.RemoveAt(i);
                selectedTilesSet.Remove(selectedTile);
            }

            UpdateGameStatusText();

            return;
        }

        if (selectedTiles.Count > 0)
        {
            var (lastTile, previousState) = selectedTiles[^1];
            if (!IsAdjacent(lastTile, tile))
            {
                return;
            }
        }

        selectedTiles.Add(new Tuple<Tile, Tile.State>(tile, tile.CurrentState));
        selectedTilesSet.Add(tile);
        UpdateGameStatusText();
        tile.SetSelectedState();
    }

    private static bool IsAdjacent(Tile lastTile, Tile newTile)
    {
        var lastRow = lastTile.RowIndex;
        var lastCol = lastTile.ColIndex;
        var newRow = newTile.RowIndex;
        var newCol = newTile.ColIndex;

        return Mathf.Abs(lastRow - newRow) <= 1 && Mathf.Abs(lastCol - newCol) <= 1;
    }

    public void OnHintButtonClicked()
    {
        UseHint();
    }

    private void UseHint()
    {
        if (isRevealingWord || puzzle.state.hints <= 0) return;
        var wordPosition = puzzle.RevealWord();
        if (wordPosition != null)
        {
            HighlightWord(wordPosition);
        }
    }

    private void HighlightWord(List<List<int>> wordPosition)
    {
        foreach (var tileAndState in wordPosition)
        {
            rows[tileAndState[0]].tiles[tileAndState[1]].SetHighlightedState();
        }
    }
}