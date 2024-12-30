using System.Collections.Generic;
using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Maps.ContentGeneration;

namespace Heretic.Roguelike.GamePlay;

public class GameLoop<T>
{
    private readonly IGameAssembler<T> gameAssembler;
    private IInputController inputController;
    private Player<T> player;
    private IList<Monster<T>> monsters;
    private Landscape<T> landscape;
    
    private bool playAnotherGame = true;
    private bool isGameFinished = false;

    public GameLoop(IGameAssembler<T> gameAssembler)
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
    }
}