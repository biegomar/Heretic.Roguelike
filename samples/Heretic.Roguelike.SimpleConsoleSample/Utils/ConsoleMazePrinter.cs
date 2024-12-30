using System.Text;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.SimpleConsoleSample.Utils;

public class ConsoleMazePrinter: IContentPrinter<char>
{
    private const string CornerStone = "+";
    private const string CellHorizontal = "---";
    private const string CellVertical = "|";
    private const string EmptyFloor = "   ";
    private const string LinkToSouthernCell = "   ";
    private const string LinkToEasternCell = " ";
    
    private int drawColumn;

    public IList<char>? Items { get; set; }

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
                
                if (GetCellByColumnAndRow(cells, column,row).Item != null)
                {
                    Console.Write(GetCellByColumnAndRow(cells, column,row).Item);
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
        int oldX = Console.CursorLeft;
        int oldY = Console.CursorTop;
        var screenPositionX = this.drawColumn + 2 + (position.X) * 4;
        var screenPositionY = (position.Y + 2) * 2;
        Console.SetCursorPosition((int)screenPositionX, (int)screenPositionY);
        Console.Write(item);
        Console.SetCursorPosition(oldX, oldY);
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