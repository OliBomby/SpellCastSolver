using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace SpellCastSolver.Game {
    public class SpellCastSolverGame : SpellCastSolverGameBase {
        private ScreenStack screenStack = null!;

        [BackgroundDependencyLoader]
        private void load() {
            // Add your top-level game components here.
            // A screen stack and sample screen has been provided for convenience, but you can replace it if you don't want to use screens.
            Child = screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };
        }

        protected override void LoadComplete() {
            base.LoadComplete();

            screenStack.Push(new MainScreen());
        }
    }
}
