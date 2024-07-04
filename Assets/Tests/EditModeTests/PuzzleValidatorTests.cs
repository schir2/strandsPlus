using System.Collections.Generic;
using Data;
using NUnit.Framework;

[TestFixture]
public class PuzzleValidatorTests
{
    private List<List<char>> grid;
    private string spangram;
    private List<string> correctWords;
    private PuzzleValidator puzzleValidator;

    [SetUp]
    public void Setup()
    {
        grid = new List<List<char>>
        {
            new List<char> { 't', 'g', 'r', 'a', 'l', 'p' },
            new List<char> { 'a', 'e', 'e', 'e', 'k', 'h' },
            new List<char> { 'b', 'a', 'm', 'm', 'l', 'a' },
            new List<char> { 'd', 'g', 'a', 'e', 'k', 'a' },
            new List<char> { 'e', 'l', 'a', 't', 'p', 'p' },
            new List<char> { 's', 't', 'e', 't', 'a', 'l' },
            new List<char> { 'i', 'g', 'r', 'n', 'o', 'i' },
            new List<char> { 'a', 'm', 's', 'e', 'p', 's' }
        };

        spangram = "greekletters";
        correctWords = new List<string> { "beta", "sigma", "gamma", "delta", "alpha", "kappa", "epsilon" };
        puzzleValidator = new PuzzleValidator(grid, correctWords, spangram);
    }

    [Test]
    public void ValidateTotalWordLength_ShouldNotThrow_WhenWordLengthsAreValid()
    {
        Assert.DoesNotThrow(() => puzzleValidator.ValidateTotalWordLength());
    }

    [Test]
    public void ValidateTotalWordLength_ShouldThrow_WhenWordLengthsAreInvalid()
    {
        var incorrectWords = new List<string> { "short", "list" };
        var invalidValidator = new PuzzleValidator(grid, incorrectWords, spangram);

        Assert.Throws<InvalidTotalWordLengthException>(() => invalidValidator.ValidateTotalWordLength());
    }

    [Test]
    public void ValidateSpangram_ShouldNotThrow_WhenSpangramIsValid()
    {
        Assert.DoesNotThrow(() => puzzleValidator.ValidateSpangram());
    }

    [Test]
    public void ValidateSpangram_ShouldThrow_WhenSpangramIsInvalid()
    {
        var invalidSpangram = "invalidspangram";
        var invalidValidator = new PuzzleValidator(grid, correctWords, invalidSpangram);

        Assert.Throws<InvalidSpangramException>(() => invalidValidator.ValidateSpangram());
    }

    [Test]
    public void ValidatePuzzle_ShouldNotThrow_WhenPuzzleIsValid()
    {
        Assert.DoesNotThrow(() => puzzleValidator.ValidatePuzzle());
    }

    [Test]
    public void FindWord_ShouldReturnTrue_WhenWordIsFound()
    {
        Assert.IsTrue(puzzleValidator.FindWord("beta"));
    }

    [Test]
    public void FindWord_ShouldThrow_WhenWordIsNotFound()
    {
        Assert.Throws<CannotFindWordException>(() => puzzleValidator.FindWord("nonexistent"));
    }

    [Test]
    public void WordTouchesOppositeSides_ShouldReturnFalse_WhenPathDoesNotTouchEdges()
    {
        var invalidValidator = new PuzzleValidator(grid, correctWords, spangram);

        // Create a path that does not touch opposite sides
        var path = new List<(int, int)>
        {
            (3, 3), (3, 4), (3, 5) // Assume a path somewhere in the middle of the grid
        };

        Assert.IsFalse(invalidValidator.WordTouchesOppositeSides(path));
    }

    [Test]
    public void WordTouchesOppositeSides_ShouldReturnTrue_WhenPathTouchesEdges()
    {
        var validValidator = new PuzzleValidator(grid, correctWords, spangram);

        // Create a path that touches opposite sides
        var path = new List<(int, int)>
        {
            (0, 0), (1, 1), (grid.Count - 1, grid[0].Count - 1) // Assume a path from one edge to another
        };

        Assert.IsTrue(validValidator.WordTouchesOppositeSides(path));
    }
}