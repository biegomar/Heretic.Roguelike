using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Interfaces;


namespace Heretic.Roguelike.Maps.Cells;

public class Cell<T> : IOrthogonalCell<T>
{
    private readonly IDictionary<Directions, ICell<T>?> neighbours = new Dictionary<Directions, ICell<T>?>();
    private readonly IList<ICell<T>> linkedCells = new List<ICell<T>>();

    public int X { get; init; }
    public int Y { get; init; }
    public int Z { get; init; }
    public IThing<T>? Item { get; set; }

    public bool IsVisible { get; set; }

    public int PathCount { get; set; }

    public bool IsVisited { get; set; }
    public bool IsHidden { get; set; }

    public ICell<T>? Predecessor { get; set; }

    public ICell<T>? NorthernNeighbour
    {
        get => neighbours[Directions.North];
        set => neighbours[Directions.North] = value;
    }

    public ICell<T>? EasternNeighbour
    {
        get => neighbours[Directions.East];
        set => neighbours[Directions.East] = value;
    }

    public ICell<T>? SouthernNeighbour
    {
        get => neighbours[Directions.South];
        set => neighbours[Directions.South] = value;
    }

    public ICell<T>? WesternNeighbour
    {
        get => neighbours[Directions.West];
        set => neighbours[Directions.West] = value;
    }

    public IList<ICell<T>> LinkedCells => this.linkedCells;
    

    public IDictionary<Directions, ICell<T>?> Neighbours => this.neighbours;

    public void LinkCell(Cell<T> cellToLink)
    {
        if (!this.LinkedCells.Contains(cellToLink))
        {
            this.LinkedCells.Add(cellToLink);
            cellToLink.LinkCell(this);
        }
    }
    
    public void SetNeighbours(IEnumerable<ICell<T>> cells, Vector dimensions)
    {
        var width = dimensions.X;
        var height = dimensions.Y;

        var enumerable = cells.ToList();
        
        // OrthogonalCell
        this.EasternNeighbour = X + 1 >= width ? null : GetCellByColumnAndRow(enumerable, X + 1, Y); 
        this.WesternNeighbour = X - 1 < 0 ? null : GetCellByColumnAndRow(enumerable, X - 1, Y);
        this.NorthernNeighbour = Y - 1 < 0 ? null : GetCellByColumnAndRow(enumerable, X, Y - 1);
        this.SouthernNeighbour = Y + 1 >= height ? null : GetCellByColumnAndRow(enumerable, X, Y + 1);
    }
    
    ICell<T> GetCellByColumnAndRow(IEnumerable<ICell<T>> cells, int column, int row)
    {
        return cells.Single(cell => cell.X == column && cell.Y == row);
    }
}