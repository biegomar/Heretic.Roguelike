using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.Cells;

public readonly struct CellItem<T>(T item, Vector position)
{
    public T Item { get; } = item;
    public Vector Position { get; } = position;
}