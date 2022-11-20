using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Lines;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using osuTK.Graphics;
using SpellCastSolverLib;

namespace SpellCastSolver.Game.Components;

public class BoardDrawable : CompositeDrawable
{
    private readonly BoardState state;
    private SpriteText gemsText = null!;
    private SmoothPath path = null!;

    public BoardDrawable(BoardState state) {
        this.state = state;
        AutoSizeAxes = Axes.Both;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        var height = state.Board.GetLength(0);
        var width = state.Board.GetLength(1);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                AddInternal(new LetterDrawable(state.Board[i, j])
                {
                    RelativeAnchorPosition = new Vector2((float) j / width, (float) i / height)
                });
            }
        }

        AddInternal(gemsText = new SpriteText
        {
            Text = $"{state.Gems.ToString()} gems",
            Colour = Color4.Magenta,
            Anchor = Anchor.BottomLeft,
            Origin = Anchor.CentreLeft,
            Y = 20
        });

        AddInternal(new BasicButton
        {
            Anchor = Anchor.BottomLeft,
            Origin = Anchor.CentreLeft,
            Text = @"+",
            Action = () => { state.Gems++; gemsText.Text = $"{state.Gems.ToString()} gems"; },
            Size = new Vector2(30, 30),
            Position = new Vector2(100, 20),
            BackgroundColour = Color4.Purple,
            HoverColour = Color4.Pink,
            FlashColour = Color4.HotPink,
        });

        AddInternal(new BasicButton
        {
            Anchor = Anchor.BottomLeft,
            Origin = Anchor.CentreLeft,
            Text = @"-",
            Action = () => { state.Gems--; gemsText.Text = $"{state.Gems.ToString()} gems"; },
            Size = new Vector2(30, 30),
            Position = new Vector2(135, 20),
            BackgroundColour = Color4.Purple,
            HoverColour = Color4.Pink,
            FlashColour = Color4.HotPink,
        });

        AddInternal(path = new SmoothPath
        {
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,
            PathRadius = 4,
            Colour = Color4.Yellow,
            Alpha = 0.5f
        });
    }

    public void SetPath((int, int)[] vertices)
    {
        if (vertices.Length == 0)
        {
            path.ClearVertices();
            return;
        }

        path.ClearVertices();

        foreach (var (r, c) in vertices)
        {
            path.AddVertex(new Vector2(c * 60, r * 60));
        }

        var start = new Vector2(vertices[0].Item2 * 60, vertices[0].Item1 * 60);
        path.Position = start - path.PositionInBoundingBox(start) + new Vector2(30);
    }
}
