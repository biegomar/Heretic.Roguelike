using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.Maps.ContentGeneration;

public class Landscape<T, TK> where TK : ICell<T>
{
    private Vector dimension;
    public int Width => (int)this.dimension.X;
    public int Height => (int)this.dimension.Y;

    public IList<TK> Cells { get; private set; } = new List<TK>();

    public string Title { get; }

    private readonly IProceduralContentGenerator<T, TK> proceduralContentGenerator;
    private readonly IContentPrinter<T, TK> contentPrinter;

    public Landscape(Vector dimension, IProceduralContentGenerator<T, TK> proceduralContentGenerator,
        IContentPrinter<T, TK> contentPrinter) : this(dimension,
        proceduralContentGenerator, contentPrinter, string.Empty)
    {
    }

    public Landscape(Vector dimension, IProceduralContentGenerator<T, TK> proceduralContentGenerator,
        IContentPrinter<T, TK> contentPrinter, string title)
    {
        this.proceduralContentGenerator = proceduralContentGenerator;
        this.contentPrinter = contentPrinter;
        this.Title = title;

        this.dimension = dimension;

        this.InitializeStructure();

        this.Cells = this.proceduralContentGenerator.Generate(this.Cells);
    }

    public void Draw(Vector startVector)
    {
        this.contentPrinter.DrawCells(this.Cells, startVector, this.Title);
    }

    public void DrawCellItems()
    {
        this.contentPrinter.DrawCellItems(this.Cells);
    }

    public void DrawItemAtPosition(Vector position, T item)
    {
        this.contentPrinter.DrawItemAtPosition(this.Cells, position, item);
    }

    public void SetCellItem(CellItem<T> cellItem)
    {
        var cell = GetCellByColumnAndRow((int)cellItem.Position.X, (int)cellItem.Position.Y);
        cell.Item = cellItem.Item;
    }

    public void ClearCellItem(Vector position)
    {
        var cell = GetCellByColumnAndRow((int)position.X, (int)position.Y);
        cell.Item = null!;
    }

    private void InitializeCells()
    {
        this.Cells = proceduralContentGenerator.InitializeCells(this.dimension);
    }

    private void LinkCells()
    {
        this.Cells = proceduralContentGenerator.LinkCells(this.Cells);
    }

    private void InitializeStructure()
    {
        InitializeCells();
        LinkCells();
    }
    
    protected TK GetCellByColumnAndRow(int column, int row)
    {
        return this.Cells.Single(cell => cell.X == column && cell.Y == row);
    }
}