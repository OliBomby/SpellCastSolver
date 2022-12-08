namespace SpellCastSolverLib;

public class LetterState
{
    public char Letter;
    public int PointsMultiplier;
    public int Multiplier;
    public bool Gem;
    public readonly int Points;

    public LetterState(char letter, int pointsMultiplier = 1, int multiplier = 1, bool gem = false)
    {
        Letter = letter;
        PointsMultiplier = pointsMultiplier;
        Multiplier = multiplier;
        Gem = gem;
        Points = GetPoints(letter);
    }

    public static int GetPoints(char letter)
    {
        return LetterPoints.ContainsKey(char.ToLower(letter)) ? LetterPoints[char.ToLower(letter)] : 0;
    }

    public static readonly Dictionary<char, int> LetterPoints = new() {
        { 'a', 1 },
        { 'b', 4 },
        { 'c', 5 },
        { 'd', 3 },
        { 'e', 1 },
        { 'f', 5 },
        { 'g', 3 },
        { 'h', 4 },
        { 'i', 1 },
        { 'j', 7 },
        { 'k', 6 },
        { 'l', 3 },
        { 'm', 4 },
        { 'n', 2 },
        { 'o', 1 },
        { 'p', 4 },
        { 'q', 8 },
        { 'r', 2 },
        { 's', 2 },
        { 't', 2 },
        { 'u', 4 },
        { 'v', 5 },
        { 'w', 5 },
        { 'x', 7 },
        { 'y', 4 },
        { 'z', 8 }
    };
}
