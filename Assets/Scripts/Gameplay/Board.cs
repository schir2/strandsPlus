using System;
using System.Collections.Generic;
using System.Timers;
using Data;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay
{
    public class Board : MonoBehaviour
    {
        public GameObject rowPrefab;
        public GameObject tilePrefab;
        public TextMeshProUGUI themeValueText;
        public TextMeshProUGUI gameProgressText;
        public TextMeshProUGUI gameStatusText;
        public TextMeshProUGUI hintButtonText;
        public Button hintButton;
        public Button pauseButton;
        private Puzzle puzzle;
        public int numRows;
        public int numCols;
        private Row[] rows;
        private readonly List<Tuple<Tile, Tile.State>> selectedTiles = new();
        private readonly HashSet<Tile> selectedTilesSet = new();

        private bool isDragging = false;
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
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }

        private static void OnPauseButtonClicked()
        {
            GameManager.Instance.ChangeState(GameState.Paused);
        }

        public void InitializeBoard(Puzzle newPuzzle)
        {
            puzzle = newPuzzle;
            CreateBoardLayout();
        }

        private void CreateBoardLayout()

        {
            numRows = puzzle.Data.puzzleGrid.Count;
            numCols = puzzle.Data.puzzleGrid[0].Count;
            ResetBoard();
            rows = new Row[numRows];
            for (var row = 0; row < puzzle.Data.puzzleGrid.Count; row++)
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
                        puzzle.Data.puzzleGrid[row][col]);
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
            var word = ConstructWordFromSelectedTiles();
            var wordEvaluation = puzzle.Guess(word);

            ResetSelection();


            switch (wordEvaluation)
            {
                case Puzzle.GuessResult.Spangram:
                    MarkSpangramWord();
                    break;
                case Puzzle.GuessResult.Correct:
                    MarkCorrectWord();
                    break;
                case Puzzle.GuessResult.Valid:
                    break;
                case Puzzle.GuessResult.Invalid:
                    break;
            }

            UpdateHintText();
            UpdateGameProgressText();
        }

        private void UpdateGameStatusText()
        {
            string word = ConstructWordFromSelectedTiles();
            gameStatusText.text = word;
        }

        private void UpdateHintText()
        {
            hintButtonText.text = $"Hints {puzzle.State.Hints}";
        }

        private void UpdateGameProgressText()
        {
            gameProgressText.text =
                $"{puzzle.State.PuzzleWordsFound().ToString()} of {puzzle.Data.PuzzleWordsCount().ToString()} words found";
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
            if (puzzle.State.UseHint())
            {
                var wordPosition = puzzle.RevealWord();
                if (wordPosition != null)
                {
                    HighlightWord(wordPosition);
                }
            }

            UpdateHintText();
        }

        private void HighlightWord(List<List<int>> wordPosition)
        {
            foreach (var tileAndState in wordPosition)
            {
                rows[tileAndState[0]].tiles[tileAndState[1]].SetHighlightedState();
            }
        }

        private void Update()
        {
            if (puzzle == null) return;

            if (GameModeManager.Instance.CurrentGameMode.CheckWinCondition(puzzle))
            {
                GameManager.Instance.ChangeState(GameState.Won);
            }

            if (GameModeManager.Instance.CurrentGameMode.CheckLoseCondition(puzzle))
            {
                GameManager.Instance.ChangeState(GameState.Lost);
            }
        }

        private void ResetBoard()
        {
            if (rows == null)
            {
                return;
            }

            if (puzzle != null)
            {
                selectedTiles.Clear();
                themeValueText.text = puzzle.Data.theme;
                UpdateGameProgressText(); 
                UpdateGameStatusText();
                UpdateHintText();
            }

            foreach (var row in rows)
            {
                Destroy(row.gameObject);
            }
        }
    }
}