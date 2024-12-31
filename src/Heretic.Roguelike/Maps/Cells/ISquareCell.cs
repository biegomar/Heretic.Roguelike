using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.Cells;

public interface ISquareCell<T> : IOrthogonalCell<T>, IDiagonalCell<T>
{
    void SetNeighbours(IEnumerable<ICell<T>> cells, Vector dimensions)
    {
        var enumerable = cells.ToList();
        (this as IOrthogonalCell<T>)?.SetNeighbours(enumerable, dimensions);
        (this as IDiagonalCell<T>)?.SetNeighbours(enumerable, dimensions);
    }
    
    new ISquareCell<T>? GetCellByColumnAndRow(IEnumerable<ICell<T>> cells, int column, int row)
    {
        return cells.Single(cell => cell.X == column && cell.Y == row) as ISquareCell<T>;
    }
}