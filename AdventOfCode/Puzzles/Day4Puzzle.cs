using System.Diagnostics;
using AdventOfCode.Abstractions;
using AdventOfCode.Helpers;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace AdventOfCode.Puzzles;

public sealed class Day4Puzzle : IDay4PuzzleSolver {

    private string PuzzleFilePath { get; set; } = string.Empty;
    
    public void SolvePart1(bool includeSplash = true) {
        if (includeSplash) {
            LoadSplashScreen(SolutionParts.PartA);
        }
        
        PuzzleFilePath = AnsiConsole.Prompt(new TextPrompt<string>("Please enter the Puzzle Input file path").DefaultValue(@"C:\AOC\2025\Day4-A.txt"));
        
        var puzzleInput = LoadPuzzleInput(PuzzleFilePath);
        var solution = RunSolutionA(puzzleInput);
        
        if (includeSplash) {
            AnsiConsole.WriteLine();
        }
        
        AnsiConsole.MarkupLine($"[green]Puzzle A Solution: {solution.Total}[/]");
    }
    
    public void SolvePart2(bool includeSplash = true, bool requiresNewInput = true) {
        if (includeSplash) {
            LoadSplashScreen(SolutionParts.PartB);
        }
        if (requiresNewInput) {
            PuzzleFilePath =AnsiConsole.Prompt(new TextPrompt<string>("Please enter the Puzzle Input file path").DefaultValue(@"C:\AOC\2025\Day4-B.txt"));
        }
        
        var puzzleInput = LoadPuzzleInput(PuzzleFilePath);
        var solution = RunSolutionB(puzzleInput);
        
        if (includeSplash) {
            AnsiConsole.WriteLine();
        }
        
        AnsiConsole.MarkupLine($"[green]Puzzle B Solution: {solution.Total}[/]");
    }

    public char[][] BuildGrid(string input, char separator = ',') {
        return BuildGrid(input.Split(separator));
    }
    
    public char[][] BuildGrid(string[] input) {
        var grid = new char[input.Length][];
        for (var i = 0; i < input.Length; i++) {
            grid[i] = new char[input.ElementAt(i).Length];
            for (var j = 0; j < input.ElementAt(i).Length; j++) {
                grid[i][j] = input[i][j];
            }
        }
        return grid;
    }
    
    public PuzzleSolution RunSolutionA(char[][] grid, char paperIco = '@', int limit = 4, bool visualise = false) {
        if (visualise) {
            var displayGrid = BuildDisplayGrid(grid);
            var solution = new PuzzleSolution(grid, 0);
            AnsiConsole.Live(displayGrid).Start(ctx => {
                solution = PassGrid(grid, paperIco, limit: limit, table: displayGrid);
                ctx.Refresh();
            });
            return solution;
        }
        return PassGrid(grid, paperIco, limit: limit);
    }
    
    public PuzzleSolution RunSolutionB(char[][] grid, char paperIco = '@', char removedIco = 'X', int limit = 4, bool visualise = false) {
        var totalRemoved = 0;
        var solution = new PuzzleSolution(grid, int.MaxValue);
        while (solution.Total > 0) {
            solution = PassGrid(solution.GridState, paperIco, removedIco, limit);
            totalRemoved += solution.Total;
        }
        return new PuzzleSolution(grid, totalRemoved);
    }

    private static Table BuildDisplayGrid(char[][] grid) {
        var displayTable = new Table();
        for (var i = 0; i < grid[0].Length; i++) {
            displayTable.AddColumn(new TableColumn(""));
        }
        for (var i = 0; i < grid.Length; i++) {
            var rows = grid[i].Select(x => new Markup(x.ToString())).ToArray<IRenderable>();
            displayTable.AddRow(rows);
        }
        return displayTable;
    }
    
    private static bool CheckPosition(char[][] grid, int x, int y, int limit = 4, char paperIcon = '@') {
        // we are checking all adjacent squares in the grid
        // suppose we can just brute for this (at least for part-a anyway)
        var paperCounter = 0;
        // check up (y - 1)
        if (y > 0 && grid[y - 1][x] == paperIcon) {
            paperCounter++;
        }
        // check down (y + 1)
        if (y < grid.Length - 1 && grid[y + 1][x] == paperIcon) {
            paperCounter++;
        }
        // check left (x - 1)
        if (x > 0 && grid[y][x - 1] == paperIcon) {
            paperCounter++;
        }
        // check right (x + 1)
        if (x < grid[y].Length - 1 && grid[y][x + 1] == paperIcon) {
            paperCounter++;
        }
        // check up-left (y - 1, x - 1)
        if (y > 0 && x > 0 && grid[y - 1][x - 1] == paperIcon) {
            paperCounter++;
        }
        // check up-right (y - 1, x + 1)
        if (y > 0 && x < grid[y].Length - 1 && grid[y - 1][x + 1] == paperIcon) {
            paperCounter++;
        }
        // check down-left (y + 1, x - 1)
        if (y < grid.Length - 1 && x > 0 && grid[y + 1][x - 1] == paperIcon) {
            paperCounter++;
        }
        // check down-right (y + 1, x + 1)
        if (y < grid.Length - 1 && x < grid[y].Length - 1 && grid[y + 1][x + 1] == paperIcon) {
            paperCounter++;
        }
        return paperCounter < limit;
    }

    private char[][] LoadPuzzleInput(string filePath = @"C:\AOC\2025\Day4.txt") {
        return BuildGrid(File.ReadAllLines(filePath));
    }
    
    private static void LoadSplashScreen(SolutionParts solutionPart) {
        var partString = solutionPart == SolutionParts.PartB ? "Part B" : "Part A";
        AnsiConsole.Write(new Panel("Puzzle 4: Printing Department!") {
            Border = BoxBorder.Rounded,
            Width = 369,
            Header = new PanelHeader($"[lime] {partString} [/]", Justify.Left)
        });
        AnsiConsole.WriteLine();
    }

    private static PuzzleSolution PassGrid(char[][] grid, char paperIco = '@', char removedIco = '@', int limit = 4, Table? table = null) {
        var total = 0;
        for (var y = 0; y < grid.Length; y++) {
            for (var x = 0; x < grid[y].Length; x++) {
                if (grid[y][x] == paperIco && CheckPosition(grid, x, y, limit)) {
                    total++;                 // part-1 counter (also counts for part-2)
                    grid[y][x] = removedIco; // part-2 removal (note the removed icon stays the same so part-a still continues to output correctly)
                    
                }
            }
        }
        return new PuzzleSolution(grid, total);
    }
    
    public sealed class PuzzleSolution(char[][] grid, int total) {

        public char[][] GridState { get; } = grid;
        
        public int Total { get; } = total;
    }
}