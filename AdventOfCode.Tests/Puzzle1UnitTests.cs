using AdventOfCode.Abstractions;
using AdventOfCode.Puzzles;

namespace AdventOfCode.Tests;

public class Puzzle1UnitTests {

#pragma warning disable CA1859
    private IDay1PuzzleSolver _puzzle = null!;
#pragma warning restore CA1859
    
    [SetUp]
    public void Setup() {
        _puzzle = new Day1Puzzle();
    }

    [Test]
    [TestCase(50, "L68,L30,R48,L5,R60,L55,L1,L99,R14,L82", 3)]
    public void TestPartA(int startPosition, string rotationInput, int expectedSolution) {
        var rotations = Day1Puzzle.GetRotations(rotationInput);
        var solution = _puzzle.RunSolution(startPosition, rotations);
        Assert.That(solution.Item1, Is.EqualTo(expectedSolution));
    }

    [Test]
    [TestCase(50, "L68,L30,R48,L5,R60,L55,L1,L99,R14,L82", 6)]
    public void TestPartB(int startPosition, string rotationInput, int expectedSolution) {
        var rotations = Day1Puzzle.GetRotations(rotationInput);
        var solution = _puzzle.RunSolution(startPosition, rotations);
        Assert.That(solution.Item2, Is.EqualTo(expectedSolution));
    }
}