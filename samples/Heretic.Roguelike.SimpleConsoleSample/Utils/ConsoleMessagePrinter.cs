using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.SimpleConsoleSample.Utils;

public class ConsoleMessagePrinter : IMessagePrinter
{
    private readonly int consoleWidth = Console.WindowWidth;
    private readonly Queue<string> messages = new ();
    
    public void QueueMessage(string message)
    {
        this.messages.Enqueue(message);
    }

    public void PrintMessages()
    {
        while (this.messages.Count > 0)
        {
            this.ClearMessage();
            var message = this.messages.Dequeue();
            
            var oldX = Console.CursorLeft;
            var oldY = Console.CursorTop;
            var screenPositionX = 0;
            var screenPositionY = 1;
            
            Console.SetCursorPosition((int)screenPositionX, (int)screenPositionY);
            Console.Write($"{message}");
            
            if (this.messages.Count > 0)
            {
                Console.Write("...more...");
                
                ConsoleKey key;
                do
                {
                    key = Console.ReadKey(true).Key;
                } while (key != ConsoleKey.Spacebar);
            }
            
            Console.SetCursorPosition(oldX, oldY);
        }
    }

    public void PrintStartScreen()
    {
        throw new NotImplementedException();
    }

    public void PrintGameOverScreen()
    {
        throw new NotImplementedException();
    }

    public void PrintGameWonScreen()
    {
        throw new NotImplementedException();
    }

    public void PrintCreditsScreen()
    {
        throw new NotImplementedException();
    }

    public void PrintWelcomeScreen()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();

        // Top border
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("╔" + new string('═', consoleWidth - 2) + "╗");

        // Empty lines and content
        for (var i = 0; i < 18; i++)
        {
            switch (i)
            {
                case 1:
                    WriteCenteredLineInBox("Nearly ROGUE", ConsoleColor.Gray);
                    break;
                case 3:
                    WriteCenteredLineInBox("This game of Rogue was designed by:", ConsoleColor.Magenta);
                    break;
                case 5:
                    WriteCenteredLineInBox("Marc Biegota");
                    break;
                case 7:
                    WriteCenteredLineInBox("Tributes:", ConsoleColor.Magenta);
                    break;
                case 9:
                    WriteCenteredLineInBox("This game was inspired by the original Rogue,");
                    break;
                case 10:
                    WriteCenteredLineInBox("created by Michael Toy and Glenn Wichman.");
                    break;
                case 13:
                    WriteCenteredLineInBox("Nearly ROGUE is provided under the MIT License", ConsoleColor.Magenta);
                    break;
                case 15:
                    WriteCenteredLineInBox("https://opensource.org/licenses/MIT");
                    break;
                default:
                    WriteCenteredLineInBox();
                    break;
            }
        }

        // Bottom border
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("╚" + new string('═', consoleWidth - 2) + "╝");
    }

    public void ClearMessage()
    {
        var emptyLine = new string(' ', this.consoleWidth - 1);
        var oldX = Console.CursorLeft;
        var oldY = Console.CursorTop;
        var screenPositionX = 0;
        var screenPositionY = 1;

        Console.SetCursorPosition((int)screenPositionX, (int)screenPositionY);
        Console.Write(emptyLine);
        Console.SetCursorPosition(oldX, oldY);
    }
    
    private void WriteCenteredLineInBox(string text = "", ConsoleColor? textColor = null)
    {
        var contentWidth = consoleWidth - 2;

        if (text.Length > contentWidth)
        {
            text = text.Substring(0, contentWidth);
        }


        var padding = (contentWidth - text.Length) / 2;
        var lineContent = new string(' ', padding) + text + new string(' ', contentWidth - padding - text.Length);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("║");

        if (textColor.HasValue)
            Console.ForegroundColor = textColor.Value;
        else
            Console.ResetColor();

        Console.Write(lineContent);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("║");
        Console.ResetColor();
    }
}