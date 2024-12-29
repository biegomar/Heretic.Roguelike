using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Weapons.Types;

public class Crossbow : IWeaponType
{
    public string Name => nameof(Crossbow);
    
    public Weapon Create()
    {
        DiceThrow diceThrow = new(1, Dice.D1);
        DiceThrow diceThrowHurl = new(1, Dice.D1);
        
        return new Weapon 
        {
            Type = Name, 
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = 1,
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }
}