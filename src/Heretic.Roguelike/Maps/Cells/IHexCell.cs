using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.Cells;

public interface IHexCell<T> : IVerticalCell<T>, IDiagonalCell<T>;