using System.Text;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.SimpleConsoleSample.Utils;

public class ConsoleMazePrinter : IContentPrinter<char>
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
    
    private int drawColumn;

    public IList<char>? Items { get; set; }
    public void DrawCells(IEnumerable<ICell<char>> cells, Vector startCellVector, string title, bool drawItems = false)
    {
        this.drawCells(cells.Cast<IOrthogonalCell<char>>(), startCellVector, title, drawItems);
    }

    public void DrawCellItems(IEnumerable<ICell<char>> cells)
    {
        this.drawCellItems(cells.Cast<IOrthogonalCell<char>>());
    }

    public void DrawSingleCellAtPosition(IEnumerable<ICell<char>> cells, Vector startMazeVector, Vector position)
    {
        this.drawSingleCellAtPosition(cells.Cast<IOrthogonalCell<char>>(), startMazeVector, position);   
    }

    public void DrawCellItemAtPosition(IEnumerable<ICell<char>> cells, Vector position)
    {
        this.drawCellItemAtPosition(cells.Cast<IOrthogonalCell<char>>(), position);   
    }

    public ConsoleMazePrinter(Vector landscapeDimensions)
    {
        this.landscapeDimensions = landscapeDimensions;

        Console.CursorVisible = false;
        Console.OutputEncoding = Encoding.UTF8;
        Console.Title = "Nearly-ROGUE: The Adventure Game";

        Console.Clear();
    }

    private void drawCells(IEnumerable<IOrthogonalCell<char>> cells, Vector startMazeVector, string title, bool drawItems = false)
    {
        this.drawColumn = (int)startMazeVector.X;
        this.DrawAllCells(cells);
        
        if (drawItems)
        {
            this.DrawCellItems(cells);
        }
    }
    

    private void drawSingleCellAtPosition(IEnumerable<IOrthogonalCell<char>> cells, Vector startMazeVector, Vector position)
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

    private void drawCellItemAtPosition(IEnumerable<IOrthogonalCell<char>> cells, Vector position)
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

    private void DrawAllCells(IEnumerable<IOrthogonalCell<char>> cells)
    {
        var maxLeft = 0;
        var maxTop = 0;
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
                maxLeft = Math.Max(maxLeft, newLeft);
                maxTop = Math.Max(maxTop, singleLineStep);
            }    
            newTop = Math.Min(newTop + 2, Console.BufferHeight - 1);
        }
    }

    private void drawCellItems(IEnumerable<IOrthogonalCell<char>> cells)
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
                
                var actualCell = GetCellByColumnAndRow(IsWithinBounds(column, row), cells, column, row);
                var item = actualCell?.Item;

                if (actualCell is { IsVisible: true } && item is { IsHidden: false })
                {
                    item.IsVisible = true;
                }
                
                Console.Write(item?.IsVisible == true ? item.Icon : " ");
            }
        }

        Console.SetCursorPosition(oldScreenPositionX, oldScreenPositionY);
    }

    public void ClearScreen()
    {
        Console.Clear();
    }
    
    private string GetCellRepresentationForPosition(IEnumerable<IOrthogonalCell<char>> cells, Vector position)
    {
        IOrthogonalCell<char>? singleCell = GetCellByColumnAndRow(IsWithinBounds((int)position.X, (int)position.Y), cells, (int)position.X, (int)position.Y);

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
    
    private string GetCellRepresentation(CellType cellType, IOrthogonalCell<char>? singleCell)
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

    private string GetCellBodyWithLinks(IOrthogonalCell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(singleCell.LinkedCells.Contains(singleCell.WesternNeighbour) ? LinkToEasternOrWesternCell : CellVertical).Append(EmptyFloor)
            .AppendLine(singleCell.LinkedCells.Contains(singleCell.EasternNeighbour) ? LinkToEasternOrWesternCell : CellVertical);
        
        return result.ToString();
    }
    
    private string GetSouthernConnector(IOrthogonalCell<char> singleCell)
    {
        return singleCell.LinkedCells.Contains(singleCell.SouthernNeighbour) ? LinkToNorthernOrSouthernCell : CellHorizontal;
    }

    private string GetNorthernConnector(IOrthogonalCell<char> singleCell)
    {
        return singleCell.LinkedCells.Contains(singleCell.NorthernNeighbour) ? LinkToNorthernOrSouthernCell : CellHorizontal;
    }
    
    private string GetTopLeftCellRepresentation(IOrthogonalCell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(TopLeft).Append(CellHorizontal).AppendLine(TDown);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(TRight).Append(GetSouthernConnector(singleCell)).AppendLine(Cross);

        return result.ToString();
    }

    private string GetTopRightCellRepresentation(IOrthogonalCell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(TDown).Append(CellHorizontal).AppendLine(TopRight);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(Cross).Append(GetSouthernConnector(singleCell)).AppendLine(TLeft);

        return result.ToString();
    }

    private string GetBottomLeftCellRepresentation(IOrthogonalCell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(TRight).Append(GetNorthernConnector(singleCell)).AppendLine(Cross);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(BottomLeft).Append(CellHorizontal).AppendLine(TUp);

        return result.ToString();
    }

    private string GetBottomRightCellRepresentation(IOrthogonalCell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(Cross).Append(GetNorthernConnector(singleCell)).AppendLine(TLeft);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(TUp).Append(CellHorizontal).AppendLine(BottomRight);

        return result.ToString();
    }

    private string GetDownCellRepresentation(IOrthogonalCell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(TDown).Append(CellHorizontal).AppendLine(TDown);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(Cross).Append(GetSouthernConnector(singleCell)).AppendLine(Cross);

        return result.ToString();
    }

    private string GetUpCellRepresentation(IOrthogonalCell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(Cross).Append(GetNorthernConnector(singleCell)).AppendLine(Cross);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(TUp).Append(CellHorizontal).AppendLine(TUp);

        return result.ToString();
    }

    private string GetRightCellRepresentation(IOrthogonalCell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(Cross).Append(GetNorthernConnector(singleCell)).AppendLine(TLeft);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(Cross).Append(GetSouthernConnector(singleCell)).AppendLine(TLeft);

        return result.ToString();
    }

    private string GetLeftCellRepresentation(IOrthogonalCell<char> singleCell)
    {
        var result = new StringBuilder();
        result.Append(TRight).Append(GetNorthernConnector(singleCell)).AppendLine(Cross);
        result.Append(GetCellBodyWithLinks(singleCell));
        result.Append(TRight).Append(GetSouthernConnector(singleCell)).AppendLine(Cross);

        return result.ToString();
    }

    private string GetFullCellRepresentation(IOrthogonalCell<char> singleCell)
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

    private IOrthogonalCell<char>? GetCellByColumnAndRow(bool isNewPositionInGrid, IEnumerable<ICell<char>> cells, int column, int row)
    {
        return isNewPositionInGrid ? cells.OfType<IOrthogonalCell<char>>().Single(cell => cell.X == column && cell.Y == row) : null;
    }
    
    private bool IsWithinBounds(int column, int row)
    {
        return column >= 0 && column < this.landscapeDimensions.X && row >= 0 && row < this.landscapeDimensions.Y;
    }
}