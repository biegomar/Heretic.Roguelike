using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.Cells;

public interface IVerticalCell<T> : ICell<T>
{
    ICell<T>? NorthernNeighbour { get; set; }
    ICell<T>? SouthernNeighbour { get; set; }
}