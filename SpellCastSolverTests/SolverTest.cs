namespace SpellCastSolverTests;

public class Tests {
    private Solver solver = null!;
    private BoardState board = null!;

    [SetUp]
    public void Setup() {
        board = BoardState.Default;
        board.Gems = 3;
        solver = new Solver();
    }

    [Test]
    public void TestSolve() {
        var results = solver.Solve(board, false).OrderByDescending(o => o.Points + o.Gems + o.Word.Length);
        var best = results.First();

        Assert.AreEqual("outwore", best.Word);
        Assert.AreEqual(46, best.Points);
        Assert.AreEqual(3, best.Gems);
    }

    [Test]
    public void TestSolveSwaps() {
        var results = solver.Solve(board).OrderByDescending(o => o.Points + o.Gems + o.Word.Length);
        var best = results.First();

        Assert.AreEqual("outworked", best.Word);
        Assert.AreEqual(52, best.Points);
        Assert.AreEqual(0, best.Gems);
    }
}