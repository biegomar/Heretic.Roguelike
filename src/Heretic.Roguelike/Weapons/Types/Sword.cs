using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Weapons.Types;

public class Sword : IWeaponType
{
    public string Name => nameof(Sword);
    
    public Weapon Create()
    {
        DiceThrow diceThrow = new(3, Dice.D4);
        DiceThrow diceThrowHurl = new(1, Dice.D2);
        
        return new Weapon 
        {
            Type = Name,
            LaunchedByType = null,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = 1,
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }
}