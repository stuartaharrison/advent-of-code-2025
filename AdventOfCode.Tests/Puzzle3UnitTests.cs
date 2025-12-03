using AdventOfCode.Abstractions;
using AdventOfCode.Puzzles;

namespace AdventOfCode.Tests;

public class Puzzle3UnitTests {
    
#pragma warning disable CA1859
    private IDay3PuzzleSolver _puzzle = null!;
#pragma warning restore CA1859
    
    [SetUp]
    public void Setup() {
        _puzzle = new Day3Puzzle();
    }

    [Test]
    [TestCase("987654321111111", 98)]
    [TestCase("811111111111119", 89)]
    [TestCase("234234234234278", 78)]
    [TestCase("818181911112111", 92)]
    public void TestPartA(string inputSequence, long expectedTotal) {
        var solution = _puzzle.RunSolution([inputSequence]);
        Assert.That(solution.TotalVoltage, Is.EqualTo(expectedTotal));
    }

    [Test]
    [TestCase("987654321111111", 987654321111)]
    [TestCase("811111111111119", 811111111119)]
    [TestCase("234234234234278", 434234234278)]
    [TestCase("818181911112111", 888911112111)]
    public void TestPartB(string inputSequence, long expectedTotal) {
        var solution = _puzzle.RunSolution([inputSequence], 12);
        Assert.That(solution.TotalVoltage, Is.EqualTo(expectedTotal));
    }
}