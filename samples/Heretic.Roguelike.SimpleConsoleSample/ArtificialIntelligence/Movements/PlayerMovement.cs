using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.SimpleConsoleSample.ArtificialIntelligence.Movements;

/// <summary>
/// Simple player movement. 
/// </summary>
public class PlayerMovement : IMotionController<char>
{
    private readonly Landscape<char, Cell<char>> landscape;

    /// <summary>
    /// Simple player movement. 
    /// </summary>
    /// <param name="landscape"></param>
    /// <param name="startingPosition">The starting position of the player.</param>
    public PlayerMovement(Landscape<char, Cell<char>> landscape, Vector startingPosition)
    {
        this.landscape = landscape;
        ActualPosition = startingPosition;
    }

    public ICreature<char>? Entity { get; set; }
    
    public Vector ActualPosition { get; set; }

    public void Translate(Vector offset)
    {
        var newPosition = this.ActualPosition + offset;
        
        var isNewPositionInGrid = IsNewPositionInGrid(newPosition);
        
        var isNewCellLinked = IsNewCellLinked(isNewPositionInGrid, newPosition);
        
        if (isNewPositionInGrid && isNewCellLinked)
        {
            this.SetItemToNewPosition(newPosition);
        }

        DrawLandscape();
    }

    private bool IsNewCellLinked(bool isNewPositionInGrid, Vector newPosition)
    {
        var isNewCellLinked = isNewPositionInGrid && (GetCellByColumnAndRow((int)this.ActualPosition.X, (int)this.ActualPosition.Y)
            .LinkedCells
            .Contains(GetCellByColumnAndRow((int)newPosition.X, (int)newPosition.Y)));
        return isNewCellLinked;
    }

    private bool IsNewPositionInGrid(Vector newPosition)
    {
        var isNewPositionInGrid = newPosition.X >= 0 && newPosition.X < landscape.Width && newPosition.Y >= 0 &&
                                  newPosition.Y < landscape.Height;
        return isNewPositionInGrid;
    }

    public void Translate()
    {
        this.Translate(Vector.Zero);
    }

    private void SetItemToNewPosition(Vector newPosition)
    {
        var actualCell = GetCellByColumnAndRow((int)this.ActualPosition.X, (int)this.ActualPosition.Y);

        if (actualCell.Item != null)
        {
            landscape.SetCellItem(new CellItem<char>(actualCell.Item,
                new Vector(
                    newPosition.X,
                    newPosition.Y, 0)));
        }

        actualCell.Item = null;
        
        this.ActualPosition = newPosition;
    }

    private void DrawLandscape()
    {
        landscape.DrawCellItems();
        landscape.DrawDashboard();
        landscape.ClearMessage();
    }
    
    private ICell<char> GetCellByColumnAndRow(int column, int row)
    {
        return landscape.Cells.Single(cell => cell.X == column && cell.Y == row);
    }
}