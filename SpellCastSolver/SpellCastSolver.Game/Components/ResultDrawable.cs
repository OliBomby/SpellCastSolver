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

public class ResultDrawable : CompositeDrawable
{
    private readonly SolveResult result;

    public ResultDrawable(SolveResult result) {
        this.result = result;
        Origin = Anchor.TopLeft;
        Size = new Vector2(200, 20);
        Margin = new MarginPadding(2);
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
            }.WithEffect(new EdgeEffect { CornerRadius = 5 }),
            new SpriteText
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                X = 10,
                Text = result.Word,
                Colour = Color4.Black,
                Scale = new Vector2(1)
            },
            new SpriteText
            {
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
                X = -10,
                Text = result.Points.ToString(),
                Colour = Color4.Yellow
            },
        };
    }
}
