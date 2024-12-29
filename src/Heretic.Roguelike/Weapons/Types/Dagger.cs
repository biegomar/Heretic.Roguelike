using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Weapons.Types;

public class Dagger : IWeaponType
{
    public string Name => nameof(Dagger);
    
    public Weapon Create()
    {
        DiceThrow diceThrow = new(1, Dice.D6);
        DiceThrow diceThrowHurl = new(1, Dice.D4);
        
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