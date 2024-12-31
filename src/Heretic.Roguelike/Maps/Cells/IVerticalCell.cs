using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.Cells;

public interface IVerticalCell<T> : ICell<T>
{
    ICell<T>? NorthernNeighbour { get; set; }
    ICell<T>? SouthernNeighbour { get; set; }
    
    void SetNeighbours(IEnumerable<ICell<T>> cells, Vector dimensions)
    {
        var height = dimensions.Y;

        var enumerable = cells.ToList();
        
        this.NorthernNeighbour = Y - 1 < 0 ? null : GetCellByColumnAndRow(enumerable, X, Y - 1);
        this.SouthernNeighbour = Y + 1 >= height ? null : GetCellByColumnAndRow(enumerable, X, Y + 1);
    }
    
    IVerticalCell<T>? GetCellByColumnAndRow(IEnumerable<ICell<T>> cells, int column, int row)
    {
        return cells.Single(cell => cell.X == column && cell.Y == row) as IVerticalCell<T>;
    }
}