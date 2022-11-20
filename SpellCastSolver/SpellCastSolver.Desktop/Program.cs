using osu.Framework.Platform;
using osu.Framework;
using SpellCastSolver.Game;

namespace SpellCastSolver.Desktop {
    public static class Program {
        public static void Main() {
            using (GameHost host = Host.GetSuitableDesktopHost(@"SpellCastSolver"))
            using (osu.Framework.Game game = new SpellCastSolverGame())
                host.Run(game);
        }
    }
}