using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.Cells;

public interface IOrthogonalCell<T>: IHorizontalCell<T>, IVerticalCell<T>
{
    void SetNeighbours(IEnumerable<ICell<T>> cells, Vector dimensions)
    {
        var enumerable = cells.ToList();
        (this as IHorizontalCell<T>)?.SetNeighbours(enumerable, dimensions);
        (this as IVerticalCell<T>)?.SetNeighbours(enumerable, dimensions);
    }
    
    new IOrthogonalCell<T>? GetCellByColumnAndRow(IEnumerable<ICell<T>> cells, int column, int row)
    {
        return cells.Single(cell => cell.X == column && cell.Y == row) as IOrthogonalCell<T>;
    }
}