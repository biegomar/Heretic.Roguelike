using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Maps.PathFinding;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.StateMachines;
using Heretic.Roguelike.StateMachines.EventArgs;
using Heretic.Roguelike.Things.Interfaces;
using Heretic.Roguelike.Things.Monsters;
using Heretic.Roguelike.Things.Players;

namespace Heretic.Roguelike.SimpleConsoleSample.ArtificialIntelligence.Movements;

public class MonsterMovement : IMotionController<char>
{
    private readonly Landscape<char, Cell<char>> landscape;
    private readonly IBattleArena<char> battleArena;
    private readonly IPathFinder pathFinder;
    private readonly FiniteStateMachine fsm;
    private bool attack;
    private IThing<char>? stash;

    public MonsterMovement(Landscape<char, Cell<char>> landscape, IBattleArena<char> battleArena, Vector startingPosition)
    {
        this.landscape = landscape;
        this.battleArena = battleArena;
        this.ActualPosition = startingPosition;
        
        this.pathFinder = InitializePathFinder(landscape);
        this.fsm = this.InitializeStateMachine();
    }

    private static PathFinderForMaze<char, Cell<char>> InitializePathFinder(Landscape<char, Cell<char>> landscape)
    {
        return new PathFinderForMaze<char, Cell<char>>(landscape);
    }

    public IThing<char> Entity { get; set; }
    
    public Vector ActualPosition { get; set; }
    
    public void Translate(Vector offset)
    {
        fsm.UpdateMachine();
    }

    public void Translate()
    {
        fsm.UpdateMachine();
    }
    
    private FiniteStateMachine InitializeStateMachine()
    {
        var idleState = new State();
        
        var seekState = new State();
        seekState.Enter += EnterSeek;
        seekState.Update += SeekPlayerUpdate;
        
        var attackState = new State();
        attackState.Enter += EnterAttack;
        attackState.Update += UpdateAttack;

        var transitToSeekState = new Transition(this.IsPlayerInReach, seekState);
        idleState.AddTransition(transitToSeekState);
        
        var transitToAttackState = new Transition(() => this.attack, attackState);
        seekState.AddTransition(transitToAttackState);

        var transitFromAttackToSeekState = new Transition(() => !this.attack, seekState);
        attackState.AddTransition(transitFromAttackToSeekState);
        
        
        var resultFsm = new FiniteStateMachine(idleState);
        resultFsm.AddState(seekState);
        
        resultFsm.StartMachine();
        resultFsm.UpdateMachine();
        
        return resultFsm;
    }
    
    private void SeekPlayerUpdate(object? sender, UpdateEventArgs eventArgs)
    {
        var playerPosition = GetPlayerPosition();
        var path = this.pathFinder.GetShortestPath(new Vector(this.ActualPosition.X, this.ActualPosition.Y, 0), playerPosition);
        if (path.Count > 1)
        {
            var newPosition = new Vector(path[1].X, path[1].Y, 0);
            
            if (IsCellBlockedByAnyThing(newPosition, out var thing))
            {
                if (thing is Player<char>)
                {
                    this.attack = true;    
                }
                else if (thing is not Monster<char>)
                {
                    this.stash = thing;
                    this.SetItemToNewPosition(newPosition);  
                }
            }
            else
            {
                this.SetItemToNewPosition(newPosition);
            }
            
            this.DrawLandscape();
        }
    }

    private void EnterSeek(object? sender, EnterEventArgs eventArgs)
    {
        var actualCell = GetCell(this.ActualPosition);

        if (actualCell.Item != null)
        {
            actualCell.Item.IsVisible = true;
            this.DrawLandscape();
        }
    }
    
    private void EnterAttack(object? sender, EnterEventArgs eventArgs)
    {
        this.Fight();
    }

    private void Fight()
    {
        if (this.Entity is ICreature<char> monster)
        {
            this.battleArena.Fight(monster, this.GetPlayer());

            DrawLandscape();
        }
    }
    
    private void UpdateAttack(object? sender, UpdateEventArgs eventArgs)
    {
        var playerPosition = GetPlayerPosition();
        var path = this.pathFinder.GetShortestPath(new Vector(this.ActualPosition.X, this.ActualPosition.Y, 0),
            playerPosition);

        if (path.Count > 1)
        {
            var nextCell = path[1];
            if ((int)nextCell.X != (int)playerPosition.X || (int)nextCell.Y != (int)playerPosition.Y)
            {
                this.attack = false;
            }
            else
            {
                this.Fight();
            }
        }
        else
        {
            this.attack = false;
        }
    }
    
    private Vector GetPlayerPosition()
    {
        return GetPlayer()!.ActualPosition;
    }

    private Player<char>? GetPlayer()
    {
        if (this.landscape.Player == null)
        {
            var playerCell = this.landscape.Cells.FirstOrDefault(c => c.Item is Player<char>);
            if (playerCell is { Item: Player<char> })
            {
                this.landscape.Player = playerCell.Item as Player<char>;
            }
            
            throw new InvalidOperationException("The player has not been set in the current landscape.");
        }
        
        return this.landscape.Player;
    }
    
    private bool IsPlayerInReach()
    {
        var cell = GetCell(this.ActualPosition);
        foreach (var neighbour in cell.Neighbours.Values.Where(x => x != null))
        {
            if (neighbour?.Item is Player<char>)
            {
                return true;
            }
        }

        return false;
    }
    
    private ICell<char>? GetCellByColumnAndRow(bool isNewPositionInGrid, int column, int row)
    {
        return isNewPositionInGrid ? this.landscape.Cells.Single(cell => cell.X == column && cell.Y == row) : null;
    }
    
    private void SetItemToNewPosition(Vector newPosition)
    {
        var sourceCell = GetCell(this.ActualPosition);
        var destinationCell = GetCell(newPosition);
        
        if (sourceCell?.Item != null && destinationCell != null)
        {
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
    
    private ICell<char>? GetCell(Vector newPosition)
    {
        return this.GetCellByColumnAndRow(IsPositionInGrid(newPosition), (int)newPosition.X, (int)newPosition.Y);
    }
    
    private bool IsPositionInGrid(Vector newPosition)
    {
        var isNewPositionInGrid = newPosition.X >= 0 && newPosition.X < this.landscape.Width && newPosition.Y >= 0 &&
                                  newPosition.Y < this.landscape.Height;
        return isNewPositionInGrid;
    }
    
    private bool IsCellBlockedByAnyThing(Vector newPosition, out IThing<char>? thing)
    {
        var actualCell = GetCell(newPosition);
        
        if (actualCell?.Item is { } foundThing)
        {
            thing = foundThing;
            return true;
        }

        thing = null;
        return false;
    }
    
    private void ReApplyStash()
    {
        if (this.stash != null && this.stash.ActualPosition != this.ActualPosition)
        {
            this.landscape.SetCellItem(new CellItem<char>(this.stash, this.stash.ActualPosition));
            this.stash = null;
        }
    }
    
    private void DrawLandscape()
    {
        this.ReApplyStash();
        this.landscape.DrawCellItems();
        this.landscape.DrawDashboard(Vector.Zero);
    }
}