using System;
using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Weapons;

public class WeaponFactory
{
    private readonly Random random = new Random();
        
    public Weapon CreateWeapon(WeaponType weaponType) 
    {
        return weaponType switch 
        {
            WeaponType.Mace => GetMace(),
            WeaponType.Sword => GetSword(),
            WeaponType.Bow => GetBow(),
            WeaponType.Arrow => GetArrow(),
            WeaponType.Dagger => GetDagger(),
            WeaponType.TwoSword => GetTwoSword(),
            WeaponType.Dart => GetDart(),
            WeaponType.Crossbow => GetCrossbow(),
            WeaponType.Bolt => GetBolt(),
            WeaponType.Spear => GetSpear(),
            WeaponType.Flame => GetFlame(),
            _ => throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null)
        };
    }

    private Weapon GetMace() 
    {
        DiceThrow diceThrow = new(2, Dice.D4);
        DiceThrow diceThrowHurl = new(1, Dice.D3);
        
        return new Weapon 
        {
            Type = WeaponType.Mace,
            LaunchedByType = null,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = 1,
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }
        
    private Weapon GetSword() 
    {
        DiceThrow diceThrow = new(3, Dice.D4);
        DiceThrow diceThrowHurl = new(1, Dice.D2);
        
        return new Weapon 
        {
            Type = WeaponType.Sword,
            LaunchedByType = null,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = 1,
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }
        
    private Weapon GetBow() 
    {
        DiceThrow diceThrow = new(1, Dice.D1);
        DiceThrow diceThrowHurl = new(1, Dice.D1);
        
        return new Weapon 
        {
            Type = WeaponType.Bow,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = 1,
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }
        
    private Weapon GetArrow() 
    {
        DiceThrow diceThrow = new(1, Dice.D1);
        DiceThrow diceThrowHurl = new(2, Dice.D3);
        
        return new Weapon 
        {
            Type = WeaponType.Arrow, 
            LaunchedByType = WeaponType.Bow,
            Flag = WeaponFlag.IsMany | WeaponFlag.IsMissile,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = this.GetCount(),
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }
        
    private Weapon GetDagger() 
    {
        DiceThrow diceThrow = new(1, Dice.D6);
        DiceThrow diceThrowHurl = new(1, Dice.D4);
        
        return new Weapon 
        {
            Type = WeaponType.Dagger,
            Flag = WeaponFlag.IsMissile,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = 1,
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }
        
    private Weapon GetTwoSword() 
    {
        DiceThrow diceThrow = new(4, Dice.D4);
        DiceThrow diceThrowHurl = new(1, Dice.D2);
        
        return new Weapon 
        {
            Type = WeaponType.TwoSword, 
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = 1,
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }
        
    private Weapon GetDart() 
    {
        DiceThrow diceThrow = new(1, Dice.D1);
        DiceThrow diceThrowHurl = new(1, Dice.D3);
        
        return new Weapon 
        {
            Type = WeaponType.Dart, 
            Flag = WeaponFlag.IsMany | WeaponFlag.IsMissile,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = this.GetCount(),
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }

    private Weapon GetCrossbow() 
    {
        DiceThrow diceThrow = new(1, Dice.D1);
        DiceThrow diceThrowHurl = new(1, Dice.D1);
        
        return new Weapon 
        {
            Type = WeaponType.Crossbow, 
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = 1,
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }

    private Weapon GetBolt() 
    {
        DiceThrow diceThrow = new(1, Dice.D2);
        DiceThrow diceThrowHurl = new(2, Dice.D5);
        
        return new Weapon 
        {
            Type = WeaponType.Bolt, 
            LaunchedByType = WeaponType.Crossbow,
            Flag = WeaponFlag.IsMany | WeaponFlag.IsMissile,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = this.GetCount(),
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }

    private Weapon GetSpear() 
    {
        DiceThrow diceThrow = new(2, Dice.D3);
        DiceThrow diceThrowHurl = new(1, Dice.D6);
        
        return new Weapon 
        {
            Type = WeaponType.Spear,
            Flag = WeaponFlag.IsMissile,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = 1,
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }

    private Weapon GetFlame() 
    {
        DiceThrow diceThrow = new(2, Dice.D4);
        DiceThrow diceThrowHurl = new(1, Dice.D3);
        
        return new Weapon 
        {
            Type = WeaponType.Flame,
            AdditionalDamage = 0,
            AdditionalHit = 0,
            Count = 1,
            Damage = new List<DiceThrow>() {diceThrow},
            HurlDamage = new List<DiceThrow>() {diceThrowHurl}
        };
    }

    private byte GetCount()
    {
        return (byte)(this.random.Next(1, 9) + 8);
    }
}