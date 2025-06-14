using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.ContentGeneration.Dungeons;

public abstract class BaseDungeonGenerator<T> : IProceduralContentGenerator<T>
{
    public MapTypes MapType { get; init; }
    public Vector Dimension { get; init; }
    public abstract IEnumerable<ICell<T>> Generate(IEnumerable<ICell<T>> elements);
        
    public IEnumerable<ICell<T>> InitializeCells()
    {
        var rooms = new List<Room<T>>();
        var width = this.Dimension.X;
        var height = this.Dimension.Y;
            
        for (int column = 0; column < width; column++)           
        {
            for(int row = 0; row < height; row++)
            {
                var instance = new Room<T>()
                {
                    X = column,
                    Y = row
                };

                rooms.Add(instance);
            }
        }

        return rooms;
    }

    public abstract IEnumerable<ICell<T>> LinkCells(IEnumerable<ICell<T>> elements);
        
    protected ICell<T> GetCellByColumnAndRow(IEnumerable<ICell<T>> cells, int column, int row)
    {
        return cells.Single(cell => cell.X == column && cell.Y == row);
    }
        
    protected Room<T>? GetRoomByColumnAndRow(IList<Room<T>> rooms, int column, int row)
    {
        return rooms.SingleOrDefault(r => r.X == column && r.Y == row);
    }
}