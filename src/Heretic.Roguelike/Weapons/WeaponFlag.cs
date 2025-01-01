using System;

namespace Heretic.Roguelike.Weapons;

[Flags]
public enum WeaponFlag
{
    IsCursed,
    IsKnown,
    DidFlash,
    IsEgo,
    IsMissile,
    IsMany,
    IsReveal 
}