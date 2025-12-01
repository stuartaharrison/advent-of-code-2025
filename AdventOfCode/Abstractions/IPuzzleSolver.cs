namespace AdventOfCode.Abstractions;

public interface IPuzzleSolver {
    void SolvePart1(bool includeSplash = true);
    void SolvePart2(bool includeSplash = true, bool requiresNewInput = true);
}