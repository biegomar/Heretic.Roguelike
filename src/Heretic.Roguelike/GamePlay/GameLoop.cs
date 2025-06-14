using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.GamePlay;

public class GameLoop<T> 
{
    private readonly IGameController<T> gameController;
    
    private bool playAnotherGame = true;
    public bool IsGameFinished { get; set;}

    public GameLoop(IGameController<T> gameController)
    {
        this.gameController = gameController;
    }
    
    public void Run()
    {
        do
        {
            this.gameController.AssembleGame(new GameAssemblePreparation<T>(this));
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