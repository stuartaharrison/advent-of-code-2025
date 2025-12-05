using AdventOfCode.Abstractions;
using AdventOfCode.Helpers;
using Spectre.Console;

namespace AdventOfCode.Puzzles;

public sealed class Day5Puzzle : IDay5PuzzleSolver {

    private string PuzzleFilePath { get; set; } = string.Empty;
    
    public void SolvePart1(bool includeSplash = true) {
        if (includeSplash) {
            LoadSplashScreen(SolutionParts.PartA);
        }
        
        PuzzleFilePath = AnsiConsole.Prompt(new TextPrompt<string>("Please enter the Puzzle Input file path").DefaultValue(@"C:\AOC\2025\Day5-A.txt"));
        
        var puzzleInput = LoadPuzzleInput(PuzzleFilePath);
        var solution = RunSolutionA(puzzleInput);
        
        if (includeSplash) {
            AnsiConsole.WriteLine();
        }
        
        AnsiConsole.MarkupLine($"[green]Puzzle A Solution: {solution.SpoiledCount}[/]");
    }
    
    public void SolvePart2(bool includeSplash = true, bool requiresNewInput = true) {
        if (includeSplash) {
            LoadSplashScreen(SolutionParts.PartB);
        }
        if (requiresNewInput) {
            PuzzleFilePath = AnsiConsole.Prompt(new TextPrompt<string>("Please enter the Puzzle Input file path").DefaultValue(@"C:\AOC\2025\Day4-B.txt"));
        }
        
        var puzzleInput = LoadPuzzleInput(PuzzleFilePath);
        var solution = RunSolutionB(puzzleInput);
        
        if (includeSplash) {
            AnsiConsole.WriteLine();
        }
        
        AnsiConsole.MarkupLine($"[green]Puzzle B Solution: {solution.TotalFreshIngredients}[/]");
    }

    public PuzzleASolution RunSolutionA(PuzzleInput input) {
        var spoiledCount = 0;
        foreach (var ingredient in input.AvailableIngredients) {
            var isSpoiled = false;
            foreach (var freshIngredientRange in input.FreshIngredientRanges) {
                if (isSpoiled) {
                    break;
                }
                var rangeSplit = freshIngredientRange.Split("-");
                var rangeLow = long.Parse(rangeSplit[0]);
                var rangeHigh = long.Parse(rangeSplit[1]);
                if (ingredient >= rangeLow && ingredient <= rangeHigh) {
                    isSpoiled = true;
                }
            }
            if (isSpoiled) {
                spoiledCount++;
            }
        }
        return new PuzzleASolution(spoiledCount);
    }

    public PuzzleBSolution RunSolutionB(PuzzleInput input) {
        var freshDatabase = new List<long>();
        foreach (var freshIngredientRange in input.FreshIngredientRanges) {
            var rangeSplit = freshIngredientRange.Split("-");
            var rangeLow = long.Parse(rangeSplit[0]);
            var rangeHigh = long.Parse(rangeSplit[1]);
            for (var i = rangeLow; i <= rangeHigh; i++) {
                if (!freshDatabase.Contains(i)) {
                    freshDatabase.Add(i);
                }
            }
        }
        return new PuzzleBSolution(freshDatabase.Count);
    }
    
    private static PuzzleInput LoadPuzzleInput(string filePath = @"C:\AOC\2025\Day5.txt") {
        var puzzleInput = File.ReadAllLines(filePath);
        var freshIngredientRanges = puzzleInput.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        var availableIngredientRanges = puzzleInput.Skip(freshIngredientRanges.Length + 1).Select(long.Parse).ToArray();
        return new PuzzleInput {
            AvailableIngredients = availableIngredientRanges,
            FreshIngredientRanges = freshIngredientRanges,
        };
    }

    private static void LoadSplashScreen(SolutionParts solutionPart) {
        var partString = solutionPart == SolutionParts.PartB ? "Part B" : "Part A";
        AnsiConsole.Write(new Panel("Puzzle 5: Cafeteria!") {
            Border = BoxBorder.Rounded,
            Width = 369,
            Header = new PanelHeader($"[lime] {partString} [/]", Justify.Left)
        });
        AnsiConsole.WriteLine();
    }
    
    public class PuzzleInput {
        
        public long[] AvailableIngredients { get; init; } = [];
        
        public string[] FreshIngredientRanges { get; init; } = [];
    }
    
    public class PuzzleASolution(int spoiledCount) {

        public int SpoiledCount { get; init; } = spoiledCount;
    }
    
    public class PuzzleBSolution(long totalFreshIngredient) {
        
        public long TotalFreshIngredients { get; init; } = totalFreshIngredient;
    }
}