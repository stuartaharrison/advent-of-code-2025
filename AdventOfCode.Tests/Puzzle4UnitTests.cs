using AdventOfCode.Abstractions;
using AdventOfCode.Puzzles;

namespace AdventOfCode.Tests;

public class Puzzle4UnitTests {
 
#pragma warning disable CA1859
    private IDay4PuzzleSolver _puzzle = null!;
#pragma warning restore CA1859
    
    [SetUp]
    public void Setup() {
        _puzzle = new Day4Puzzle();
    }

    [Test]
    [TestCase("..@@.@@@@.,@@@.@.@.@@,@@@@@.@.@@,@.@@@@..@.,@@.@@@@.@@,.@@@@@@@.@,.@.@.@.@@@,@.@@@.@@@@,.@@@@@@@@.,@.@.@@@.@.", 13)]
    public void TestPartA(string puzzleInput, int expected) {
        var grid = _puzzle.BuildGrid(puzzleInput);
        var solution = _puzzle.RunSolutionA(grid);
        Assert.That(solution.Total, Is.EqualTo(expected));
    }
    
    [Test]
    [TestCase("..@@.@@@@.,@@@.@.@.@@,@@@@@.@.@@,@.@@@@..@.,@@.@@@@.@@,.@@@@@@@.@,.@.@.@.@@@,@.@@@.@@@@,.@@@@@@@@.,@.@.@@@.@.", 43)]
    public void TestPartB(string puzzleInput, int expected) {
        var grid = _puzzle.BuildGrid(puzzleInput);
        var solution = _puzzle.RunSolutionB(grid);
        Assert.That(solution.Total, Is.EqualTo(expected));
    }
}