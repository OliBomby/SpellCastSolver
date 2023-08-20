using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
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
    public partial class MainScreen : Screen {
        private FillFlowContainer resultsContainer = null!;
        private Solver solver = null!;
        private readonly BoardState boardState = BoardState.Default;
        private BasicTextBox input = null!;
        private BoardDrawable boardDrawable = null!;

        [BackgroundDependencyLoader]
        private void load() {
            solver = new Solver();

            InternalChildren = new Drawable[] {
                new Box {
                    Colour = new Color4(30, 30, 30, 255),
                    RelativeSizeAxes = Axes.Both,
                },
                new SpriteText {
                    Y = 20,
                    Text = "SpellCast Solver",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 40)
                },
                boardDrawable = new BoardDrawable(boardState)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                new SpriteText
                {
                    Anchor = Anchor.TopLeft,
                    Position = new Vector2(20),
                    Text = $"{solver.WordCount.ToString()} words loaded",
                },
                new BasicButton
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Text = @"Solve",
                    Action = solve,
                    Size = new Vector2(200, 100),
                    Margin = new MarginPadding(20),
                    BackgroundColour = Color4.Purple,
                    HoverColour = Color4.Pink,
                    FlashColour = Color4.HotPink,
                },
                resultsContainer = new FillFlowContainer
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Y = 120,
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Margin = new MarginPadding(20),
                },
                new FillFlowContainer
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Margin = new MarginPadding(20),
                    Children = new Drawable[]
                    {
                        new BasicButton
                        {
                            Text = @"Import",
                            Action = import,
                            Size = new Vector2(200, 100),
                            BackgroundColour = Color4.Purple,
                            HoverColour = Color4.Pink,
                            FlashColour = Color4.HotPink,
                        },
                        input = new BasicTextBox
                        {
                            Size = new Vector2(400, 40),
                            PlaceholderText = @"Enter board state",
                        }
                    }
                }
            };
        }

        private void recreateBoard()
        {
            RemoveInternal(boardDrawable, true);
            AddInternal(boardDrawable = new BoardDrawable(boardState)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            });
        }

        private void import()
        {
            for (int i = 0; i < Math.Min(25, input.Text.Length); i++)
            {
                var row = i / 5;
                var col = i % 5;
                var letter = input.Text[i];
                boardState.Board[row, col].Letter = letter;
            }

            recreateBoard();
        }

        private void solve()
        {
            var result = solver.Solve(boardState);
            resultsContainer.Clear(true);

            foreach (var solveResult in result.OrderByDescending(o => o.Points + o.Gems + o.Word.Length * 0.1).DistinctBy(o => o.Word).Take(40))
            {
                var resultDrawable = new ResultDrawable(solveResult)
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                };

                resultsContainer.Add(resultDrawable);
                resultDrawable.IsActive.ValueChanged += e => IsActiveOnValueChanged(e, resultDrawable);
            }
        }

        private void IsActiveOnValueChanged(ValueChangedEvent<bool> changedEvent, ResultDrawable result)
        {
            boardDrawable.SetPath(changedEvent.NewValue ? result.Result.Path : Array.Empty<(int, int)>());
        }
    }
}
