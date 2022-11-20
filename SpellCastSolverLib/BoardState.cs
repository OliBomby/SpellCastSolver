namespace SpellCastSolverLib;

public struct BoardState
{
    public readonly LetterState[,] Board;
    public int Gems;

    public int Rows => Board.GetLength(0);
    public int Cols => Board.GetLength(1);

    public BoardState(LetterState[,] board, int gems)
    {
        Board = board;
        Gems = gems;
    }

    public static BoardState Default => new(new LetterState[,]
    {
        { new('H'), new('B'), new('O', 3, gem:true), new('G', gem:true), new('E') },
        { new('E', gem:true), new('R'), new('E', gem:true), new('W'), new('A') },
        { new('Q'), new('I'), new('F'), new('X'), new('T') },
        { new('O'), new('T'), new('N'), new('E', gem:true), new('U', multiplier:2) },
        { new('I', gem:true), new('L', gem:true), new('D'), new('I', gem:true), new('O', gem:true) }
    }, 0);

    public bool IsValid((int, int) pos) {
        return pos.Item1 >= 0 && pos.Item1 < Rows && pos.Item2 >= 0 && pos.Item2 < Cols;
    }
}
