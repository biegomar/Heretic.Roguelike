using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.Cells;

public interface IHorizontalCell<T> : ICell<T>
{
    ICell<T>? EasternNeighbour { get; set; }
    ICell<T>? WesternNeighbour { get; set; }
}