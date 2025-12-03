using System.Diagnostics;
using AdventOfCode.Abstractions;
using AdventOfCode.Helpers;
using Spectre.Console;

namespace AdventOfCode.Puzzles;

public sealed class Day3Puzzle : IDay3PuzzleSolver {

    private string PuzzleFilePath { get; set; } = string.Empty;
    
    public void SolvePart1(bool includeSplash = true) {
        if (includeSplash) {
            LoadSplashScreen(SolutionParts.PartA);
        }
        
        PuzzleFilePath = AnsiConsole.Prompt(new TextPrompt<string>("Please enter the Puzzle Input file path").DefaultValue(@"C:\AOC\2025\Day3-A.txt"));
        
        var puzzleInput = LoadPuzzleBanks(PuzzleFilePath);
        var solution = RunSolution(puzzleInput);
        
        if (includeSplash) {
            AnsiConsole.WriteLine();
        }
        
        AnsiConsole.MarkupLine($"[green]Puzzle A Solution: {solution.TotalVoltage}[/]");
    }
    
    public void SolvePart2(bool includeSplash = true, bool requiresNewInput = true) {
        if (includeSplash) {
            LoadSplashScreen(SolutionParts.PartB);
        }
        if (requiresNewInput) {
            PuzzleFilePath =AnsiConsole.Prompt(new TextPrompt<string>("Please enter the Puzzle Input file path").DefaultValue(@"C:\AOC\2025\Day3-B.txt"));
        }
        
        var puzzleInput = LoadPuzzleBanks(PuzzleFilePath);
        var solution = RunSolution(puzzleInput, 12);
        
        if (includeSplash) {
            AnsiConsole.WriteLine();
        }
        
        AnsiConsole.MarkupLine($"[green]Puzzle B Solution: {solution.TotalVoltage}[/]");
    }

    public SolutionResponse RunSolution(IReadOnlyList<string> banks, int maxBatteries = 2) {
        var bankVoltages = new List<long>();
        foreach (var bank in banks) {
            var batteries = new int[maxBatteries];
            var batteryArray = bank.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();

            var pointer = 0;
            for (var i = 0; i < maxBatteries; i++) {
                var maxVoltage = int.MinValue;
                for (var j = pointer; j < batteryArray.Length - (maxBatteries - i - 1); j++) {
                    if (batteryArray[j] > maxVoltage) {
                        maxVoltage = batteryArray[j];
                        pointer = j + 1;
                    }
                }
                batteries[i] = maxVoltage;
            }
            
            var combined = string.Join("", batteries);
            bankVoltages.Add(long.Parse(combined));
        }
        return new SolutionResponse {
            Banks = bankVoltages,
            TotalVoltage = bankVoltages.Sum()
        };
    }
    
    private static string[] LoadPuzzleBanks(string filePath = @"C:\AOC\2025\Day3.txt") {
        return File.ReadAllLines(filePath);
    }
    
    private static void LoadSplashScreen(SolutionParts solutionPart) {
        var partString = solutionPart == SolutionParts.PartB ? "Part B" : "Part A";
        AnsiConsole.Write(new Panel("Puzzle 3: Lobby!") {
            Border = BoxBorder.Rounded,
            Width = 369,
            Header = new PanelHeader($"[lime] {partString} [/]", Justify.Left)
        });
        AnsiConsole.WriteLine();
    }

    public sealed class SolutionResponse {
        
        public IEnumerable<long> Banks { get; init; } = new List<long>();
        
        public long TotalVoltage { get; init; }
    }
}