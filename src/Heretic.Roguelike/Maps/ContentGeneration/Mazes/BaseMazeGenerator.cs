using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.ContentGeneration.Mazes;

public abstract class BaseMazeGenerator<T> : IProceduralContentGenerator<T>
{
    public MapTypes MapType { get; init; }
    public Vector Dimension { get; init; }
    
    public abstract IEnumerable<ICell<T>> Generate(IEnumerable<ICell<T>> elements);

    public virtual IEnumerable<ICell<T>> InitializeCells()
    {
        var cells = new List<ICell<T>>();
        var width = this.Dimension.X;
        var height = this.Dimension.Y;
            
        for (int column = 0; column < width; column++)           
        {
            for(int row = 0; row < height; row++)
            {
                var instance = new Cell<T>()
                {
                    X = column,
                    Y = row
                };

                cells.Add(instance);
            }
        }

        return cells;
    }

    public abstract IEnumerable<ICell<T>> LinkCells(IEnumerable<ICell<T>> elements);

    protected ICell<T> GetCellByColumnAndRow(IEnumerable<ICell<T>> cells, int column, int row)
    {
        return cells.Single(cell => cell.X == column && cell.Y == row);
    }
}