using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;
using SpellCastSolverLib;

namespace SpellCastSolver.Game.Components;

public class BoardDrawable : CompositeDrawable
{
    private readonly BoardState state;

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

        AddInternal(new SpriteText
        {
            Text = $"{state.Gems.ToString()} gems",
            Colour = Color4.Magenta,
            Anchor = Anchor.BottomLeft
        });
    }
}
