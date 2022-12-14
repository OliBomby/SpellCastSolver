namespace SpellCastSolverLib;

public class LetterState {
    private int multiplier;

    public char Letter;
    public int PointsMultiplier;
    public bool Gem;
    public int Points => GetPoints(Letter);

    public int Multiplier {
        get => LetterPoints.ContainsKey(char.ToLower(Letter)) ? multiplier : 0;
        set => multiplier = value;
    }

    public LetterState(char letter, int pointsMultiplier = 1, int multiplier = 1, bool gem = false)
    {
        Letter = letter;
        PointsMultiplier = pointsMultiplier;
        this.multiplier = multiplier;
        Gem = gem;
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
