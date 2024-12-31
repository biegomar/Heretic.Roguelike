using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.Cells;

public interface IDiagonalCell<T> : ICell<T>
{
    ICell<T>? NorthernEastNeighbour { get; set; }
    ICell<T>? SouthernWestNeighbour { get; set; }
    ICell<T>? SouthernEastNeighbour { get; set; }
    ICell<T>? NorthernWestNeighbour { get; set; }
    
    void SetNeighbours(IEnumerable<ICell<T>> cells, Vector dimensions)
    {
        var width = dimensions.X;
        var height = dimensions.Y;
        
        this.NorthernEastNeighbour = X + 1 >= width || Y - 1 < 0 ? null : GetCellByColumnAndRow(cells, X + 1, Y - 1);
        this.SouthernWestNeighbour = X - 1 < 0 || Y + 1 >= height ? null : GetCellByColumnAndRow(cells, X - 1, Y + 1);
        this.SouthernEastNeighbour = X + 1 >= width || Y + 1 >= height ? null : GetCellByColumnAndRow(cells, X + 1, Y + 1);
        this.NorthernWestNeighbour = X - 1 < 0 || Y - 1 < 0 ? null : GetCellByColumnAndRow(cells, X - 1, Y - 1);
    }
    
    IDiagonalCell<T>? GetCellByColumnAndRow(IEnumerable<ICell<T>> cells, int column, int row)
    {
        return cells.Single(cell => cell.X == column && cell.Y == row) as IDiagonalCell<T>;
    }
}