using AdventOfCode.Abstractions;
using AdventOfCode.Puzzles;

namespace AdventOfCode.Tests;

public class Puzzle2UnitTests {
    
#pragma warning disable CA1859
    private IDay2PuzzleSolver _puzzle = null!;
#pragma warning restore CA1859
    
    [SetUp]
    public void Setup() {
        _puzzle = new Day2Puzzle();
    }

    [Test]
    [TestCase("11-22", 2, 33)]
    [TestCase("95-115", 1, 99)]
    [TestCase("998-1012", 1, 1010)]
    [TestCase("1188511880-1188511890", 1, 1188511885)]
    [TestCase("222220-222224", 1, 222222)]
    [TestCase("1698522-1698528", 0, 0)]
    [TestCase("446443-446449", 1, 446446)]
    [TestCase("38593856-38593862", 1, 38593859)]
    [TestCase("11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124", 8, 1227775554)]
    public void TestPartA(string inputRange, long expectedCount, long expectedTotal) {
        var ranges = Day2Puzzle.GetProductIdRanges(inputRange);
        var solution = _puzzle.RunSolutionA(ranges);
        
        using (Assert.EnterMultipleScope()) {
            Assert.That(solution.InvalidCount, Is.EqualTo(expectedCount));
            Assert.That(solution.TotalInvalid, Is.EqualTo(expectedTotal));
        }
    }

    [Test]
    [TestCase("11-22", 2, 33)]
    [TestCase("95-115", 2, 210)]
    [TestCase("998-1012", 2, 2009)]
    [TestCase("1188511880-1188511890", 1, 1188511885)]
    [TestCase("222220-222224", 1, 222222)]
    [TestCase("1698522-1698528", 0, 0)]
    [TestCase("446443-446449", 1, 446446)]
    [TestCase("38593856-38593862", 1, 38593859)]
    [TestCase("565653-565659", 1, 565656)]
    [TestCase("824824821-824824827", 1, 824824824)]
    [TestCase("2121212118-2121212124", 1, 2121212121)]
    [TestCase("11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124", 13, 4174379265)]
    public void TestPartB(string inputRange, long expectedCount, long expectedTotal) {
        var ranges = Day2Puzzle.GetProductIdRanges(inputRange);
        var solution = _puzzle.RunSolutionB(ranges);
        
        using (Assert.EnterMultipleScope()) {
            Assert.That(solution.InvalidCount, Is.EqualTo(expectedCount));
            Assert.That(solution.TotalInvalid, Is.EqualTo(expectedTotal));
        }
    }
}