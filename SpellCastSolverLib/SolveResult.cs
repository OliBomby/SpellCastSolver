namespace SpellCastSolverLib; 

public struct SolveResult {
    public readonly string Word;
    public readonly int Points;
    public readonly int Gems;
    public readonly (int, int)[] Path;

    public SolveResult(string word, int points, int gems, (int, int)[] path) {
        Word = word;
        Points = points;
        Gems = gems;
        Path = path;
    }
}