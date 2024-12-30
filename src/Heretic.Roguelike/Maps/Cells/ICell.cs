namespace Heretic.Roguelike.Maps.Cells;

public interface ICell<T>
{
    int X { get; }
    int Y { get; }
    int Z { get; }
    
    T Item { get; set; }
}