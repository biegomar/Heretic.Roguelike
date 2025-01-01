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
    private IList<Monster<T>> monsters;
    private Landscape<T, TK> landscape;
    
    private bool playAnotherGame = true;
    private bool isGameFinished = false;

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
                //this.player.Translate(this.inputController.GetInput());
                
                //this.monsterMovement.MoveTo(default);
            } while (!isGameFinished);

            this.playAnotherGame = false;
        } while (playAnotherGame); 
    }

    private void InitGame()
    {
        var gamePreparation = this.gameAssembler.AssembleGame();
        this.inputController = gamePreparation.InputController;
        this.player = gamePreparation.Player;
        this.landscape = gamePreparation.Landscape;
        
        landscape.Draw(Vector.Zero);
        this.landscape.DrawCellItems();
    }
}