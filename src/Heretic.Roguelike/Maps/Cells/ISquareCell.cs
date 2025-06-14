using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.Cells;

public interface ISquareCell<T> : IOrthogonalCell<T>, IDiagonalCell<T>;