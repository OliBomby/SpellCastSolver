namespace SpellCastSolverLib;

public struct LetterState
{
    public readonly char Letter;
    public readonly int Points;
    public readonly int PointsMultiplier;
    public readonly int Multiplier;
    public readonly bool Gem;

    public LetterState(char letter, int points, int pointsMultiplier = 1, int multiplier = 1, bool gem = false)
    {
        Letter = letter;
        Points = points;
        PointsMultiplier = pointsMultiplier;
        Multiplier = multiplier;
        Gem = gem;
    }
}
