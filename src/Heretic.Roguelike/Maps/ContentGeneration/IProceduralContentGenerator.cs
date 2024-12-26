using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.ContentGeneration;

public interface IProceduralContentGenerator<T>
{
    public IList<Cell<T>> Generate(IList<Cell<T>> cells);
        
    public IList<Cell<T>> InitializeCells(IList<Cell<T>> cells, Vector dimension);

    public IList<Cell<T>> LinkCells(IList<Cell<T>> cells);
}