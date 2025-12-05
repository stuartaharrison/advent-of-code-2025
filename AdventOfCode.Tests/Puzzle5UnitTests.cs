using AdventOfCode.Abstractions;
using AdventOfCode.Puzzles;

namespace AdventOfCode.Tests;

public class Puzzle5UnitTests {
    
#pragma warning disable CA1859
    private IDay5PuzzleSolver _puzzle = null!;
#pragma warning restore CA1859
    
    [SetUp]
    public void Setup() {
        _puzzle = new Day5Puzzle();
    }

    [Test]
    [TestCase("3-5,10-14,16-20,12-18", "1,5,8,11,17,32", 3)]
    public void TestPartA(string freshInputRange, string availableInputRange, int expectedResult) {
        var freshIngredientRange = freshInputRange.Split(',');
        var availableIngredientRange = availableInputRange.Split(',').Select(long.Parse).ToArray();
        var puzzleInput = new Day5Puzzle.PuzzleInput() {
            FreshIngredientRanges = freshIngredientRange,
            AvailableIngredients = availableIngredientRange
        };
        
        var solution = _puzzle.RunSolutionA(puzzleInput);
        Assert.That(solution.SpoiledCount, Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase("3-5,10-14,16-20,12-18", 14)]
    public void TestPartB(string freshInputRange, int expectedResult) {
        var freshIngredientRange = freshInputRange.Split(',');
        var puzzleInput = new Day5Puzzle.PuzzleInput() {
            FreshIngredientRanges = freshIngredientRange,
            AvailableIngredients = []
        };
        
        var solution = _puzzle.RunSolutionB(puzzleInput);
        Assert.That(solution.TotalFreshIngredients, Is.EqualTo(expectedResult));
    }
}