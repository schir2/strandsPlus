using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class BoardTests
{
    private GameObject boardGameObject;
    private Board board;
    private Puzzle puzzle;



    private static TextMeshProUGUI CreateTextMeshPro(string name)
    {
        var textGameObject = new GameObject(name);
        return textGameObject.AddComponent<TextMeshProUGUI>();
    }

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject for the Board
        boardGameObject = new GameObject();
        board = boardGameObject.AddComponent<Board>();

        // Setup required dependencies (Mocks/Stubs)
        board.rowPrefab = new GameObject();
        board.tilePrefab = new GameObject();
        board.themeValueText = CreateTextMeshPro("ThemeValueText");
        board.gameProgressText = CreateTextMeshPro("GameProgressText");
        board.gameStatusText = CreateTextMeshPro("GameStatusText");
        board.hintButtonText = CreateTextMeshPro("HintButtonText");
        board.hintButton = new GameObject().AddComponent<Button>();
        board.timer = new GameObject().AddComponent<TimerText>();

        // Initialize Puzzle
        puzzle = new Puzzle
        {
            data = new PuzzleData
            {
                theme = "Greek Mythology",
                spangram = "greekletters",
                correctWords = new List<string> { "beta", "sigma", "gamma", "delta", "alpha", "kappa", "epsilon" },
                puzzleGrid = new List<List<string>>
                {
                    new List<string>{ "t", "g", "r", "a", "l", "p" },
                    new List<string>{ "a", "e", "e", "e", "k", "h" },
                    new List<string>{ "b", "a", "m", "m", "l", "a" },
                    new List<string>{ "d", "g", "a", "e", "k", "a" },
                    new List<string>{ "e", "l", "a", "t", "p", "p" },
                    new List<string>{ "s", "t", "e", "t", "a", "l" },
                    new List<string>{ "i", "g", "r", "n", "o", "i" },
                    new List<string>{ "a", "m", "s", "e", "p", "s" }
                }
            }
        };
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(boardGameObject);
    }

    [UnityTest]
    public IEnumerator TestSetupBoard()
    {
        // Act
        board.InitializeBoard(puzzle);

        // Assert
        Assert.AreEqual("Greek Mythology", board.themeValueText.text);
        Assert.AreEqual(8, board.numRows);
        Assert.AreEqual(6, board.numCols);

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestTileSelection()
    {

        // Simulate tile selection
        var tile = Tile.CreateTile(board.tilePrefab, board.transform, 2, 2, "m");

        // Act
        board.OnTilePointerDown(tile, null);
        board.OnTilePointerEnter(tile, null);
        board.OnTilePointerUp(tile, null);

        // Assert
        Assert.Contains(tile, board.selectedTiles);

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestEndSelection()
    {

        var tile1 = Tile.CreateTile(board.tilePrefab, board.transform, 2, 0, "b");
        var tile2 = Tile.CreateTile(board.tilePrefab, board.transform, 2, 1, "e");
        var tile3 = Tile.CreateTile(board.tilePrefab, board.transform, 2, 2, "t");
        var tile4 = Tile.CreateTile(board.tilePrefab, board.transform, 2, 3, "a");

        // Act
        board.OnTilePointerDown(tile1, null);
        board.OnTilePointerEnter(tile2, null);
        board.OnTilePointerEnter(tile3, null);
        board.OnTilePointerEnter(tile4, null);
        board.OnTilePointerUp(tile4, null);

        // Assert
        Assert.Contains(tile1, board.selectedTiles);
        Assert.Contains(tile2, board.selectedTiles);
        Assert.Contains(tile3, board.selectedTiles);
        Assert.Contains(tile4, board.selectedTiles);
        Assert.AreEqual("beta", board.gameStatusText.text);

        yield return null;
    }
}
