using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.ContentGeneration;

public interface IProceduralContentGenerator<T, TK> where TK : ICell<T>
{
    public IList<TK> Generate(IList<TK> cells);
        
    public IList<TK> InitializeCells(Vector dimension);

    public IList<TK> LinkCells(IList<TK> cells);
}