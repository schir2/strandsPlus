using System;
using System.Collections.Generic;
using Data;
using TMPro;
using UI;
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
        public Timer timer;
        public Puzzle puzzle;
        public int numRows;
        public int numCols;
        private Row[] rows;
        public List<Tuple<Tile, Tile.State>> selectedTiles = new();
        private HashSet<Tile> selectedTilesSet = new();

        private bool isDragging = false;
        private bool isRevealingWord = false;
        private int rowIndex;
        private int columnIndex;

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

        private void Awake()
        {
            rows = new Row[numRows];
        }

        private void Start()
        {
            hintButton.onClick.AddListener(OnHintButtonClicked);
        }

        public void SetupBoard(Puzzle newPuzzle)
        {
            puzzle = newPuzzle;
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
                    rowComponent.tiles[col] = Tile.CreateTile(tilePrefab, rowGO.transform, row, col, newPuzzle.data.puzzleGrid[row][col]);
                }
            }

            timer.StartTimer();

        }


        private void Update()
        {

            UpdateHintText();
        }

        public bool HasWon()
        {
            return false;
        }

        public void OnTilePointerDown(Tile tile, PointerEventData eventData)
        {
            isDragging = true;
            selectedTiles.Clear();
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

        public void EndSelection()
        {
            isDragging = false;
            string word = "";
            foreach (var (tile, prevousState) in selectedTiles)
            {
                word += tile.GetLetter();
            }

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

        public void UpdateGameStatusText()
        {
            string word = "";
            foreach (var(tile, prevousState) in selectedTiles)
            {
                word += tile.GetLetter();
            }
            gameStatusText.text = word;
        }

        public void UpdateHintText()
        {
            hintButtonText.text = $"Hints {puzzle.state.hints}";
        }

        public void UpdateGameProgressText()
        {
            gameProgressText.text = $"{puzzle.state.PuzzleWordsFound().ToString()} of {puzzle.data.PuzzleWordsCount().ToString()} words found";
        }

        public void MarkSpangramWord()
        {
            foreach (var(tile, previousState) in selectedTiles)
            {
                tile.SetSpangramState();
            }
        }

        public void ResetSelection()
        {
            foreach (var (tile, previousState) in selectedTiles)
            {
                tile.SetState(previousState);
            }
        }

        public void MarkCorrectWord()
        {
            foreach (var (tile, previousState) in selectedTiles)
            {
                tile.SetCorrectState();
            }
        }



        public void AddTileToSelection(Tile tile)
        {
            if (selectedTilesSet.Contains(tile))
            {


                for (int i = selectedTiles.Count - 1; i > 0; i--)
                {
                    var (selectedTile, prevousState) = selectedTiles[i];
                    if (tile == selectedTile) { break; }
                
                    selectedTile.SetState(prevousState);
                    selectedTiles.RemoveAt(i);
                }
                UpdateGameStatusText();

                return;

            }
            if (selectedTiles.Count > 0)
            {
                var (lastTile, previousState) = selectedTiles[selectedTiles.Count - 1];
                if (!IsAdjacent(lastTile, tile))
                {
                    return;
                }
            }
            selectedTiles.Add(new Tuple<Tile, Tile.State>(tile, tile.currentState));
            UpdateGameStatusText();
            tile.SetSelectedState();
        }

        public bool IsAdjacent(Tile lastTile, Tile newTile)
        {
            int lastRow = lastTile.rowIndex;
            int lastCol = lastTile.colIndex;
            int newRow = newTile.rowIndex;
            int newCol = newTile.colIndex;

            return Mathf.Abs(lastRow - newRow) <= 1 && Mathf.Abs(lastCol - newCol) <= 1;
        }

        public void OnHintButtonClicked()
        {
            UseHint();
        }

        public void UseHint()
        {
            if (!isRevealingWord && puzzle.state.hints > 0)
            {
                List<List<int>> wordPosition = puzzle.RevealWord();
                if (wordPosition != null)
                {
                    HighlightWord(wordPosition);
                }
            }


        }

        public void HighlightWord(List<List<int>> wordPosition)
        {
            for (int i = 0; i < wordPosition.Count; i++)
            {
                rows[wordPosition[i][0]].tiles[wordPosition[i][1]].SetHightlightedState();
            }
        }

    }
}