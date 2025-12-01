using AdventOfCode.Abstractions;
using Spectre.Console;

namespace AdventOfCode.Puzzles;

public sealed class Day1Puzzle : IDay1PuzzleSolver {

    private int StartPosition { get; set; }

    private string PuzzleFilePath { get; set; } = string.Empty;
    
    public void SolvePart1(bool includeSplash = true) {
        if (includeSplash) {
            AnsiConsole.Write(new Panel("Puzzle 1: The safe cracker!") {
                Border = BoxBorder.Rounded,
                Width = 369,
                Header = new PanelHeader("[lime] Part A [/]", Justify.Left)
            });
            AnsiConsole.WriteLine();
        }
        
        StartPosition = AnsiConsole.Prompt(new TextPrompt<int>("What is the dials starting position?").DefaultValue(50));
        PuzzleFilePath = AnsiConsole.Prompt(new TextPrompt<string>("Please enter the Puzzle Input file path").DefaultValue(@"C:\AOC\2025\Day1-A.txt"));
        
        var puzzleInput = LoadPuzzleInput(PuzzleFilePath);
        var rotations =  GetRotations(puzzleInput);
        var solution = RunSolution(StartPosition, rotations);

        if (includeSplash) {
            AnsiConsole.WriteLine();
        }
        
        AnsiConsole.MarkupLine($"[green]Puzzle A Solution: {solution.Item1}[/]");
    }

    public void SolvePart2(bool includeSplash = true, bool requiresNewInput = true) {
        if (includeSplash) {
            AnsiConsole.Write(new Panel("Puzzle 1: The safe cracker!") {
                Border = BoxBorder.Rounded,
                Width = 369,
                Header = new PanelHeader("[lime] Part B [/]", Justify.Left)
            });
            AnsiConsole.WriteLine();
        }

        if (requiresNewInput) {
            StartPosition = AnsiConsole.Prompt(new TextPrompt<int>("What is the dials starting position?").DefaultValue(50));
            PuzzleFilePath = AnsiConsole.Prompt(new TextPrompt<string>("Please enter the Puzzle Input file path").DefaultValue(@"C:\AOC\2025\Day1-A.txt"));    
        }
        
        var puzzleInput = LoadPuzzleInput(PuzzleFilePath);
        var rotations =  GetRotations(puzzleInput);
        var solution = RunSolution(StartPosition, rotations);

        if (includeSplash) {
            AnsiConsole.WriteLine();
        }
        
        AnsiConsole.MarkupLine($"[green]Puzzle B Solution: {solution.Item2}[/]");
    }

    public Tuple<int, int> RunSolution(int startPosition, IEnumerable<Rotation> rotations, int minValue = 0, int maxValue = 99) {
        var zeroCount = 0;
        var clicksOntoZeroCount = 0;
        var dialPosition = startPosition;
        
        foreach (var rotation in rotations) {
            // not the most elegant way, but it works and it's not too slow.
            for (var i = 0; i < rotation.TurnAmount; i++) {
                if (rotation.Turn == DialRotations.Left) {
                    dialPosition -= 1;
                    if (dialPosition < minValue) {
                        dialPosition = maxValue;
                    }
                }
                else {
                    dialPosition += 1;
                    if (dialPosition > maxValue) {
                        dialPosition = minValue;
                    }
                }
                
                // this is for part-2 will basically check if each individual click lands on 0
                // instead of just counting whenever it lands on 0 after ALL clicks of a rotation
                if (dialPosition == 0) {
                    clicksOntoZeroCount++;
                }
            }
            
            // now we will check where the position of the dial is (AFTER we do the tidying)
            if (dialPosition == 0) {
                zeroCount++;
            }
        }
        
        return new Tuple<int, int>(zeroCount, clicksOntoZeroCount);
    }

    public static IEnumerable<Rotation> GetRotations(string rotationString) {
        return rotationString.Split(',').Where(x => !string.IsNullOrWhiteSpace(x) && (x.StartsWith($"L") || x.StartsWith($"R"))).Select(x => {
            var direction = x[0] == 'L' ? DialRotations.Left : DialRotations.Right;
            var turnAmount = int.Parse(x[1..]);
            return new Rotation {
                Turn = direction,
                TurnAmount = turnAmount,
            };
        });
    }

    private static string LoadPuzzleInput(string filePath = @"C:\AOC\2025\Day1.txt") {
        return File.ReadAllLines(filePath).Aggregate(string.Empty, (current, line) => $"{current},{line.ToUpper()}").TrimEnd(',');
    }
    
    public enum DialRotations {
        Left,
        Right
    }
    
    public struct Rotation {
        public DialRotations Turn { get; set; }
        public int TurnAmount { get; set; }
    }
}