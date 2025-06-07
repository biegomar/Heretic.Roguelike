using Heretic.Roguelike.Maps.Cells;

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
            this.gameController.MessagePrinter.PrintMessages();
            
            do
            {
                this.gameController.ProcessInput();
            } while (!IsGameFinished);

            this.playAnotherGame = false;
        } while (playAnotherGame); 
    }
}