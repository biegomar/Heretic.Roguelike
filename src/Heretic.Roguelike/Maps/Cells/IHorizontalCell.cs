using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.Cells;

public interface IHorizontalCell<T> : ICell<T>
{
    ICell<T>? EasternNeighbour { get; set; }
    ICell<T>? WesternNeighbour { get; set; }

    void SetNeighbours(IEnumerable<ICell<T>> cells, Vector dimensions)
    {
        var width = dimensions.X;
        
        this.EasternNeighbour = X + 1 >= width ? null : GetCellByColumnAndRow(cells, X + 1, Y); 
        this.WesternNeighbour = X - 1 < 0 ? null : GetCellByColumnAndRow(cells, X - 1, Y);
    }
    
    IHorizontalCell<T>? GetCellByColumnAndRow(IEnumerable<ICell<T>> cells, int column, int row)
    {
        return cells.Single(cell => cell.X == column && cell.Y == row) as IHorizontalCell<T>;
    }
}