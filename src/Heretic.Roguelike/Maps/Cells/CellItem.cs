﻿using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things;

namespace Heretic.Roguelike.Maps.Cells;

public readonly struct CellItem<T>(ICreature<T> item, Vector position)
{
    public ICreature<T> Item { get; } = item;
    public Vector Position { get; } = position;
}