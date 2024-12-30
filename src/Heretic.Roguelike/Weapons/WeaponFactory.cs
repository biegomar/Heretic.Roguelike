using System;
using System.Collections.Generic;
using Heretic.Roguelike.Weapons.Types;

namespace Heretic.Roguelike.Weapons;

public class WeaponFactory
{
    private readonly IDictionary<string, IWeaponType> weaponTypes = new Dictionary<string, IWeaponType>()
    {
        {nameof(Arrow), new Arrow()},
        {nameof(Bolt), new Bolt()},
        {nameof(Bow), new Bow()},
        {nameof(Crossbow), new Crossbow()},
        {nameof(Dagger), new Dagger()},
        {nameof(Dart), new Dart()},
        {nameof(Flame), new Flame()},
        {nameof(Mace), new Mace()},
        {nameof(Spear), new Spear()},
        {nameof(Sword), new Sword()},
        {nameof(TwoSword), new TwoSword()},
    };
        
    public Weapon CreateWeapon(string weaponType) 
    {
        if (!this.weaponTypes.TryGetValue(weaponType, out var armour))
        {
            throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, "Weapon type not registered.");
        }
        
        return armour.Create();
    }
    
    public void RegisterWeaponType(IWeaponType weaponType)
    {
        this.weaponTypes.TryAdd(weaponType.Name, weaponType);
    }
}