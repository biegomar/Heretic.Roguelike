namespace Heretic.Roguelike.Utils;

public interface IMessagePrinter
{
    void QueueMessage(string message);
    
    void PrintMessages();
    
    void PrintStartScreen();
    
    void PrintGameOverScreen();
    
    void PrintGameWonScreen();
    
    void PrintCreditsScreen();
    
    void PrintWelcomeScreen();
    
    void ClearMessage();
}