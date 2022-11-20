using System;
using osu.Framework.Allocation;
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

public class LetterDrawable : CompositeDrawable
{
    private LetterState state;
    private Box gemBox = null!;
    private SpriteText letterText = null!;
    private SpriteText pointsText = null!;
    private int multState;

    public LetterDrawable(LetterState state) {
        this.state = state;
        Origin = Anchor.TopLeft;
        Size = new Vector2(50, 50);
        Margin = new MarginPadding(5);
        multState = state.Multiplier != 1 ? 3 : state.PointsMultiplier != 1 ? state.PointsMultiplier - 1 : 0;
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
            letterText = new SpriteText
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Text = state.Letter.ToString().ToUpper(),
                Colour = state.PointsMultiplier == 1 ? state.Multiplier == 1 ? Color4.Black : Color4.Magenta : Color4.Yellow,
                Scale = new Vector2(2)
            },
            pointsText = new SpriteText
            {
                RelativeAnchorPosition = new Vector2(0.8f, 0.8f),
                Origin = Anchor.Centre,
                Text = (state.Points * state.PointsMultiplier).ToString(),
                Colour = Color4.Black,
                Scale = new Vector2(0.7f)
            },
        };

        AddInternal(gemBox = new Box
        {
            RelativeAnchorPosition = new  Vector2(0.2f, 0.75f),
            Size = new Vector2(5, 10),
            Colour = Color4.Magenta,
            Alpha = state.Gem ? 1 : 0,
            Origin = Anchor.Centre,
        });
    }

    protected override bool OnClick(ClickEvent e)
    {
        state.Gem = !state.Gem;
        gemBox.FadeTo(state.Gem ? 1 : 0, 100);

        return true;
    }

    protected override bool OnScroll(ScrollEvent e)
    {
        multState = MathHelper.Clamp(multState + (e.ScrollDelta.Y > 0 ? 1 : -1), 0, 3);

        switch (multState)
        {
            case 0:
                state.PointsMultiplier = 1;
                state.Multiplier = 1;
                break;
            case 1:
                state.PointsMultiplier = 2;
                state.Multiplier = 1;
                break;
            case 2:
                state.PointsMultiplier = 3;
                state.Multiplier = 1;
                break;
            case 3:
                state.PointsMultiplier = 1;
                state.Multiplier = 2;
                break;
        }

        letterText.FadeColour(state.PointsMultiplier == 1 ? state.Multiplier == 1 ? Color4.Black : Color4.Magenta : Color4.Yellow, 100);
        pointsText.Text = (state.Points * state.PointsMultiplier).ToString();

        return true;
    }
}
