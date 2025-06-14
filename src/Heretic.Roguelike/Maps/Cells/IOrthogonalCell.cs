using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.Cells;

public interface IOrthogonalCell<T>: IHorizontalCell<T>, IVerticalCell<T>;