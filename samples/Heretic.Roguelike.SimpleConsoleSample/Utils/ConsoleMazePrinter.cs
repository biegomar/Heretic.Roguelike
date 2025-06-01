using System.Text;
using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things;
using Heretic.Roguelike.Things.Players;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.SimpleConsoleSample.Utils;

public class ConsoleMazePrinter : IContentPrinter<char, Cell<char>>
{
    private enum CellType
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        Down,
        Up,
        Right,
        Left,
        Full,
        Empty,
    }

    private const int STARTROWFORMAZE = 3;
    private readonly IArmourCalculator armourCalculator;
    private readonly Vector landscapeDimensions;

    private const string TopLeft = "┌";
    private const string TopRight = "┐";
    private const string BottomLeft = "└";
    private const string BottomRight = "┘";
    private const string TDown = "┬";
    private const string TUp = "┴";
    private const string TRight = "├";
    private const string TLeft = "┤";
    private const string CellHorizontal = "───";
    private const string CellVertical = "│";
    private const string Cross = "┼";
    private const string EmptyFloor = "   ";
    private const string LinkToNorthernOrSouthernCell = "   ";
    private const string LinkToEasternOrWesternCell = " ";

    private readonly int consoleWidth = Console.WindowWidth;
    private readonly int consoleHeight = Console.WindowHeight;
    private int drawColumn;
    private int lastMessageLength = 0;

    public IList<char>? Items { get; set; }

    public ConsoleMazePrinter(IArmourCalculator armourCalculator, Vector landscapeDimensions)
    {
        this.armourCalculator = armourCalculator;
        this.landscapeDimensions = landscapeDimensions;

        Console.CursorVisible = false;
        Console.OutputEncoding = Encoding.UTF8;
        Console.Title = "Nearly-ROGUE: The Adventure Game";

        Console.Clear();
    }

    public void DrawCells(IList<Cell<char>> cells, Vector startMazeVector, string title, bool drawItems = false)
    {
        this.drawColumn = (int)startMazeVector.X;
        this.DrawAllCells(cells);
        
        if (drawItems)
        {
            this.DrawCellItems(cells);
        }
    }

    public void DrawSingleCellAtPosition(IList<Cell<char>> cells, Vector startMazeVector, Vector position)
    {
        this.drawColumn = (int)startMazeVector.X;
        var newTop = STARTROWFORMAZE + 2 * (int)position.Y;
        var newLeft = this.drawColumn + 4 * (int)position.X;
        
        var cellRepresentation = GetCellRepresentationForPosition(cells, position);
        var lines = cellRepresentation.Split([Environment.NewLine], StringSplitOptions.None);
        var singleLineStep = newTop;
        foreach (var line in lines)
        {
            Console.SetCursorPosition(newLeft, singleLineStep);
            Console.Write(line);
            singleLineStep += 1;
        }
    }

    public void DrawCellItemAtPosition(IList<Cell<char>> cells, Vector position)
    {
        var singleCell = GetCellByColumnAndRow(IsWithinBounds((int)position.X, (int)position.Y), cells, (int)position.X, (int)position.Y);

        if (singleCell != null)
        {
            var oldX = Console.CursorLeft;
            var oldY = Console.CursorTop;
            var screenPositionX = this.drawColumn + 2 + (position.X) * 4;
            var screenPositionY = (position.Y + 2) * 2;
            Console.SetCursorPosition((int)screenPositionX, (int)screenPositionY);
            var icon = ' ';
            
            if (singleCell.Item is {IsVisible: true} item)
            {
                icon = item.Icon;
            }
            
            Console.Write(icon);
            Console.SetCursorPosition(oldX, oldY);
        }
    }

    private void DrawAllCells(IList<Cell<char>> cells)
    {
        var (left, top) = Console.GetCursorPosition();
        var newTop = STARTROWFORMAZE;
        
        for (var row = 0; row < landscapeDimensions.Y; row++)
        {
            var newLeft = this.drawColumn >= left ? this.drawColumn : left;
            for (var column = 0; column < landscapeDimensions.X; column++)
            {
                var cellRepresentation = GetCellRepresentationForPosition(cells, new Vector(column, row, 0));
                var lines = cellRepresentation.Split([Environment.NewLine], StringSplitOptions.None);
                var singleLineStep = newTop;
                foreach (var line in lines)
                {
                    Console.SetCursorPosition(newLeft, singleLineStep);
                    Console.Write(line);
                    singleLineStep += 1;
                }
                
                newLeft = Math.Min(newLeft + 4, Console.BufferWidth - 1);
            }    
            newTop = Math.Min(newTop + 2, Console.BufferHeight - 1);
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
                
                var item = GetCellByColumnAndRow(IsWithinBounds(column, row), cells, column, row)?.Item;
                
                if (item is {IsVisible:true})
                {
                    Console.Write(item.Icon);    
                }
                else
                {
                    Console.Write(" ");
                } 
            }
        }

        Console.SetCursorPosition(oldScreenPositionX, oldScreenPositionY);
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

        this.lastMessageLength = message.Length;
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
    
    private string GetCellRepresentationForPosition(IList<Cell<char>> cells, Vector position)
    {
        var singleCell = GetCellByColumnAndRow(IsWithinBounds((int)position.X, (int)position.Y), cells, (int)position.X, (int)position.Y);

        if (singleCell == null)
        {
            return GetCellRepresentation(CellType.Empty, null);
        }
        
        if (!singleCell.IsVisible)
        {
            return GetCellRepresentation(CellType.Empty, singleCell);
        }
        
        if (position.Y == 0)
        {
            if (position.X == 0)
            {
                return GetCellRepresentation(CellType.TopLeft, singleCell);    
            }

            if ((int)position.X == (int)landscapeDimensions.X - 1)
            {
                return GetCellRepresentation(CellType.TopRight, singleCell);
            }
            
            return GetCellRepresentation(CellType.Down, singleCell);
        }
        
        if ((int)position.Y == (int)landscapeDimensions.Y - 1)
        {
            if (position.X == 0)
            {
                return GetCellRepresentation(CellType.BottomLeft, singleCell);    
            }

            if ((int)position.X == (int)landscapeDimensions.X - 1)
            {
                return GetCellRepresentation(CellType.BottomRight, singleCell);
            }
            
            return GetCellRepresentation(CellType.Up, singleCell);
        }

        if (position.X == 0)
        {
            return GetCellRepresentation(CellType.Left, singleCell);
        }

        if ((int)position.X == (int)landscapeDimensions.X - 1)
        {
            return GetCellRepresentation(CellType.Right, singleCell);
        }
        
        return GetCellRepresentation(CellType.Full, singleCell);
    }
    
    private string GetCellRepresentation(CellType cellType, Cell<char>? singleCell)
    {
        return cellType switch
        {
            CellType.TopLeft => GetTopLeftCellRepresentation(singleCell!),
            CellType.TopRight => GetTopRightCellRepresentation(singleCell!),
            CellType.BottomLeft => GetBottomLeftCellRepresentation(singleCell!),
            CellType.BottomRight => GetBottomRightCellRepresentation(singleCell!),
            CellType.Down => GetDownCellRepresentation(singleCell!),
            CellType.Up => GetUpCellRepresentation(singleCell!),
            CellType.Right => GetRightCellRepresentation(singleCell!),
            CellType.Left => GetLeftCellRepresentation(singleCell!),
            CellType.Empty => GetEmptyCellRepresentation(),
            _ => GetFullCellRepresentation(singleCell!)
        };
    }

    private string GetCellBodyWithLinks(Cell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(singleCell.LinkedCells.Contains(singleCell.WesternNeighbour) ? LinkToEasternOrWesternCell : CellVertical).Append(EmptyFloor)
            .AppendLine(singleCell.LinkedCells.Contains(singleCell.EasternNeighbour) ? LinkToEasternOrWesternCell : CellVertical);
        
        return result.ToString();
    }
    
    private string GetSouthernConnector(Cell<char> singleCell)
    {
        return singleCell.LinkedCells.Contains(singleCell.SouthernNeighbour) ? LinkToNorthernOrSouthernCell : CellHorizontal;
    }

    private string GetNorthernConnector(Cell<char> singleCell)
    {
        return singleCell.LinkedCells.Contains(singleCell.NorthernNeighbour) ? LinkToNorthernOrSouthernCell : CellHorizontal;
    }
    
    private string GetTopLeftCellRepresentation(Cell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(TopLeft).Append(CellHorizontal).AppendLine(TDown);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(TRight).Append(GetSouthernConnector(singleCell)).AppendLine(Cross);

        return result.ToString();
    }

    private string GetTopRightCellRepresentation(Cell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(TDown).Append(CellHorizontal).AppendLine(TopRight);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(Cross).Append(GetSouthernConnector(singleCell)).AppendLine(TLeft);

        return result.ToString();
    }

    private string GetBottomLeftCellRepresentation(Cell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(TRight).Append(GetNorthernConnector(singleCell)).AppendLine(Cross);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(BottomLeft).Append(CellHorizontal).AppendLine(TUp);

        return result.ToString();
    }

    private string GetBottomRightCellRepresentation(Cell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(Cross).Append(GetNorthernConnector(singleCell)).AppendLine(TLeft);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(TUp).Append(CellHorizontal).AppendLine(BottomRight);

        return result.ToString();
    }

    private string GetDownCellRepresentation(Cell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(TDown).Append(CellHorizontal).AppendLine(TDown);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(Cross).Append(GetSouthernConnector(singleCell)).AppendLine(Cross);

        return result.ToString();
    }

    private string GetUpCellRepresentation(Cell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(Cross).Append(GetNorthernConnector(singleCell)).AppendLine(Cross);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(TUp).Append(CellHorizontal).AppendLine(TUp);

        return result.ToString();
    }

    private string GetRightCellRepresentation(Cell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(Cross).Append(GetNorthernConnector(singleCell)).AppendLine(TLeft);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(Cross).Append(GetSouthernConnector(singleCell)).AppendLine(TLeft);

        return result.ToString();
    }

    private string GetLeftCellRepresentation(Cell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(TRight).Append(GetNorthernConnector(singleCell)).AppendLine(Cross);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(TRight).Append(GetSouthernConnector(singleCell)).AppendLine(Cross);

        return result.ToString();
    }

    private string GetFullCellRepresentation(Cell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(Cross).Append(GetNorthernConnector(singleCell)).AppendLine(Cross);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(Cross).Append(GetSouthernConnector(singleCell)).AppendLine(Cross);

        return result.ToString();
    }
    
    private string GetEmptyCellRepresentation()
    {
        var result = new StringBuilder();
        result.AppendLine("     ");
        result.AppendLine("     ");
        result.AppendLine("     ");

        return result.ToString();
    }

    private Cell<char>? GetCellByColumnAndRow(bool isNewPositionInGrid, IList<Cell<char>> cells, int column, int row)
    {
        return isNewPositionInGrid ? cells.Single(cell => cell.X == column && cell.Y == row) : null;
    }
    
    private bool IsWithinBounds(int column, int row)
    {
        return column >= 0 && column < this.landscapeDimensions.X && row >= 0 && row < this.landscapeDimensions.Y;
    }
}