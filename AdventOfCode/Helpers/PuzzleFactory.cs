using AdventOfCode.Abstractions;
using AdventOfCode.Puzzles;

namespace AdventOfCode.Helpers;

public static class PuzzleFactory {

    public static IPuzzleSolver ForDay(int? day = null) {
        day ??= DateTime.Now.Day;
        return day switch {
            1 => new Day1Puzzle(),
            2 => new Day2Puzzle(),
            3 => new Day3Puzzle(),
            4 => new Day4Puzzle(),
            5 => new Day5Puzzle(),
            _ => throw new ArgumentException($"Unknown day {day}")
        };
    }
}