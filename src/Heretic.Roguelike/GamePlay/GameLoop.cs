using System.Collections.Generic;
using System.Linq.Expressions;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.GamePlay;

public class GameLoop<T, TK> where TK : class, ICell<T> 
{
    private readonly IGameController<T, TK> gameController;
    
    private bool playAnotherGame = true;
    public bool IsGameFinished { get; set;}

    public GameLoop(IGameController<T, TK> gameController)
    {
        this.gameController = gameController;
    }
    
    public void Run()
    {
        do
        {
            this.gameController.AssembleGame(new GameAssemblePreparation<T, TK>(this));
            this.gameController.DrawWelcomeScreen();
            this.gameController.SetPlayerData();
            this.gameController.DrawLandscape();
            
            do
            {
                this.gameController.ProcessInput();
            } while (!IsGameFinished);

            this.playAnotherGame = false;
        } while (playAnotherGame); 
    }
}