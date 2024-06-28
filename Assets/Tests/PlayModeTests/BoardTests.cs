using System.Collections;
using System.Collections.Generic;
using Data;
using Gameplay;
using NUnit.Framework;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

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
        board.timer = new GameObject().AddComponent<Timer>();
    }

    private Puzzle SetUpPuzzle()
    {
        var puzzleData = new PuzzleData
        {
            theme = "Greek Mythology",
            spangram = "greekletters",
            correctWords = new List<string> { "beta", "sigma", "gamma", "delta", "alpha", "kappa", "epsilon" },
            puzzleGrid = new List<List<string>>
            {
                new() { "t", "g", "r", "a", "l", "p" },
                new() { "a", "e", "e", "e", "k", "h" },
                new() { "b", "a", "m", "m", "l", "a" },
                new() { "d", "g", "a", "e", "k", "a" },
                new() { "e", "l", "a", "t", "p", "p" },
                new() { "s", "t", "e", "t", "a", "l" },
                new() { "i", "g", "r", "n", "o", "i" },
                new() { "a", "m", "s", "e", "p", "s" }
            }
        };
        puzzle = new Puzzle(puzzleData, new HashSet<string> { "valid" });
        return puzzle;
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
        board.InitializeBoard(SetUpPuzzle());

        // Assert
        Assert.AreEqual("Greek Mythology", board.themeValueText.text);
        Assert.AreEqual(8, board.numRows);
        Assert.AreEqual(6, board.numCols);

        yield return null;
    }
}