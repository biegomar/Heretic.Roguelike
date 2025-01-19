using System;

namespace Heretic.Roguelike.Things.Monsters;

[Flags]
public enum MonsterFlag
{
    Mean,
    Flying,
    Regeneration,
    Greedy,
    Invisible 
}