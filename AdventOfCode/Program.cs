using System.Text;
using AdventOfCode.Helpers;
using Spectre.Console;

// write out the base splash screen
Console.OutputEncoding = Encoding.UTF8;
AnsiConsole.Write(new Panel("Puzzle Solver for the Advent of Code 2025 season.") {
    Border = BoxBorder.Rounded,
    Width = 369,
    Header = new PanelHeader("[lime] Advent of Code 2025 [/]", Justify.Left)
});
AnsiConsole.WriteLine();

// give the user the choice to input the day they want the data to display for
var day = AnsiConsole.Prompt(new TextPrompt<int?>("What day do you want to solve?").DefaultValue(null));
var part = AnsiConsole.Prompt(
    new SelectionPrompt<SolutionParts>()
        .Title("What part would you like to run?")
        .AddChoices(new[] {
            SolutionParts.PartA,
            SolutionParts.PartB,
            SolutionParts.All
        })
);

var requiresSecondInput = false;
if (part is SolutionParts.All) {
    requiresSecondInput = AnsiConsole.Prompt(
        new TextPrompt<bool>("Does Part B require a second puzzle input?")
            .AddChoice(true)
            .AddChoice(false)
            .DefaultValue(false)
            .WithConverter(choice => choice ? "y" : "n")
    );
}

// use our factory to create the correct concrete puzzle solution class, and then we can run the solve method
// NOTE: any prompts or uploads for data that acts as the puzzle input will be requested for by the puzzle classes
// NOTE: I did it this way this year because it was just easier than trying to be too clever.
var puzzleSolver = PuzzleFactory.ForDay(day);
switch (part) {
    case SolutionParts.PartA:
        AnsiConsole.Clear();
        puzzleSolver.SolvePart1();
        break;
    case SolutionParts.PartB:
        AnsiConsole.Clear();
        puzzleSolver.SolvePart2();
        break;
    case SolutionParts.All:
        AnsiConsole.Clear();
        puzzleSolver.SolvePart1();
        puzzleSolver.SolvePart2(false, requiresSecondInput);
        break;
    default:
        throw new ArgumentOutOfRangeException();
}

AnsiConsole.WriteLine();
AnsiConsole.MarkupLine("[bold gold3]Advent of Code 2025![/]");
AnsiConsole.MarkupLine("[bold lime]Puzzles by Eric Wastl.[/]");
AnsiConsole.MarkupLine("[bold red]Solutions by Stuart Harrison.[/]");
AnsiConsole.WriteLine();
AnsiConsole.Markup("[bold lime]Press any key to exit...[/]");
Console.ReadLine();