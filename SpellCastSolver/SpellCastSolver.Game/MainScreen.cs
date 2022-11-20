using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using SpellCastSolver.Game.Components;
using SpellCastSolverLib;

namespace SpellCastSolver.Game {
    public class MainScreen : Screen {
        private SpriteText testText = null!;

        [BackgroundDependencyLoader]
        private void load(DictionaryStore dictionaryStore) {
            var data = dictionaryStore.Get(@"words_alpha.txt");

            InternalChildren = new Drawable[] {
                new Box {
                    Colour = Color4.Black,
                    RelativeSizeAxes = Axes.Both,
                },
                new SpriteText {
                    Y = 20,
                    Text = "SpellCast Solver",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 40)
                },
                new BoardDrawable(BoardState.Default)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                new SpriteText
                {
                    Anchor = Anchor.TopLeft,
                    Position = new Vector2(20),
                    Text = $"{data.Length.ToString()} words loaded",
                },
                new FillFlowContainer
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Margin = new MarginPadding(20),
                    Children = new Drawable[]
                    {
                        new BasicButton
                        {
                            Text = @"Solve",
                            Action = () => testText.Text = "Solved",
                            Size = new Vector2(200, 100),
                            Margin = new MarginPadding(20),
                            BackgroundColour = Color4.Purple,
                            HoverColour = Color4.Pink,
                            FlashColour = Color4.HotPink,
                        },
                        testText = new SpriteText()
                    }
                }
            };
        }
    }
}
