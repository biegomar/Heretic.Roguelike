﻿using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Creatures;
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
    /// <param name="icon">The icon that is used to display the player on the map.</param>
    public PlayerMovement(Landscape<char, Cell<char>> landscape, Vector startingPosition, char icon)
    {
        this.landscape = landscape;
        Icon = icon;
        ActualPosition = startingPosition;
        
        this.SetItem();
    }

    public char Icon { get; set; }
    public Vector ActualPosition { get; set; }

    public void Translate(Vector offset)
    {
        var newPosition = this.ActualPosition + offset;
        
        var isNewPositionInGrid = IsNewPositionInGrid(newPosition);
        
        var isNewCellLinked = IsNewCellLinked(isNewPositionInGrid, newPosition);
        
        if (isNewPositionInGrid && isNewCellLinked)
        {
            landscape.ClearCellItem(new Vector(this.ActualPosition.X, this.ActualPosition.Y, 0));
            
            this.ActualPosition = newPosition;
        
            SetAndDrawItem();
        }
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

    private void SetItem()
    {
        landscape.SetCellItem(new CellItem<char>(this.Icon, 
            new Vector(
                this.ActualPosition.X,
                this.ActualPosition.Y,0)));
    }
    
    private void SetAndDrawItem()
    {
        this.SetItem();
        landscape.DrawCellItems();
    }
    
    private ICell<char> GetCellByColumnAndRow(int column, int row)
    {
        return landscape.Cells.Single(cell => cell.X == column && cell.Y == row);
    }
}