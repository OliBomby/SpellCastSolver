using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace SpellCastSolver.Game.Tests {
    public partial class SpellCastSolverTestBrowser : SpellCastSolverGameBase {
        protected override void LoadComplete() {
            base.LoadComplete();

            AddRange(new Drawable[] {
                new TestBrowser("SpellCastSolver"),
                new CursorContainer()
            });
        }

        public override void SetHost(GameHost host) {
            base.SetHost(host);
            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}
