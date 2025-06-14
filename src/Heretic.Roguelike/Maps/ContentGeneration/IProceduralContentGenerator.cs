using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.ContentGeneration;

public interface IProceduralContentGenerator<T>
{
    MapTypes MapType { get; init; }
    
    IEnumerable<ICell<T>> Generate(IEnumerable<ICell<T>> elements);
        
    IEnumerable<ICell<T>> InitializeCells(Vector dimension);

    IEnumerable<ICell<T>> LinkCells(IEnumerable<ICell<T>> elements);
}