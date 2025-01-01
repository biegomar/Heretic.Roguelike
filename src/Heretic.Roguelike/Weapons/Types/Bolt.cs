using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Weapons.Types;

public class Bolt : IWeaponType
{
    public string Name => nameof(Bolt);
    
    public Weapon Create()
    {
        DiceThrow diceThrow = new(1, Dice.D2);
        DiceThrow diceThrowHurl = new(2, Dice.D5);
        
        return new Weapon 
        {
            Type = Name, 
            LaunchedByType = WeaponType.Crossbow,
            Flags = WeaponFlag.IsMany | WeaponFlag.IsMissile,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = IWeaponType.GetCount(),
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }
}