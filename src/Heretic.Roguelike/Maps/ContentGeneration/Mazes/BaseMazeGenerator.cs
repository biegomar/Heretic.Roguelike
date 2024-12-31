using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration.Dungeons;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.ContentGeneration.Mazes;

public abstract class BaseMazeGenerator<T, TK> : IProceduralContentGenerator<T, TK> where TK : class, ICell<T>, new()
{
    public abstract IList<TK> Generate(IList<TK> cells);

    public IList<TK> InitializeCells(Vector dimension)
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

    public IList<TK> LinkCells(IList<TK> cells)
    {
        var width = cells.Max(cell => cell.X) + 1;
        var height = cells.Max(cell => cell.Y) + 1;
            
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cellToLink = GetCellByColumnAndRow(cells, x, y);
                if (cellToLink is IOrthogonalCell<T> orthogonalCell)
                {
                    orthogonalCell.SetNeighbours(cells, new Vector(width, height, 0));
                }
            }
        }

        return cells;
    }

    protected TK GetCellByColumnAndRow(IList<TK> cells, int column, int row)
    {
        return cells.Single(cell => cell.X == column && cell.Y == row);
    }
}