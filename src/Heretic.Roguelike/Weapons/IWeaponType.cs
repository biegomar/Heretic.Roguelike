using System;

namespace Heretic.Roguelike.Weapons;

public interface IWeaponType
{
    string Name { get; }
    
    Weapon Create();

    public static byte GetCount()
    {
        Random random = new();
        return (byte)(random.Next(1, 9) + 8);
    }
}