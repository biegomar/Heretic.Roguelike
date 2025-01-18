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
            this.gameController.AssembleGame(this);
            do
            {
                this.gameController.ProcessInput();
            } while (!IsGameFinished);

            this.playAnotherGame = false;
        } while (playAnotherGame); 
    }
}