using System;

namespace Heretic.Roguelike.Amours;

[Flags]
public enum ArmourFlag
{
    IsCursed,
    IsKnown,
    DidFlash,
    IsEgo,
    IsMissile,
    IsMany,
    IsReveal
}