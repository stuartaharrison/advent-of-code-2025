using System.Text.RegularExpressions;
using AdventOfCode.Abstractions;
using AdventOfCode.Helpers;
using Spectre.Console;

namespace AdventOfCode.Puzzles;

public sealed class Day2Puzzle : IDay2PuzzleSolver {

    private string PuzzleFilePath { get; set; } = string.Empty;
    
    public void SolvePart1(bool includeSplash = true) {
        if (includeSplash) {
            LoadSplashScreen(SolutionParts.PartA);
        }
        
        PuzzleFilePath = AnsiConsole.Prompt(new TextPrompt<string>("Please enter the Puzzle Input file path").DefaultValue(@"C:\AOC\2025\Day2-A.txt"));
        
        var puzzleInput = LoadPuzzleInput(PuzzleFilePath);
        var idRanges = GetProductIdRanges(puzzleInput);
        var solution = RunSolutionA(idRanges);

        if (includeSplash) {
            AnsiConsole.WriteLine();
        }
        
        AnsiConsole.MarkupLine($"[green]Puzzle A Solution: {solution.TotalInvalid} (Count: {solution.InvalidCount})[/]");
    }
    
    public void SolvePart2(bool includeSplash = true, bool requiresNewInput = true) {
        if (includeSplash) {
            LoadSplashScreen(SolutionParts.PartB);
        }
        if (requiresNewInput) {
            PuzzleFilePath =AnsiConsole.Prompt(new TextPrompt<string>("Please enter the Puzzle Input file path").DefaultValue(@"C:\AOC\2025\Day2-B.txt"));
        }
        
        var puzzleInput = LoadPuzzleInput(PuzzleFilePath);
        var idRanges = GetProductIdRanges(puzzleInput);
        var solution = RunSolutionB(idRanges);
        
        if (includeSplash) {
            AnsiConsole.WriteLine();
        }
        
        AnsiConsole.MarkupLine($"[green]Puzzle B Solution: {solution.TotalInvalid} (Count: {solution.InvalidCount})[/]");

    }

    public SolutionResponse RunSolutionA(IEnumerable<IdRange> idRanges) {
        var invalidIds = new List<long>();
        
        // loop each range in the entire puzzle input
        // ranges are like `11-22` and the input will have it like `11-22,95-115` etc
        foreach (var range in idRanges) {
            // now we have a range, we can go between this range and identify any that match what we consider invalid
            // Rule: You can find the invalid IDs by looking for any ID which is made only of some sequence of digits repeated twice.
            // So, 55 (5 twice), 6464 (64 twice), and 123123 (123 twice)
            for (var i = range.Start; i <= range.End; i++) {
                // made a mistake!
                // since we need a sequence to be invalid, any number that can't be split equally, we assume is okay here
                var value = i.ToString();
                if ((value.Length % 2) != 0) {
                    continue;
                }

                // since its now been validated as evenly split-able 
                // we can assume that splitting it in half and checking if they match, would give us a correctly invalid input
                var firstValue = value[..(value.Length / 2)];
                var secondValue = value[(value.Length / 2)..];
                if (string.Equals(firstValue, secondValue, StringComparison.Ordinal)) {
                    invalidIds.Add(i);
                }
            }
        }
        
        return new SolutionResponse {
            InvalidCount = invalidIds.Count,
            TotalInvalid = invalidIds.Sum(),
        };
    }

    public SolutionResponse RunSolutionB(IEnumerable<IdRange> idRanges) {
        var invalidIds = new List<long>();

        foreach (var range in idRanges) {
            // Copies from my part-a original solution
            // Turns out that I had solved Part B first but didn't give quite the right answer for Part A - doh!
            // Anyway, again similar to Part A - loop through each number in the range
            for (var i = range.Start; i <= range.End; i++) {
                // let's use regex to try and find any pattern matches
                // now since we know any repeating sequence is invalid, we can use a bit of regex to match this
#pragma warning disable SYSLIB1045
                var match = Regex.Match(i.ToString(), "^(.*)\\1+$");
#pragma warning restore SYSLIB1045
                
                // if we get a match, lets just add it to our list of all invalid ids
                // we can then "query" this list to get our answers
                if (match.Success) {
                    invalidIds.Add(i);
                }
            }
        }
        
        return new SolutionResponse {
            InvalidCount = invalidIds.Count,
            TotalInvalid = invalidIds.Sum(),
        };
    }

    public static IEnumerable<IdRange> GetProductIdRanges(string rangeInput) {
        return rangeInput.Split(',').Where(x => !string.IsNullOrWhiteSpace(x) && x.Contains('-')).Select(x => {
            var split = x.Split('-');
            return new IdRange {
                Start = long.Parse(split[0]),
                End = long.Parse(split[1]),
            };
        });
    }
    
    private static string LoadPuzzleInput(string filePath = @"C:\AOC\2025\Day2.txt") {
        return File.ReadAllText(filePath);
    }

    private static void LoadSplashScreen(SolutionParts solutionPart) {
        var partString = solutionPart == SolutionParts.PartB ? "Part B" : "Part A";
        AnsiConsole.Write(new Panel("Puzzle 2: Gift Shop!") {
            Border = BoxBorder.Rounded,
            Width = 369,
            Header = new PanelHeader($"[lime] {partString} [/]", Justify.Left)
        });
        AnsiConsole.WriteLine();
    }
    
    public sealed class IdRange {
        
        /// <summary>
        /// The smallest number/starting number in the range
        /// </summary>
        public long Start { get; init; }
        
        /// <summary>
        /// The largest/ending number in the rage
        /// </summary>
        public long End { get; init; }
    }
    
    public sealed class SolutionResponse {
        
        /// <summary>
        /// The total number of invalid ids found in a sequence
        /// </summary>
        public long InvalidCount { get; init; }
    
        /// <summary>
        /// The total count of all the numbers that are invalid.
        /// For example, if we had 2 invalid numbers 11 and 22, this total would be 33
        /// </summary>
        public long TotalInvalid { get; init; }
    }
}