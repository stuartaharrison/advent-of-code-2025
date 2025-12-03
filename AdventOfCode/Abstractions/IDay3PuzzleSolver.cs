using AdventOfCode.Puzzles;

namespace AdventOfCode.Abstractions;

public interface IDay3PuzzleSolver : IPuzzleSolver {
    Day3Puzzle.SolutionResponse RunSolution(IReadOnlyList<string> banks, int maxBatteries = 2);
}