using AdventOfCode.Puzzles;

namespace AdventOfCode.Abstractions;

public interface IDay2PuzzleSolver : IPuzzleSolver {
    Day2Puzzle.SolutionResponse RunSolutionA(IEnumerable<Day2Puzzle.IdRange> idRanges);
    Day2Puzzle.SolutionResponse RunSolutionB(IEnumerable<Day2Puzzle.IdRange> idRanges);
}