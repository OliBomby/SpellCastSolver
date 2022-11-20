using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;
using SpellCastSolverLib;

namespace SpellCastSolver.Game.Components;

public class LetterDrawable : CompositeDrawable
{
    private readonly LetterState state;

    public LetterDrawable(LetterState state) {
        this.state = state;
        Origin = Anchor.TopLeft;
        Size = new Vector2(50, 50);
        Margin = new MarginPadding(5);
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        InternalChildren = new Drawable[]
        {
            new Box
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft,
                Colour = Color4.Gray,
            }.WithEffect(new EdgeEffect { CornerRadius = 10 }),
            new SpriteText
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Text = state.Letter.ToString(),
                Colour = state.PointsMultiplier == 1 ? state.Multiplier == 1 ? Color4.Black : Color4.Magenta : Color4.Yellow,
                Scale = new Vector2(2)
            },
            new SpriteText
            {
                RelativeAnchorPosition = new Vector2(0.8f, 0.8f),
                Origin = Anchor.Centre,
                Text = state.Points.ToString(),
                Colour = Color4.Black,
                Scale = new Vector2(0.7f)
            },
        };

        if (state.Gem)
        {
            AddInternal(new Box
            {
                RelativeAnchorPosition = new  Vector2(0.2f, 0.75f),
                Size = new Vector2(5, 10),
                Colour = Color4.Magenta,
                Origin = Anchor.Centre,
            });
        }
    }
}
