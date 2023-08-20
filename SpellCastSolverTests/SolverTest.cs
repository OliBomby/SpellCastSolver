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
    public void TestSolve()
    {
        var results = solver.Solve(board, false).OrderByDescending(o => o.Points + o.Gems + o.Word.Length);
        var best = results.First();
        Assert.Multiple(() =>
        {
            Assert.That(best.Word, Is.EqualTo("outwore"));
            Assert.That(best.Points, Is.EqualTo(46));
            Assert.That(best.Gems, Is.EqualTo(0));
        });
    }

    [Test]
    public void TestSolveSwaps()
    {
        var results = solver.Solve(board).OrderByDescending(o => o.Points + o.Gems + o.Word.Length);
        var best = results.First();
        Assert.Multiple(() =>
        {
            Assert.That(best.Word, Is.EqualTo("outworked"));
            Assert.That(best.Points, Is.EqualTo(64));
            Assert.That(best.Gems, Is.EqualTo(-3));
        });
    }
}