using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;
using SpellCastSolverLib;

namespace SpellCastSolver.Game.Components;

public partial class ResultDrawable : CompositeDrawable
{
    private readonly SolveResult result;
    private Drawable box = null!;
    private readonly BindableBool isActive = new();

    public IBindable<bool> IsActive => isActive;
    public SolveResult Result => result;

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
            box = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft,
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
            new SpriteText
            {
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
                X = -40,
                Text = result.Gems.ToString(),
                Colour = Color4.Magenta
            },
        };

        box.Colour = isActive.Value ? Color4.LightGray : Color4.Gray;
    }

    protected override bool OnClick(ClickEvent e)
    {
        isActive.Value = !isActive.Value;
        box.FadeColour(isActive.Value ? Color4.LightGray : Color4.Gray, 100);

        return true;
    }
}
