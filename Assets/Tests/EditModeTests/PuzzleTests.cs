using System.Collections.Generic;
using Data;
using NUnit.Framework;

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
                new() { "t", "g", "r", "a", "l", "p" },
                new() { "a", "e", "e", "e", "k", "h" },
                new() { "b", "a", "m", "m", "l", "a" },
                new() { "d", "g", "a", "e", "k", "a" },
                new() { "e", "l", "a", "t", "p", "p" },
                new() { "s", "t", "e", "t", "a", "l" },
                new() { "i", "g", "r", "n", "o", "i" },
                new() { "a", "m", "s", "e", "p", "s" }
            },
            wordPositions = new Dictionary<string, List<List<int>>>
            {
                {
                    "beta",
                    new List<List<int>>
                    {
                        new() { 2, 0 }, new() { 2, 1 }, new() { 2, 2 }, new() { 2, 3 }
                    }
                },
                {
                    "sigma",
                    new List<List<int>>
                    {
                        new() { 1, 0 }, new() { 2, 0 }, new() { 3, 0 }, new() { 4, 0 },
                        new() { 5, 0 }
                    }
                },
                {
                    "gamma",
                    new List<List<int>>
                    {
                        new() { 2, 2 }, new() { 2, 3 }, new() { 2, 4 }, new() { 2, 5 },
                        new() { 2, 6 }
                    }
                },
                {
                    "delta",
                    new List<List<int>>
                    {
                        new() { 3, 0 }, new() { 3, 1 }, new() { 3, 2 }, new() { 3, 3 },
                        new() { 3, 4 }
                    }
                },
                {
                    "alpha",
                    new List<List<int>>
                    {
                        new() { 4, 0 }, new() { 4, 1 }, new() { 4, 2 }, new() { 4, 3 },
                        new() { 4, 4 }
                    }
                },
                {
                    "kappa",
                    new List<List<int>>
                    {
                        new() { 5, 0 }, new() { 5, 1 }, new() { 5, 2 }, new() { 5, 3 },
                        new() { 5, 4 }
                    }
                },
                {
                    "epsilon",
                    new List<List<int>>
                    {
                        new() { 6, 0 }, new() { 6, 1 }, new() { 6, 2 }, new() { 6, 3 },
                        new() { 6, 4 }, new() { 6, 5 }
                    }
                }
            }
        };

        puzzle = new Puzzle(puzzleData, validWords);
    }

    [Test]
    public void TestInit()
    {
        Assert.AreEqual("Greek Mythology", puzzle.Data.theme);
        Assert.AreEqual("greekletters", puzzle.Data.spangram);
        Assert.AreEqual(7, puzzle.Data.correctWords.Count);
    }

    [Test]
    public void TestPuzzleWordsCount()
    {
        int count = puzzle.Data.PuzzleWordsCount();
        Assert.AreEqual(8, count); // 7 correct words + 1 spangram
    }

    [Test]
    public void TestPuzzleWordsFound()
    {
        int found = puzzle.State.PuzzleWordsFound();
        Assert.AreEqual(0, found);

        puzzle.Guess("beta");
        found = puzzle.State.PuzzleWordsFound();
        Assert.AreEqual(1, found);

        puzzle.Guess("greekletters");
        found = puzzle.State.PuzzleWordsFound();
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
        puzzle.State.Hints = 0;
        Assert.AreEqual(null, puzzle.RevealWord());
        Assert.AreEqual(puzzle.State.Hints, 0);

        puzzle.State.Hints++;
        Assert.AreEqual(puzzle.Data.wordPositions[puzzle.Data.correctWords[0]], puzzle.RevealWord());
        Assert.AreEqual(puzzle.State.Hints, 0);

        puzzle.Guess(puzzle.Data.correctWords[0]);
        Assert.AreEqual(puzzle.State.Hints, 0);
        Assert.AreEqual(null, puzzle.RevealWord());
        puzzle.State.Hints++;
        Assert.AreEqual(puzzle.Data.wordPositions[puzzle.Data.correctWords[1]], puzzle.RevealWord());
    }
}