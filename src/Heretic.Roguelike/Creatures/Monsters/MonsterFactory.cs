using System;
using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters;

public class MonsterFactory<T>
{
 private readonly IDictionary<MonsterBreed, T> icons;
    
    public MonsterFactory(IDictionary<MonsterBreed, T> icons)
    {
        this.icons = icons;
    }

    public Monster<T> CreateMonster(MonsterBreed monsterBreed)
    {
        return monsterBreed switch
        {
            MonsterBreed.Aquator => GetAquator(),
            MonsterBreed.Bat => GetBat(),
            MonsterBreed.Centaur => GetCentaur(),
            MonsterBreed.Dragon => GetDragon(),
            MonsterBreed.Emu => GetEmu(),
            MonsterBreed.VenusFlytrap => GetVenusFlytrap(),
            MonsterBreed.Griffin => GetGriffin(),
            MonsterBreed.Hobgoblin => GetHobgoblin(),
            MonsterBreed.IceMonster => GetIceMonster(),
            MonsterBreed.Jabberwock => GetJabberwock(),
            MonsterBreed.Kestrel => GetKestrel(),
            MonsterBreed.Leprechaun => GetLeprechaun(),
            MonsterBreed.Medusa => GetMedusa(),
            MonsterBreed.Nymph => GetNymph(),
            MonsterBreed.Orc => GetOrc(),
            MonsterBreed.Phantom => GetPhantom(),
            MonsterBreed.Quagga => GetQuagga(),
            MonsterBreed.Rattlesnake => GetRattlesnake(),
            MonsterBreed.Snake => GetSnake(),
            MonsterBreed.Troll => GetTroll(),
            MonsterBreed.Urvile => GetUrvile(),
            MonsterBreed.Vampire => GetVampire(),
            MonsterBreed.Wraith => GetWraith(),
            MonsterBreed.Xeroc => GetXeroc(),
            MonsterBreed.Yeti => GetYeti(),
            MonsterBreed.Zombie => GetZombie(),
            _ => throw new ArgumentOutOfRangeException(nameof(monsterBreed), monsterBreed, null)
        };
    }

    private Monster<T> GetZombie()
    {
        byte expLevel = 2;
        DiceThrow diceThrow = new(1, Dice.D8);
        
        return new()
        {
            Breed = MonsterBreed.Zombie,
            ExperienceLevel = expLevel,
            Experience = 6,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = 8,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Zombie]
        };
    }

    private Monster<T> GetYeti()
    {
        byte expLevel = 4;
        DiceThrow diceThrow = new(1, Dice.D6);
        
        return new()
        {
            Breed = MonsterBreed.Yeti,
            ExperienceLevel = expLevel,
            Experience = 50,
            TreasurePercentage = 30,
            AmorClass = 6,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow, diceThrow},
            Icon = this.icons[MonsterBreed.Yeti]
        };
    }

    private Monster<T> GetXeroc()
    {
        byte expLevel = 7;
        DiceThrow diceThrow = new(3, Dice.D4);
        
        return new()
        {
            Breed = MonsterBreed.Xeroc,
            ExperienceLevel = expLevel,
            Experience = 100,
            TreasurePercentage = 30,
            AmorClass = 7,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Xeroc]
        };
    }

    private Monster<T> GetWraith()
    {
        byte expLevel = 5;
        DiceThrow diceThrow = new(1, Dice.D6);
        
        return new()
        {
            Breed = MonsterBreed.Wraith,
            ExperienceLevel = expLevel,
            Experience = 55,
            TreasurePercentage = 0,
            AmorClass = 4,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Wraith]
        };
    }

    private Monster<T> GetVampire()
    {
        byte expLevel = 8;
        DiceThrow diceThrow = new(1, Dice.D10);
        
        return new()
        {
            Breed = MonsterBreed.Vampire,
            ExperienceLevel = expLevel,
            Experience = 350,
            Flags = MonsterFlag.Mean | MonsterFlag.Regeneration,
            TreasurePercentage = 20,
            AmorClass = 1,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Vampire]
        };
    }

    private Monster<T> GetUrvile()
    {
        byte expLevel = 7;
        DiceThrow diceThrowD3 = new(1, Dice.D3);
        DiceThrow diceThrowD6 = new(4, Dice.D6);
        
        return new()
        {
            Breed = MonsterBreed.Urvile,
            ExperienceLevel = expLevel,
            Experience = 190,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = -2,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrowD3, diceThrowD3, diceThrowD3, diceThrowD6},
            Icon = this.icons[MonsterBreed.Urvile]
        };
    }

    private Monster<T> GetTroll()
    {
        byte expLevel = 6;
        DiceThrow diceThrowD6 = new(2, Dice.D6);
        DiceThrow diceThrowD8 = new(1, Dice.D8);
        
        return new()
        {
            Breed = MonsterBreed.Troll,
            ExperienceLevel = expLevel,
            Experience = 120,
            Flags = MonsterFlag.Mean | MonsterFlag.Regeneration,
            TreasurePercentage = 50,
            AmorClass = 4,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrowD8, diceThrowD8, diceThrowD6},
            Icon = this.icons[MonsterBreed.Troll]
        };
    }

    private Monster<T> GetSnake()
    {
        byte expLevel = 2;
        DiceThrow diceThrow = new(1, Dice.D3);
        
        return new()
        {
            Breed = MonsterBreed.Snake,
            ExperienceLevel = expLevel,
            Experience = 1,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = 8,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Snake]
        };
    }

    private Monster<T> GetRattlesnake()
    {
        byte expLevel = 2;
        DiceThrow diceThrow = new(1, Dice.D6);
        
        return new()
        {
            Breed = MonsterBreed.Rattlesnake,
            ExperienceLevel = expLevel,
            Experience = 9,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = 3,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Rattlesnake]
        };
    }

    private Monster<T> GetQuagga()
    {
        byte expLevel = 3;
        DiceThrow diceThrowD2 = new(1, Dice.D2);
        DiceThrow diceThrowD4 = new(1, Dice.D4);
        
        return new()
        {
            Breed = MonsterBreed.Quagga,
            ExperienceLevel = expLevel,
            Experience = 32,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 30,
            AmorClass = 2,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrowD2, diceThrowD2, diceThrowD4},
            Icon = this.icons[MonsterBreed.Quagga]
        };
    }

    private Monster<T> GetPhantom()
    {
        byte expLevel = 8;
        DiceThrow diceThrow = new(4, Dice.D4);
        
        return new()
        {
            Breed = MonsterBreed.Phantom,
            ExperienceLevel = expLevel,
            Experience = 120,
            Flags = MonsterFlag.Invisible,
            TreasurePercentage = 0,
            AmorClass = 3,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Phantom]
        };
    }

    private Monster<T> GetOrc()
    {
        byte expLevel = 1;
        DiceThrow diceThrow = new(1, Dice.D8);
        
        return new()
        {
            Breed = MonsterBreed.Orc,
            ExperienceLevel = expLevel,
            Experience = 5,
            Flags = MonsterFlag.Greedy,
            TreasurePercentage = 15,
            AmorClass = 6,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Orc]
        };
    }

    private Monster<T> GetNymph()
    {
        byte expLevel = 3;
        DiceThrow diceThrow = new(0, Dice.D0);
        
        return new()
        {
            Breed = MonsterBreed.Nymph,
            ExperienceLevel = expLevel,
            Experience = 37,
            TreasurePercentage = 100,
            AmorClass = 9,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Nymph]
        };
    }

    private Monster<T> GetMedusa()
    {
        byte expLevel = 8;
        DiceThrow diceThrowD4 = new(3, Dice.D4);
        DiceThrow diceThrowD5 = new(2, Dice.D5);
        
        return new()
        {
            Breed = MonsterBreed.Medusa,
            ExperienceLevel = expLevel,
            Experience = 200,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 40,
            AmorClass = 2,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrowD4, diceThrowD4, diceThrowD5},
            Icon = this.icons[MonsterBreed.Medusa]
        };
    }

    private Monster<T> GetLeprechaun()
    {
        byte expLevel = 3;
        DiceThrow diceThrow = new(1, Dice.D2);
        
        return new()
        {
            Breed = MonsterBreed.Leprechaun,
            ExperienceLevel = expLevel,
            Experience = 10,
            Flags = MonsterFlag.Greedy,
            TreasurePercentage = 0,
            AmorClass = 8,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Leprechaun]
        };
    }

    private Monster<T> GetKestrel()
    {
        byte expLevel = 1;
        DiceThrow diceThrow = new(1, Dice.D4);
        
        return new()
        {
            Breed = MonsterBreed.Kestrel,
            ExperienceLevel = expLevel,
            Experience = 1,
            Flags = MonsterFlag.Mean | MonsterFlag.Flying,
            TreasurePercentage = 0,
            AmorClass = 7,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Kestrel]
        };
    }

    private Monster<T> GetJabberwock()
    {
        byte expLevel = 15;
        DiceThrow diceThrowD4 = new(2, Dice.D4);
        DiceThrow diceThrowD12 = new(2, Dice.D12);
        
        return new()
        {
            Breed = MonsterBreed.Jabberwock,
            ExperienceLevel = expLevel,
            Experience = 4000,
            TreasurePercentage = 70,
            AmorClass = 6,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrowD12, diceThrowD4},
            Icon = this.icons[MonsterBreed.Jabberwock]
        };
    }

    private Monster<T> GetIceMonster()
    {
        byte expLevel = 1;
        DiceThrow diceThrow = new(1, Dice.D2);
        
        return new()
        {
            Breed = MonsterBreed.IceMonster,
            ExperienceLevel = expLevel,
            Experience = 15,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = 9,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.IceMonster]
        };
    }

    private Monster<T> GetHobgoblin()
    {
        byte expLevel = 1;
        DiceThrow diceThrow = new(1, Dice.D8);
        
        return new()
        {
            Breed = MonsterBreed.Hobgoblin,
            ExperienceLevel = expLevel,
            Experience = 3,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = 5,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Hobgoblin]
        };
    }

    private Monster<T> GetGriffin()
    {
        byte expLevel = 13;
        DiceThrow diceThrowD3 = new(4, Dice.D3);
        DiceThrow diceThrowD5 = new(3, Dice.D5);
        
        return new()
        {
            Breed = MonsterBreed.Griffin,
            ExperienceLevel = expLevel,
            Experience = 2000,
            Flags = MonsterFlag.Mean | MonsterFlag.Flying | MonsterFlag.Regeneration,
            TreasurePercentage = 20,
            AmorClass = 2,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrowD3, diceThrowD5, diceThrowD3},
            Icon = this.icons[MonsterBreed.Griffin]
        };
    }

    private Monster<T> GetVenusFlytrap()
    {
        //special behaviour needed.
        
        byte expLevel = 8;
        DiceThrow diceThrow = new(1, Dice.D0);
        
        return new()
        {
            Breed = MonsterBreed.VenusFlytrap,
            ExperienceLevel = expLevel,
            Experience = 80,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = 3,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.VenusFlytrap]
        };
    }

    private Monster<T> GetEmu()
    {
        byte expLevel = 1;
        DiceThrow diceThrow = new(1, Dice.D2);
        
        return new()
        {
            Breed = MonsterBreed.Emu,
            ExperienceLevel = expLevel,
            Experience = 2,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = 7,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Emu]
        };
    }

    private Monster<T> GetDragon()
    {
        byte expLevel = 10;
        DiceThrow diceThrowD8 = new(1, Dice.D8);
        DiceThrow diceThrowD10 = new(3, Dice.D10);
        
        return new()
        {
            Breed = MonsterBreed.Dragon,
            ExperienceLevel = expLevel,
            Experience = 6800,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 100,
            AmorClass = -1,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrowD8, diceThrowD8, diceThrowD10},
            Icon = this.icons[MonsterBreed.Dragon]
        };
    }

    private Monster<T> GetCentaur()
    {
        byte expLevel = 4;
        DiceThrow diceThrow = new(1, Dice.D6);
        
        return new()
        {
            Breed = MonsterBreed.Centaur,
            ExperienceLevel = expLevel,
            Experience = 25,
            TreasurePercentage = 15,
            AmorClass = 4,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow, diceThrow},
            Icon = this.icons[MonsterBreed.Centaur]
        };
    }

    private Monster<T> GetBat()
    {
        byte expLevel = 1;
        DiceThrow diceThrow = new(1, Dice.D2);
        
        return new()
        {
            Breed = MonsterBreed.Bat,
            ExperienceLevel = expLevel,
            Experience = 1,
            Flags = MonsterFlag.Flying,
            TreasurePercentage = 0,
            AmorClass = 3,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = this.icons[MonsterBreed.Bat]
        };
    }

    private Monster<T> GetAquator()
    {
        byte expLevel = 5;
        DiceThrow diceThrow = new(0, Dice.D0);
        
        return new()
        {
            Breed = MonsterBreed.Aquator,
            ExperienceLevel = expLevel,
            Experience = 20,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = 2,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow, diceThrow},
            Icon = this.icons[MonsterBreed.Aquator]
        };
    }      
}