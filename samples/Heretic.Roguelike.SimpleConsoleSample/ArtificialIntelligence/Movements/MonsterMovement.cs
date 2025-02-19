﻿using System.Diagnostics;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Maps.ContentGeneration.Mazes;
using Heretic.Roguelike.Maps.PathFinding;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.StateMachines;
using Heretic.Roguelike.StateMachines.EventArgs;
using Heretic.Roguelike.Things;
using Heretic.Roguelike.Things.Monsters;
using Heretic.Roguelike.Things.Players;

namespace Heretic.Roguelike.SimpleConsoleSample.ArtificialIntelligence.Movements;

public class MonsterMovement : IMotionController<char>
{
    private readonly Landscape<char, Cell<char>> landscape;
    private readonly IBattleArena<char> battleArena;
    private readonly IPathFinder pathFinder;
    private FiniteStateMachine fsm;
    private bool attack;

    public MonsterMovement(Landscape<char, Cell<char>> landscape, IBattleArena<char> battleArena, Vector startingPosition)
    {
        this.landscape = landscape;
        this.battleArena = battleArena;
        this.pathFinder = new PathFinderForMaze<char, Cell<char>>(landscape);
        this.ActualPosition = startingPosition;
        
        this.InitializeStateMachine();
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
        
        var transitToAttackState = new Transition(() => this.attack, attackState);
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
            var newPosition = path[1];
            if ((int)newPosition.X == (int)playerPosition.X && (int)newPosition.Y == (int)playerPosition.Y)
            {
                this.attack = true;
            }
            else
            {
                if (!IsNewPositionBlockedByAnyMonster(newPosition))
                {
                    this.SetItemToNewPosition(newPosition);    
                }
            }
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
            
            landscape.DrawCellItems();
            landscape.DrawDashboard();
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
        var cell = GetCellByColumnAndRow((int)this.ActualPosition.X, (int)this.ActualPosition.Y);
        foreach (var neighbour in cell.Neighbours.Values.Where(x => x != null))
        {
            if (neighbour?.Item is Player<char>)
            {
                return true;
            }
        }

        return false;
    }
    
    private IOrthogonalCell<char> GetCellByColumnAndRow(int column, int row)
    {
        return this.landscape.Cells.Single(cell => cell.X == column && cell.Y == row);
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
        landscape.DrawCellItems();
    }

    private bool IsNewPositionBlockedByAnyMonster(Vector newPosition)
    {
        var newCell = GetCellByColumnAndRow((int)newPosition.X, (int)newPosition.Y);

        return newCell.Item is Monster<char>;
    }
}