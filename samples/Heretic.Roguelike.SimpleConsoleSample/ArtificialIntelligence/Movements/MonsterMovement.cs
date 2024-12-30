using System.Diagnostics;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Maps.ContentGeneration.Mazes;
using Heretic.Roguelike.Maps.PathFinding;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.StateMachines;
using Heretic.Roguelike.StateMachines.EventArgs;

namespace Heretic.Roguelike.SimpleConsoleSample.ArtificialIntelligence.Movements;

public class MonsterMovement : IMotionController<char>
{
    private readonly Landscape<char> landscape;
    private readonly IPathFinder pathFinder;
    private FiniteStateMachine fsm;
    private bool attack;

    public MonsterMovement(Landscape<char> landscape, Vector startingPosition)
    {
        this.landscape = landscape;
        this.pathFinder = new PathFinderForMaze<char>(landscape);
        ActualPosition = startingPosition;
        InitializeStateMachine();
    }

    public char Icon { get; set; }
    public Vector ActualPosition { get; set; }
    
    public void Translate(Vector offset)
    {
        fsm.UpdateMachine();
    }

    public void Translate()
    {
        fsm.UpdateMachine();
    }
    
    private void InitializeStateMachine()
    {
        var idleState = new State();
        
        var seekState = new State();
        seekState.Update += SeekPlayerUpdate;
        
        var attackState = new State();
        attackState.Enter += EnterAttack;
        attackState.Update += UpdateAttack;

        var transitToSeekState = new Transition(this.IsPlayerInReach, seekState);
        idleState.AddTransition(transitToSeekState);
        
        var transitToAttackState = new Transition(() => attack, attackState);
        seekState.AddTransition(transitToAttackState);

        var transitFromAttackToSeekState = new Transition(() => !this.attack, seekState);
        attackState.AddTransition(transitFromAttackToSeekState);
        
        
        fsm = new FiniteStateMachine(idleState);
        fsm.AddState(seekState);
        
        fsm.StartMachine();
        fsm.UpdateMachine();
    }
    
    private void SeekPlayerUpdate(object? sender, UpdateEventArgs eventArgs)
    {
        var playerPosition = GetPlayerPosition();
        var path = this.pathFinder.GetShortestPath(new Vector(this.ActualPosition.X, this.ActualPosition.Y, 0), playerPosition);
        if (path.Count > 1)
        {
            var nextCell = path[1];
            if (nextCell.X == playerPosition.X && nextCell.Y == playerPosition.Y)
            {
                this.attack = true;
            }
            else
            {
                this.landscape.ClearCellItem(new Vector(this.ActualPosition.X, this.ActualPosition.Y, 0));
            
                this.ActualPosition = new Vector(nextCell.X, nextCell.Y, 0);
            }
            
            SetAndDrawItem();
        }
    }
    
    private void EnterAttack(object? sender, EnterEventArgs eventArgs)
    {
        Debug.WriteLine("Enter Attack!");
    }
    
    private void UpdateAttack(object? sender, UpdateEventArgs eventArgs)
    {
        var playerPosition = GetPlayerPosition();
        var path = this.pathFinder.GetShortestPath(new Vector(this.ActualPosition.X, this.ActualPosition.Y, 0),
            playerPosition);

        if (path.Count > 1)
        {
            var nextCell = path[1];
            if (nextCell.X != playerPosition.X || nextCell.Y != playerPosition.Y)
            {
                this.attack = false;
            }
        }
        else
        {
            this.attack = false;
        }
    }
    
    private Vector GetPlayerPosition()
    {
        var playerCell = this.landscape.Cells.Cast<Cell<ICreature<char>>>().FirstOrDefault(c => c.Item is Player<char>);
        if (playerCell != null)
        {
            return new Vector(playerCell.X, playerCell.Y, 0);
        }
        else
        {
            return null;
        }
    }
    
    private bool IsPlayerInReach()
    {
        var cell = GetCellByColumnAndRow((int)this.ActualPosition.X, (int)this.ActualPosition.Y);
        foreach (var neighbour in cell.Neighbours.Values.Where(x => x.Item != null))
        {
            if (neighbour.Item is Player<char>)
            {
                return true;
            }
        }

        return false;
    }
    
    private Cell<char> GetCellByColumnAndRow(int column, int row)
    {
        return this.landscape.Cells.Single(cell => cell.X == column && cell.Y == row);
    }
    
    private void SetAndDrawItem()
    {
        landscape.SetCellItem(new CellItem<char>(this.Icon, 
            new Vector(
                this.ActualPosition.X,
                this.ActualPosition.Y,0)));
        landscape.DrawCellItems();
    }
}