namespace SpellCastSolverLib;

public struct BoardState
{
    public readonly LetterState[,] Board;
    public readonly int Gems;

    public BoardState(LetterState[,] board, int gems)
    {
        Board = board;
        Gems = gems;
    }

    public static BoardState Default => new(new LetterState[,]
    {
        { new('H', 4), new('B', 4), new('O', 3, 3, gem:true), new('G', 3, gem:true), new('E', 1) },
        { new('E', 1, gem:true), new('R', 2), new('E', 1,gem:true), new('W', 5), new('A', 1) },
        { new('Q', 8), new('I', 1), new('F', 5), new('X', 7), new('T', 2) },
        { new('O', 1), new('T', 2), new('N', 2), new('E', 1, gem:true), new('U', 4, multiplier:2) },
        { new('I', 1, gem:true), new('L', 3, gem:true), new('D', 3), new('I', 1, gem:true), new('O', 1, gem:true) }
    }, 0);
}
