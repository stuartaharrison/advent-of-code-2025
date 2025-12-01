using AdventOfCode.Puzzles;

namespace AdventOfCode.Abstractions;

public interface IDay1PuzzleSolver : IPuzzleSolver {

    Tuple<int, int> RunSolution(int startPosition, IEnumerable<Day1Puzzle.Rotation> rotations, int minValue = 0, int maxValue = 99);
}