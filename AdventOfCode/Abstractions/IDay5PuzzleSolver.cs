using AdventOfCode.Puzzles;

namespace AdventOfCode.Abstractions;

public interface IDay5PuzzleSolver : IPuzzleSolver {
    Day5Puzzle.PuzzleASolution RunSolutionA(Day5Puzzle.PuzzleInput input);
    Day5Puzzle.PuzzleBSolution RunSolutionB(Day5Puzzle.PuzzleInput input);
}