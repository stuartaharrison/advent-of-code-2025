using AdventOfCode.Puzzles;

namespace AdventOfCode.Abstractions;

public interface IDay4PuzzleSolver : IPuzzleSolver {
    char[][] BuildGrid(string input, char separator = ',');
    char[][] BuildGrid(string[] input);
    Day4Puzzle.PuzzleSolution RunSolutionA(char[][] grid, char paperIco = '@', int limit = 4, bool visualise = false);
    Day4Puzzle.PuzzleSolution RunSolutionB(char[][] grid, char paperIco = '@', char removedIco = 'X', int limit = 4, bool visualise = false);
}