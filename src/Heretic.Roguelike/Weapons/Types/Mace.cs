using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Weapons.Types;

public class Mace : IWeaponType
{
    public string Name => nameof(Mace);
    
    public Weapon Create()
    {
        DiceThrow diceThrow = new(2, Dice.D4);
        DiceThrow diceThrowHurl = new(1, Dice.D3);
        
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