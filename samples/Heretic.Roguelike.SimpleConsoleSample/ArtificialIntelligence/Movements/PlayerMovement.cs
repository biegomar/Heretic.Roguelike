using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things;
using Heretic.Roguelike.Things.Monsters;

namespace Heretic.Roguelike.SimpleConsoleSample.ArtificialIntelligence.Movements;

/// <summary>
/// Simple player movement. 
/// </summary>
public class PlayerMovement : IMotionController<char>
{
    private readonly Landscape<char, Cell<char>> landscape;
    private readonly IBattleArena<char> battleArena;

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

    public ICreature<char> Entity { get; set; }
    
    public Vector ActualPosition { get; set; }

    public void Translate(Vector offset)
    {
        var newPosition = this.ActualPosition + offset;
        
        var actualCell = this.GetCell(this.ActualPosition);
        var newCell = this.GetCell(newPosition);
        
        if (this.AreCellsLinked(actualCell, newCell))
        {
            if (this.IsCellBlockedByAnyMonster(newCell))
            {
                this.FightMonster(this.GetMonsterFromCell(newCell));
            }
            else
            {
                this.MoveItemToNewCell(actualCell, newCell);
            }       
        } 
        
        this.DrawLandscape();
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
        
            this.ActualPosition = newPosition;
        }
    }

    private void DrawLandscape()
    {
        this.landscape.DrawCellItems();
        this.landscape.DrawDashboard();
        this.landscape.ClearMessage();
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
    
    private bool IsCellBlockedByAnyMonster(ICell<char>? cell)
    {
        if (cell != null)
        {
            return cell.Item is Monster<char>;
        }
        
        return false;
    }

    private Monster<char>? GetMonsterFromCell(ICell<char>? cell)
    {
        if (cell?.Item is Monster<char> monster)
        {
            return monster;
        }  
        
        return null;
    }
    
    private void FightMonster(Monster<char>? monster)
    {
        if (monster != null)
        {
            this.battleArena.Fight(this.Entity, monster);    
        }
    }
}