using System.Collections.Generic;
using System.Linq.Expressions;
using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.GamePlay;

public class GameLoop<T, TK> where TK : class, ICell<T> 
{
    private readonly IGameAssembler<T, TK> gameAssembler;
    private IInputController inputController;
    private Player<T> player;
    private IEnumerable<Monster<T>> monsters;
    private Landscape<T, TK> landscape;
    
    private bool playAnotherGame = true;
    public bool IsGameFinished { get; set;}

    public GameLoop(IGameAssembler<T, TK> gameAssembler)
    {
        this.gameAssembler = gameAssembler;
    }
    
    public void Run()
    {
        do
        {
            InitGame();
            do
            {
                this.inputController.ProcessInput();
                this.MoveMonsters();
            } while (!IsGameFinished);

            this.playAnotherGame = false;
        } while (playAnotherGame); 
    }

    private void MoveMonsters()
    {
        foreach (var monster in monsters)
        {
            monster.Translate();
        }
    }

    private void InitGame()
    {
        var gamePreparation = this.gameAssembler.AssembleGame(this);
        this.inputController = gamePreparation.InputController;
        this.player = gamePreparation.Player;
        this.landscape = gamePreparation.Landscape;
        this.monsters = gamePreparation.Monsters;
        
        this.landscape.Draw(Vector.Zero);
        this.landscape.DrawCellItems();
    }
}