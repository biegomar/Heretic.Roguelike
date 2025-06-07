using System;
using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Players;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.Maps.ContentGeneration;

public class Landscape<T, TK> where TK : ICell<T>
{
    private readonly IProceduralContentGenerator<T, TK> proceduralContentGenerator;
    private readonly IContentPrinter<T, TK> contentPrinter;
    private readonly IDashboard<T, TK> dashboard;
    private readonly IMessagePrinter messagePrinter;
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
        IContentPrinter<T, TK> contentPrinter, IDashboard<T, TK> dashboard, IMessagePrinter messagePrinter) : this(dimension,
        proceduralContentGenerator, contentPrinter, dashboard, messagePrinter, string.Empty)
    {
    }

    public Landscape(Vector dimension, IProceduralContentGenerator<T, TK> proceduralContentGenerator,
        IContentPrinter<T, TK> contentPrinter, IDashboard<T, TK> dashboard, IMessagePrinter messagePrinter, string title)
    {
        this.proceduralContentGenerator = proceduralContentGenerator;
        this.contentPrinter = contentPrinter;
        this.dashboard = dashboard;
        this.messagePrinter = messagePrinter;
        
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

    public void DrawDashboard(Vector position)
    {
        this.dashboard.DrawDashboard(this.Cells, this.player!, this.CurrentFloor, position);;
    }

    public void DrawCellItemAtPosition(Vector position)
    {
        this.contentPrinter.DrawCellItemAtPosition(this.Cells, position);
    }

    public void DrawSingleCellAtPosition(Vector startVector, Vector position)
    {
        this.contentPrinter.DrawSingleCellAtPosition(this.Cells, startVector, position);
    }

    public void QueueMessage(string message)
    {
        this.messagePrinter.QueueMessage(message);
    }

    public void PrintMessages()
    {
        this.messagePrinter.PrintMessages();
    }
    
    public void ClearMessage()
    {
        this.messagePrinter.ClearMessage();
    }

    public void ClearLandscape()
    {
        this.contentPrinter.ClearScreen();
    }

    public bool IsCellVisible(Vector position)
    {
        if (IsWithinBounds(position))
        {
            var cell = GetCellByColumnAndRow(position);
            return cell.IsVisible;    
        }

        return false;
    }

    public void SetCellVisibility(Vector position, bool isVisible)
    {
        if (IsWithinBounds(position))
        {
            var cell = GetCellByColumnAndRow(position);
            cell.IsVisible = isVisible;    
        }
    }
    
    public void SetCellItem(CellItem<T> cellItem)
    {
        if (IsWithinBounds(cellItem.Position))
        {
            var cell = GetCellByColumnAndRow(cellItem.Position);
            cell.Item = cellItem.Item;    
        }
    }

    public CellItem<T>? GetCellItem(Vector position)
    {
        if (IsWithinBounds(position))
        {
            var cell = GetCellByColumnAndRow(position);
            return cell.Item != null ? new CellItem<T>(cell.Item, position) : null;    
        }

        return null;
    }
    
    public void SetPlayerIntoCell(Player<T>? playerForCell)
    {
        if (this.player == null && playerForCell != null && IsWithinBounds(playerForCell.ActualPosition))
        {
            this.SetCellItem(new CellItem<T>(playerForCell, new Vector(playerForCell.ActualPosition.X, playerForCell.ActualPosition.Y, 0)));
            this.player = playerForCell;
        }
    }

    public void RemoveCellItem(Vector position)
    {
        if (IsWithinBounds(position))
        {
            var cell = GetCellByColumnAndRow(position);
            cell.Item = null!;    
        }
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
    
    protected TK GetCellByColumnAndRow(Vector position)
    {
        return this.Cells.Single(cell => cell.X == (int)position.X && cell.Y == (int)position.Y);
    }
    
    protected bool IsWithinBounds(Vector position)
    {
        return position.X >= 0 && position.X < dimension.X && position.Y >= 0 && position.Y < dimension.Y;
    }
}