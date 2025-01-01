using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Weapons.Types;

public class Dart : IWeaponType
{
    public string Name => nameof(Dart);
    
    public Weapon Create()
    {
        DiceThrow diceThrow = new(1, Dice.D1);
        DiceThrow diceThrowHurl = new(1, Dice.D3);
        
        return new Weapon 
        {
            Type = Name, 
            Flags = WeaponFlag.IsMany | WeaponFlag.IsMissile,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = IWeaponType.GetCount(),
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }
}