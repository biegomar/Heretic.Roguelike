using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration.Dungeons;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.ContentGeneration.Mazes;

public abstract class BaseMazeGenerator<T, TK> : IProceduralContentGenerator<T, TK> where TK : class, ICell<T>, new()
{
    public abstract IList<TK> Generate(IList<TK> cells);
    public abstract IList<TK> LinkCells(IList<TK> cells);

    public virtual IList<TK> InitializeCells(Vector dimension)
    {
        var cells = new List<TK>();
        var width = dimension.X;
        var height = dimension.Y;
            
        for (int column = 0; column < width; column++)           
        {
            for(int row = 0; row < height; row++)
            {
                var instance = new TK()
                {
                    X = column,
                    Y = row
                };

                cells.Add(instance);
            }
        }

        return cells;
    }

    protected TK GetCellByColumnAndRow(IList<TK> cells, int column, int row)
    {
        return cells.Single(cell => cell.X == column && cell.Y == row);
    }
}