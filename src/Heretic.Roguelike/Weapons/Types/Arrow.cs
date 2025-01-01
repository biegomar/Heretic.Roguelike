using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Weapons.Types;

public class Arrow : IWeaponType
{ 
    public string Name => nameof(Arrow);
    
    public Weapon Create()
    {
        DiceThrow diceThrow = new(1, Dice.D1);
        DiceThrow diceThrowHurl = new(2, Dice.D3);
        
        return new Weapon 
        {
            Type = Name, 
            LaunchedByType = WeaponType.Bow,
            Flags = WeaponFlag.IsMany | WeaponFlag.IsMissile,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = IWeaponType.GetCount(),
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }
}