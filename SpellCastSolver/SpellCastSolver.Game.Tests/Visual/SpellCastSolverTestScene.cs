using osu.Framework.Testing;

namespace SpellCastSolver.Game.Tests.Visual {
    public class SpellCastSolverTestScene : TestScene {
        protected override ITestSceneTestRunner CreateRunner() => new SpellCastSolverTestSceneTestRunner();

        private class SpellCastSolverTestSceneTestRunner : SpellCastSolverGameBase, ITestSceneTestRunner {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete() {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}