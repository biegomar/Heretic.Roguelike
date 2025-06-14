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
}