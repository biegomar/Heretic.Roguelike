using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Weapons.Types;

public class Spear : IWeaponType
{
    public string Name => nameof(Spear);
    
    public Weapon Create()
    {
        DiceThrow diceThrow = new(2, Dice.D3);
        DiceThrow diceThrowHurl = new(1, Dice.D6);
        
        return new Weapon 
        {
            Type = Name,
            Flag = WeaponFlag.IsMissile,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = 1,
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }
}