using System.Collections;
using System.Collections.Generic;
using Data;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PuzzleTests
{

    private Puzzle puzzle;
    private PuzzleData puzzleData;

    [SetUp]
    public void Setup()
    {
        HashSet<string> validWords = new HashSet<string> { "valid" };
        puzzleData = new PuzzleData
        {
            theme = "Greek Mythology",
            spangram = "greekletters",
            correctWords = new List<string> { "beta", "sigma", "gamma", "delta", "alpha", "kappa", "epsilon" },
            puzzleGrid = new List<List<string>>
            {
                new List<string> { "t", "g", "r", "a", "l", "p" },
                new List<string> { "a", "e", "e", "e", "k", "h" },
                new List<string> { "b", "a", "m", "m", "l", "a" },
                new List<string> { "d", "g", "a", "e", "k", "a" },
                new List<string> { "e", "l", "a", "t", "p", "p" },
                new List<string> { "s", "t", "e", "t", "a", "l" },
                new List<string> { "i", "g", "r", "n", "o", "i" },
                new List<string> { "a", "m", "s", "e", "p", "s" }
            },
            wordPositions = new Dictionary<string, List<List<int>>>
            {
                { "beta", new List<List<int>> { new List<int> { 2, 0 }, new List<int> { 2, 1 }, new List<int> { 2, 2 }, new List<int> { 2, 3 } } },
                { "sigma", new List<List<int>> { new List<int> { 1, 0 }, new List<int> { 2, 0 }, new List<int> { 3, 0 }, new List<int> { 4, 0 }, new List<int> { 5, 0 } } },
                { "gamma", new List<List<int>> { new List<int> { 2, 2 }, new List<int> { 2, 3 }, new List<int> { 2, 4 }, new List<int> { 2, 5 }, new List<int> { 2, 6 } } },
                { "delta", new List<List<int>> { new List<int> { 3, 0 }, new List<int> { 3, 1 }, new List<int> { 3, 2 }, new List<int> { 3, 3 }, new List<int> { 3, 4 } } },
                { "alpha", new List<List<int>> { new List<int> { 4, 0 }, new List<int> { 4, 1 }, new List<int> { 4, 2 }, new List<int> { 4, 3 }, new List<int> { 4, 4 } } },
                { "kappa", new List<List<int>> { new List<int> { 5, 0 }, new List<int> { 5, 1 }, new List<int> { 5, 2 }, new List<int> { 5, 3 }, new List<int> { 5, 4 } } },
                { "epsilon", new List<List<int>> { new List<int> { 6, 0 }, new List<int> { 6, 1 }, new List<int> { 6, 2 }, new List<int> { 6, 3 }, new List<int> { 6, 4 }, new List<int> { 6, 5 } } }
            }
        };

        puzzle = new Puzzle();
        puzzle.Init(puzzleData, validWords);
    }

    [Test]
    public void TestInit()
    {
        Assert.AreEqual("Greek Mythology", puzzle.data.theme);
        Assert.AreEqual("greekletters", puzzle.data.spangram);
        Assert.AreEqual(7, puzzle.data.correctWords.Count);
    }

    [Test]
    public void TestPuzzleWordsCount()
    {
        int count = puzzle.data.PuzzleWordsCount();
        Assert.AreEqual(8, count); // 7 correct words + 1 spangram
    }

    [Test]
    public void TestPuzzleWordsFound()
    {
        int found = puzzle.state.PuzzleWordsFound();
        Assert.AreEqual(0, found);

        puzzle.Guess("beta");
        found = puzzle.state.PuzzleWordsFound();
        Assert.AreEqual(1, found);

        puzzle.Guess("greekletters");
        found = puzzle.state.PuzzleWordsFound();
        Assert.AreEqual(2, found);
    }

    [Test]
    public void TestIsSpangramWord()
    {
        Assert.IsTrue(puzzle.IsSpangramWord("greekletters"));
        Assert.IsFalse(puzzle.IsSpangramWord("notspangram"));
    }

    [Test]
    public void TestIsCorrectdWord()
    {
        Assert.IsTrue(puzzle.IsCorrectdWord("beta"));
        Assert.IsFalse(puzzle.IsCorrectdWord("incorrect"));
    }

    [Test]
    public void TestIsValidWord()
    {
        Assert.IsTrue(puzzle.IsValidWord("valid"));
        Assert.IsFalse(puzzle.IsValidWord("invalid"));
    }

    [Test]
    public void TestGuess()
    {
        Assert.AreEqual(puzzle.Guess("beta"), Puzzle.GuessResult.Correct);
        Assert.AreEqual(puzzle.Guess("betax"), Puzzle.GuessResult.Invalid);
        Assert.AreEqual(puzzle.Guess("greekletters"), Puzzle.GuessResult.Spangram);
        Assert.AreEqual(puzzle.Guess("valid"), Puzzle.GuessResult.Valid);
    }

    [Test]
    public void TestRevealWord()
    {
        puzzle.state.hints = 0;
        Assert.AreEqual(null, puzzle.RevealWord());
        Assert.AreEqual(puzzle.state.hints, 0);

        puzzle.state.hints++;
        Assert.AreEqual(puzzle.data.wordPositions[puzzle.data.correctWords[0]],puzzle.RevealWord());
        Assert.AreEqual(puzzle.state.hints, 0);

        puzzle.Guess(puzzle.data.correctWords[0]);
        Assert.AreEqual(puzzle.state.hints, 0);
        Assert.AreEqual(null, puzzle.RevealWord());
        puzzle.state.hints++;
        Assert.AreEqual(puzzle.data.wordPositions[puzzle.data.correctWords[1]], puzzle.RevealWord());
    }


}
