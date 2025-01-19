using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things;

namespace Heretic.Roguelike.Maps.Cells;

public readonly struct CellItem<T>(IThing<T> item, Vector position)
{
    public IThing<T> Item { get; } = item;
    public Vector Position { get; } = position;
}