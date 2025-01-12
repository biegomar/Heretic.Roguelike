using System.Text;
using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
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
    
    private int drawColumn;

    public IList<char>? Items { get; set; }

    public ConsoleMazePrinter(IArmourCalculator armourCalculator)
    {
        this.armourCalculator = armourCalculator;
        Console.CursorVisible = false;
    }
    
    public void DrawCells(IList<Cell<char>> cells, Vector startMazeVector, string title, bool drawItems = false)
    {
        this.drawColumn = (int)startMazeVector.X;
            
        var (left, top) = Console.GetCursorPosition();
        Console.SetCursorPosition(this.drawColumn, 0);
        Console.WriteLine(title);
            
        string[] lines = GetMazeStringRepresentation(cells).Split(new[] { Environment.NewLine }, StringSplitOptions.None);

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
        for (int column = 0; column < width; column++)
        {
            for (int row = 0; row < height; row++)
            {
                var screenPositionX = this.drawColumn + 2 + (column) * 4;
                var screenPositionY = (row + 2) * 2;
                
                Console.SetCursorPosition(screenPositionX, screenPositionY);
                var item = GetCellByColumnAndRow(cells, column, row).Item;
                if (item != null)
                {
                    Console.Write(item.Icon);
                    
                }
                else
                {
                    Console.Write(' ');    
                }
                
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

    public void DrawDashboard(IList<Cell<char>> cells, Player<char> player, ushort currentFloor)
    {
        var height = cells.Max(cell => cell.Y) + 1;

        var level = $"Level:{currentFloor}".PadRight(12);
        var hits = $"Hits:{player.HitPoints}({player.MaxHitPoints})".PadRight(12);
        var strength = $"Str:{player.Strength}({player.MaxStrength})".PadRight(12);
        var gold = $"Gold:{player.Gold}".PadRight(12);
        var armourValue = this.armourCalculator.CalculateArmourFromArmourClass(player.ActiveArmour?.AmorClass ?? player.AmourClass);
        var armour = $"Armor:{armourValue}".PadRight(12);
        var experience = $"Exp:{ExperienceLevels.GetExperienceLevelName(player.ExperienceLevel)}".PadRight(12);
        
        
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
        var width =  (cells.Max(cell => cell.X) + 2) * 4;
        var paddedMessage = message.PadRight(width);
        var oldX = Console.CursorLeft;
        var oldY = Console.CursorTop;
        var screenPositionX = 0;
        var screenPositionY = 1;
        
        Console.SetCursorPosition((int)screenPositionX, (int)screenPositionY);
        Console.Write($"{paddedMessage}");
        Console.SetCursorPosition(oldX, oldY);
    }

    public Action<string> DrawGameMessage(string message)
    {
        throw new NotImplementedException();
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
            
        for (int row = 0; row < height; row++)
        {                
            var bodyRow = new StringBuilder();
            var bottomRow = new StringBuilder();

            bodyRow.Append(CellVertical);

            for (int column = 0; column < width; column++)
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