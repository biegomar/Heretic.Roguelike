using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Common;
using Heretic.Roguelike.Things.Interfaces;
using Heretic.Roguelike.Things.Monsters;

namespace Heretic.Roguelike.SimpleConsoleSample.ArtificialIntelligence.Movements;

/// <summary>
/// Simple player movement. 
/// </summary>
public class PlayerMovement : IMotionController<char>
{
    private readonly Landscape<char, Cell<char>> landscape;
    private readonly IBattleArena<char> battleArena;
    private IThing<char>? stash;

    /// <summary>
    /// Simple player movement. 
    /// </summary>
    /// <param name="landscape"></param>
    /// <param name="battleArena"></param>
    /// <param name="startingPosition">The starting position of the player.</param>
    public PlayerMovement(Landscape<char, Cell<char>> landscape, IBattleArena<char> battleArena, Vector startingPosition)
    {
        this.landscape = landscape;
        this.battleArena = battleArena;
        ActualPosition = startingPosition;
    }

    public IThing<char> Entity { get; set; }
    
    public Vector ActualPosition { get; set; }

    private ICell<char>? lookLeftCell;
    private ICell<char>? lookAheadCell;
    private ICell<char>? lookRightCell;

    public void Translate(Vector offset)
    {
        var newPosition = this.ActualPosition + offset;
        
        var actualCell = this.GetCell(this.ActualPosition);
        var newCell = this.GetCell(newPosition);
        ClearAheadCells();
        
        this.landscape.ClearMessage();
        
        if (this.AreCellsLinked(actualCell, newCell))
        {
            if (this.IsCellBlockedByAnyThing(newCell, out var thing))
            {
                if (thing is Monster<char> monster)
                {
                    this.FightMonster(monster);    
                }
                else if (thing is Exit<char>)
                {
                    this.stash = thing;
                    this.MoveItemToNewCell(actualCell, newCell);
                    UpdateLookAheadCellsVisibility(newPosition, offset);
                }
                else
                {
                    this.stash = thing;
                    if (this.Entity is ICreature<char> player)
                    {
                        if (this.stash != null)
                        {
                            if (player.Pick(this.stash))
                            {
                                this.stash = null;
                            }
                        }
                    }
                    
                    this.MoveItemToNewCell(actualCell, newCell);
                    UpdateLookAheadCellsVisibility(newPosition, offset);
                }
            }
            else
            {
                this.MoveItemToNewCell(actualCell, newCell);
                UpdateLookAheadCellsVisibility(newPosition, offset);
            }       
        } 
        
        this.DrawLandscape();
    }

    private void UpdateLookAheadCellsVisibility(Vector newPosition, Vector offset)
    {
        var lookAheadPosition = newPosition + offset;
        var lookLeftPosition = newPosition + new Vector(-offset.Y, offset.X, 0); 
        var lookRightPosition = newPosition + new Vector(offset.Y, -offset.X, 0); 

        
        this.lookAheadCell = this.GetCell(lookAheadPosition);
        this.lookLeftCell = this.GetCell(lookLeftPosition);
        this.lookRightCell = this.GetCell(lookRightPosition);

        this.SetCellAndItemToVisible(this.lookAheadCell);
        this.SetCellAndItemToVisible(this.lookLeftCell);
        this.SetCellAndItemToVisible(this.lookRightCell);
    }

    private bool AreCellsLinked(ICell<char>? sourceCell, ICell<char>? destinationCell)
    {
        if (sourceCell == null || destinationCell == null)
        {
            return false;
        }
        
        return sourceCell.LinkedCells.Contains(destinationCell); 
    }

    public void Translate()
    {
        this.Translate(Vector.Zero);
    }

    private void MoveItemToNewCell(ICell<char>? sourceCell, ICell<char>? destinationCell)
    {
        if (sourceCell?.Item != null && destinationCell != null)
        {
            var newPosition = new Vector(destinationCell.X, destinationCell.Y, 0);
            
            this.landscape.SetCellItem(new CellItem<char>(sourceCell.Item, newPosition));
            
            sourceCell.Item = null;
            destinationCell.IsVisible = true;
            if (destinationCell.Item != null)
            {
                destinationCell.Item.IsVisible = true;
            }
        
            this.ActualPosition = newPosition;
        }
    }

    private void SetCellAndItemToVisible(ICell<char>? cell)
    {
        if (cell is not null)
        {
            cell.IsVisible = true;
            if (cell.Item is not null)
            {
                cell.Item.IsVisible = true;
            }
        }
    }

    private void ClearAheadCells()
    {
        this.lookLeftCell = null;
        this.lookRightCell = null;
        this.lookAheadCell = null;
    }

    private void DrawLandscape()
    {
        this.ReApplyStash();
        this.landscape.DrawSingleCellAtPosition(Vector.Zero, this.ActualPosition);
        
        if (this.lookAheadCell != null)
        {
            this.landscape.DrawSingleCellAtPosition(Vector.Zero, new Vector(this.lookAheadCell.X, this.lookAheadCell.Y, 0));    
        }

        if (this.lookLeftCell != null)
        {
            this.landscape.DrawSingleCellAtPosition(Vector.Zero, new Vector(this.lookLeftCell.X, this.lookLeftCell.Y, 0));    
        }

        if (this.lookRightCell != null)
        {
            this.landscape.DrawSingleCellAtPosition(Vector.Zero, new Vector(this.lookRightCell.X, this.lookRightCell.Y, 0));   
        }
        
        this.landscape.DrawCellItems();
        this.landscape.DrawDashboard(Vector.Zero);
    }
    
    private ICell<char>? GetCellByColumnAndRow(bool isNewPositionInGrid, int column, int row)
    {
        return isNewPositionInGrid ? this.landscape.Cells.Single(cell => cell.X == column && cell.Y == row) : null;
    }
    
    private bool IsPositionInGrid(Vector newPosition)
    {
        var isNewPositionInGrid = newPosition.X >= 0 && newPosition.X < this.landscape.Width && newPosition.Y >= 0 &&
                                  newPosition.Y < this.landscape.Height;
        return isNewPositionInGrid;
    }

    private ICell<char>? GetCell(Vector newPosition)
    {
        return this.GetCellByColumnAndRow(IsPositionInGrid(newPosition), (int)newPosition.X, (int)newPosition.Y);
    }
    
    private bool IsCellBlockedByAnyThing(ICell<char>? cell, out IThing<char>? thing)
    {
        if (cell?.Item is { } foundThing)
        {
            thing = foundThing;
            return true;
        }

        thing = null;
        return false;
    }
    
    private void FightMonster(Monster<char>? monster)
    {
        if (monster != null)
        {
            if (this.Entity is ICreature<char> player)
            {
                this.battleArena.Fight(player, monster);    
            }
        }
    }

    private void ReApplyStash()
    {
        if (this.stash != null && this.stash.ActualPosition != this.ActualPosition)
        {
            this.landscape.SetCellItem(new CellItem<char>(this.stash, this.stash.ActualPosition));
            this.stash = null;
        }
    }
}