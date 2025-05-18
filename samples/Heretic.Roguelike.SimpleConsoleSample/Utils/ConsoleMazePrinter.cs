using System.Text;
using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things;
using Heretic.Roguelike.Things.Players;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.SimpleConsoleSample.Utils;

public class ConsoleMazePrinter: IContentPrinter<char, Cell<char>>
{
    private readonly IArmourCalculator armourCalculator;
    private const string CornerStone = "+";
    private const string CellHorizontal = "---";
    private const string CellVertical = "|";
    private const string EmptyFloor = "   ";
    private const string LinkToSouthernCell = "   ";
    private const string LinkToEasternCell = " ";
    
    private readonly int consoleWidth = Console.WindowWidth;
    private readonly int consoleHeight = Console.WindowHeight;
    private int drawColumn;
    private int lastMessageLength = 0;

    public IList<char>? Items { get; set; }

    public ConsoleMazePrinter(IArmourCalculator armourCalculator)
    {
        this.armourCalculator = armourCalculator;
        
        Console.CursorVisible = false;
        Console.OutputEncoding = Encoding.UTF8;
        Console.Title = "ROGUE: The Adventure Game";
        
        Console.Clear();
    }
    
    public void DrawCells(IList<Cell<char>> cells, Vector startMazeVector, string title, bool drawItems = false)
    {
        this.drawColumn = (int)startMazeVector.X;
            
        var (left, _) = Console.GetCursorPosition();
        Console.SetCursorPosition(this.drawColumn, 0);
        Console.WriteLine(title);
            
        var lines = GetMazeStringRepresentation(cells).Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        var newTop = 3;
        foreach (var line in lines)
        {
            var newLeft = this.drawColumn >= left ? this.drawColumn : left;
            Console.SetCursorPosition(newLeft,newTop);
            Console.WriteLine(line);
            newTop = Math.Min(newTop + 1, Console.BufferHeight - 1);
        }

        if (drawItems)
        {
            this.DrawCellItems(cells);
        }
    }

    public void DrawCellItems(IList<Cell<char>> cells)
    {
        var width = cells.Max(cell => cell.X) + 1;
        var height = cells.Max(cell => cell.Y) + 1;
        
        var (oldScreenPositionX, oldScreenPositionY) = Console.GetCursorPosition();
        for (var column = 0; column < width; column++)
        {
            for (var row = 0; row < height; row++)
            {
                var screenPositionX = this.drawColumn + 2 + (column) * 4;
                var screenPositionY = (row + 2) * 2;
                
                Console.SetCursorPosition(screenPositionX, screenPositionY);
                var item = GetCellByColumnAndRow(cells, column, row).Item;
                Console.Write(item?.Icon ?? ' ');
            }
        }
            
        Console.SetCursorPosition(oldScreenPositionX, oldScreenPositionY);
    }

    public void DrawItemAtPosition(IList<Cell<char>> cells, Vector position, char item)
    {
        var oldX = Console.CursorLeft;
        var oldY = Console.CursorTop;
        var screenPositionX = this.drawColumn + 2 + (position.X) * 4;
        var screenPositionY = (position.Y + 2) * 2;
        Console.SetCursorPosition((int)screenPositionX, (int)screenPositionY);
        Console.Write(item);
        Console.SetCursorPosition(oldX, oldY);
    }

    public void DrawDashboard(IList<Cell<char>> cells, Player<char> player, int currentFloor)
    {
        var height = cells.Max(cell => cell.Y) + 1;

        var level = $"Level:{currentFloor}".PadRight(12);
        var hits = $"Hits:{player.HitPoints}({player.MaxHitPoints})".PadRight(12);
        var strength = $"Str:{player.Strength}({player.MaxStrength})".PadRight(12);
        var gold = $"Gold:{player.Gold}".PadRight(12);
        var armourValue = this.armourCalculator.CalculateArmourFromArmourClass(player.ActiveArmour?.AmorClass ?? player.AmourClass);
        var armour = $"Armor:{armourValue}".PadRight(12);
        var experience = $"Exp:{ExperienceLevels.GetExperienceLevelName(player.ExperienceLevel)} ({player.Experience})".PadRight(12);
        
        
        var oldX = Console.CursorLeft;
        var oldY = Console.CursorTop;
        var screenPositionX = this.drawColumn;
        var screenPositionY = (height + 2) * 2;
        
        Console.SetCursorPosition((int)screenPositionX, (int)screenPositionY);
        Console.Write($"{level}{hits}{strength}{gold}{armour}{experience}");
        Console.SetCursorPosition(oldX, oldY);
    }

    public void DrawMessage(IList<Cell<char>> cells, string message)
    {
        var waitForKey = message.StartsWith("##");
        if (waitForKey)
        {
            message = message.Substring(2);
            message += "...more...";
        }
        this.lastMessageLength =  message.Length;
        var paddedMessage = message.PadRight(this.lastMessageLength);
        var oldX = Console.CursorLeft;
        var oldY = Console.CursorTop;
        var screenPositionX = 0;
        var screenPositionY = 1;
        
        Console.SetCursorPosition((int)screenPositionX, (int)screenPositionY);
        Console.Write($"{paddedMessage}");
        Console.SetCursorPosition(oldX, oldY);
        if (waitForKey)
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;       
            } while (key != ConsoleKey.Spacebar);
            this.ClearMessage(cells);
        }
    }

    public void DrawStartScreen()
    {
        throw new NotImplementedException();
    }

    public void DrawGameOverScreen()
    {
        throw new NotImplementedException();
    }

    public void DrawGameWonScreen()
    {
        throw new NotImplementedException();
    }

    public void DrawCreditsScreen()
    {
        throw new NotImplementedException();
    }

    public void DrawWelcomeScreen()
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
                    WriteCenteredLineInBox("ROGUE: The Adventure Game", ConsoleColor.Gray);
                    break;
                case 3:
                    WriteCenteredLineInBox("The game of Rogue was designed by:", ConsoleColor.Magenta);
                    break;
                case 5:
                    WriteCenteredLineInBox("Michael Toy and Glenn Wichman");
                    break;
                case 7:
                    WriteCenteredLineInBox("Various implementations by:", ConsoleColor.Magenta);
                    break;
                case 9:
                    WriteCenteredLineInBox("Ken Arnold, Jon Lane and Michael Toy");
                    break;
                case 11:
                    WriteCenteredLineInBox("Adapted for the IBM PC by:", ConsoleColor.Magenta);
                    break;
                case 13:
                    WriteCenteredLineInBox("A.I. Design");
                    break;
                case 15:
                    WriteCenteredLineInBox("(C)Copyright 1985", ConsoleColor.Yellow);
                    WriteCenteredLineInBox("Epyx Incorporated", ConsoleColor.Yellow);
                    WriteCenteredLineInBox("All Rights Reserved", ConsoleColor.Yellow);
                    break;
                default:
                    WriteCenteredLineInBox(); 
                    break;
            }
        }

        // Bottom border
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("╚" + new string('═', consoleWidth - 2) + "╝");

        // Eingabezeile
        Console.WriteLine();
        Console.Write("Rogue's Name? ");
        Console.ResetColor();
        var name = Console.ReadLine();

        Console.WriteLine($"\nWelcome, {name}. Your adventure begins...");
        Thread.Sleep(2000); // nur für Demo 

    }
    
    private void WriteCenteredLineInBox(string text = "", ConsoleColor? textColor  = null)
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

    public void ClearMessage(IList<Cell<char>> cells)
    {
        var emptyLine = new string(' ', this.lastMessageLength);
        var oldX = Console.CursorLeft;
        var oldY = Console.CursorTop;
        var screenPositionX = 0;
        var screenPositionY = 1;
        
        Console.SetCursorPosition((int)screenPositionX, (int)screenPositionY);
        Console.Write(emptyLine);
        Console.SetCursorPosition(oldX, oldY);
    }

    public void ClearScreen()
    {
        Console.Clear();
    }

    private string GetMazeStringRepresentation(IList<Cell<char>> cells)
    {
        var result = new StringBuilder();
        
        var width = cells.Max(cell => cell.X) + 1;
        var height = cells.Max(cell => cell.Y) + 1;

        //North wall
        var segment = CornerStone + CellHorizontal;
        result.Append(string.Join("", Enumerable.Repeat(segment, width)));
        result.AppendLine(CornerStone);
            
        for (var row = 0; row < height; row++)
        {                
            var bodyRow = new StringBuilder();
            var bottomRow = new StringBuilder();

            bodyRow.Append(CellVertical);

            for (var column = 0; column < width; column++)
            {
                var singleCell = GetCellByColumnAndRow(cells, column, row);
                bodyRow.Append(EmptyFloor).Append(singleCell.LinkedCells.Contains(singleCell.EasternNeighbour) ? LinkToEasternCell : CellVertical);
                bottomRow.Append(CornerStone).Append(singleCell.LinkedCells.Contains(singleCell.SouthernNeighbour) ? LinkToSouthernCell : CellHorizontal);
            }
                
            bottomRow.Append(CornerStone);

            result.AppendLine(bodyRow.ToString());
            result.AppendLine(bottomRow.ToString());
        }
           

        return result.ToString();
    }
    
    private Cell<char> GetCellByColumnAndRow(IList<Cell<char>> cells, int column, int row)
    {
        return cells.Single(cell => cell.X == column && cell.Y == row);
    }
}