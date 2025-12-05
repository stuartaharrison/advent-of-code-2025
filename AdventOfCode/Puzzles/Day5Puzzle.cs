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
        // tried to brute force this, took 4-5 hours on the first range, so not efficient enough!
        // the best method is to do ((y - x) + 1) for each input to get the result - this should be pretty quick math
        // HOWEVER, some of the inputs might have or be within a previous range
        // You can see in the example where there is 16-20 and then 12-18 - you would basically need the range to be 12-20
        // So we need to loop each of these and check the min and max of the range against current ones for an "existing" one and then do a replacement in our final list
        // We should be able to count/sum the final valid ranges to get the answer
        var validRanges = new List<InputRange>();
        foreach (var inputRange in input.GetFreshIngredientRanges()) {
            var newRange = inputRange; // we can safely assume that the range we have is valid to begin with
            // NOW lets compare it to previous runs to see if we need to "merge" some of these
            var outdatedRanges = new List<InputRange>();
            foreach (var existingValidRange in validRanges) {
                // similar check to how Part A is solved!
                if (newRange.Min <= existingValidRange.Max && newRange.Max >= existingValidRange.Min) {
                    var newMin = Math.Min(newRange.Min, existingValidRange.Min);
                    var newMax =  Math.Max(newRange.Max, existingValidRange.Max);
                    newRange = new InputRange(newMin, newMax);
                    
                    // we still have to remove the old one here
                    // oops! Don't be me and try remove from the list that is being iterated from!
                    outdatedRanges.Add(existingValidRange);
                }
            }
            validRanges = validRanges.Except(outdatedRanges).ToList();
            validRanges.Add(newRange);
        }

        var freshCount = validRanges.Sum(x => (x.Max - x.Min) + 1);
        return new PuzzleBSolution(freshCount);
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
    
    public record InputRange(long Min, long Max);
    
    public class PuzzleInput {
        
        public long[] AvailableIngredients { get; init; } = [];
        
        public string[] FreshIngredientRanges { get; init; } = [];

        public IReadOnlyList<InputRange> GetFreshIngredientRanges() => FreshIngredientRanges.Select(x => {
            var rangeSplit = x.Split('-');
            return new InputRange(long.Parse(rangeSplit[0]), long.Parse(rangeSplit[1]));
        }).ToList();
    }
    
    public class PuzzleASolution(int spoiledCount) {

        public int SpoiledCount { get; init; } = spoiledCount;
    }
    
    public class PuzzleBSolution(long totalFreshIngredient) {
        
        public long TotalFreshIngredients { get; init; } = totalFreshIngredient;
    }
}