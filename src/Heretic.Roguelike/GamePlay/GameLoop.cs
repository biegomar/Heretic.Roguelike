using System.Collections.Generic;
using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Maps.ContentGeneration;

namespace Heretic.Roguelike.GamePlay;

public class GameLoop<T>
{
    private IGameAssembler<T> gameAssembler;
    private GamePreparationInputStructure<T> gamePreparation;
    private IInputController inputController;
    private Player<T> player;
    private IList<Monster<T>> monsters;
    private Landscape<ICreature<T>> landscape;
    
    private bool playAnotherGame = true;
    private bool isGameFinished = false;

    public GameLoop(IGameAssembler<T> gameAssembler, GamePreparationInputStructure<T> gamePreparation)
    {
        this.gameAssembler = gameAssembler;
        this.gamePreparation = gamePreparation;
    }
    
    public void Run()
    {
        do
        {
            InitGame();
            do
            {
                this.player.Translate(this.inputController.GetInput());
                
                //this.monsterMovement.MoveTo(default);
            } while (!isGameFinished);

            this.playAnotherGame = false;
        } while (playAnotherGame); 
    }

    private void InitGame()
    {
        var gamePreparationOutput = this.gameAssembler.AssembleGame(this.gamePreparation);
        this.inputController = gamePreparationOutput.InputController;
        this.player = gamePreparationOutput.Player;
        this.landscape = gamePreparationOutput.Landscape;
    }
}