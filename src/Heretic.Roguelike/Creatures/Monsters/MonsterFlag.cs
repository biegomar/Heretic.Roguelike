using System;

namespace Heretic.Roguelike.Creatures.Monsters;

[Flags]
public enum MonsterFlag
{
    Mean,
    Flying,
    Regeneration,
    Greedy,
    Invisible 
}