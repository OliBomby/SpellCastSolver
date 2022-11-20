using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osuTK;
using SpellCastSolver.Resources;

namespace SpellCastSolver.Game {
    public class SpellCastSolverGameBase : osu.Framework.Game {
        // Anything in this class is shared between the test browser and the game implementation.
        // It allows for caching global dependencies that should be accessible to tests, or changing
        // the screen scaling for all components including the test browser and framework overlays.

        protected override Container<Drawable> Content { get; }

        protected SpellCastSolverGameBase() {
            // Ensure game and tests scale with window size and screen DPI.
            base.Content.Add(Content = new DrawSizePreservingFillContainer {
                // You may want to change TargetDrawSize to your "default" resolution, which will decide how things scale and position when using absolute coordinates.
                TargetDrawSize = new Vector2(1366, 768)
            });
        }

        [BackgroundDependencyLoader]
        private void load() {
            Resources.AddStore(new DllResourceStore(typeof(SpellCastSolverResources).Assembly));
            dependencies.Cache(DictionaryStore = new DictionaryStore(new NamespacedResourceStore<byte[]>(Resources, @"Dictionaries")));
        }

        private DependencyContainer dependencies = null!;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        public DictionaryStore DictionaryStore { get; private set; } = null!;
    }
}
