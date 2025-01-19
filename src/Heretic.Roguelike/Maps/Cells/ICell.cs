using System.Collections.Generic;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things;

namespace Heretic.Roguelike.Maps.Cells;

public interface ICell<T>
{
    int X { get; init; }
    int Y { get; init;}
    int Z { get; init;}
    
    int PathCount { get; set; }

    bool IsVisited { get; set; }

    ICell<T>? Predecessor { get; set; }
    
    IThing<T>? Item { get; set; }

    IDictionary<Directions, ICell<T>?> Neighbours { get; }
    IList<ICell<T>> LinkedCells { get; }

    void SetNeighbours(IEnumerable<ICell<T>> cells, Vector dimensions)
    {
    }
    
    void LinkCell(ICell<T> cellToLink)
    {
        if (!this.LinkedCells.Contains(cellToLink))
        {
            this.LinkedCells.Add(cellToLink);
            cellToLink.LinkCell(this);
        }
    }
}