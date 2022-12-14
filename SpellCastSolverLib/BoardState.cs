namespace SpellCastSolverLib;

public class BoardState
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
        { new('H'), new('B'), new('O', 3), new('G'), new('E') },
        { new('E'), new('R'), new('E'), new('W'), new('A') },
        { new('Q'), new('I'), new('F'), new('X'), new('T') },
        { new('O'), new('T'), new('N'), new('E'), new('U', multiplier:2) },
        { new('I'), new('L'), new('D'), new('I'), new('O') }
    }, 0);

    public bool IsValid((int, int) pos) {
        return pos.Item1 >= 0 && pos.Item1 < Rows && pos.Item2 >= 0 && pos.Item2 < Cols;
    }
}
