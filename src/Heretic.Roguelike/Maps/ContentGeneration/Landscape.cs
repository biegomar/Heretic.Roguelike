using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Players;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.Maps.ContentGeneration;

public class Landscape<T, TK> where TK : ICell<T>
{
    private readonly IProceduralContentGenerator<T, TK> proceduralContentGenerator;
    private readonly IContentPrinter<T, TK> contentPrinter;
    private readonly IDictionary<int, IList<TK>> cellsInLevel = new Dictionary<int, IList<TK>>();
    
    private Vector dimension;
    public int Width => (int)this.dimension.X;
    public int Height => (int)this.dimension.Y;
    public int Depth => (int)this.dimension.Z;

    public int CurrentFloor { get; set; } = 1;

    public IList<TK> Cells
    {
        get => this.cellsInLevel[this.CurrentFloor];
        private set => this.cellsInLevel[this.CurrentFloor] = value;
    }

    public string Title { get; }

    private Player<T>? player;
    public Player<T>? Player
    {
        get
        {
            if (this.player == null)
            {
                throw new InvalidOperationException("The player has not been set in the current landscape.");
            }
            return this.player;
        }
        set => this.SetPlayerIntoCell(value);
    }

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

    public void DrawDashboard()
    {
        this.contentPrinter.DrawDashboard(this.Cells, this.player!, this.CurrentFloor);
    }

    public void DrawItemAtPosition(Vector position, T item)
    {
        this.contentPrinter.DrawItemAtPosition(this.Cells, position, item);
    }

    public void DrawMessage(string message)
    {
        this.contentPrinter.DrawMessage(this.Cells, message);
    }
    
    public void ClearMessage()
    {
        this.contentPrinter.ClearMessage(this.Cells);
    }

    public void SetCellItem(CellItem<T> cellItem)
    {
        var cell = GetCellByColumnAndRow((int)cellItem.Position.X, (int)cellItem.Position.Y);
        cell.Item = cellItem.Item;
    }
    
    public void SetPlayerIntoCell(Player<T> playerForCell)
    {
        if (player == null)
        {
            this.SetCellItem(new CellItem<T>(playerForCell, new Vector(playerForCell.ActualPosition.X, playerForCell.ActualPosition.Y, 0)));
            this.player = playerForCell;
        }
    }

    public void RemoveCellItem(Vector position)
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