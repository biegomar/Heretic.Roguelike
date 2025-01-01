using System;
using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Weapons;

public class Weapon
{
    public string? Type { get; init; }
    public Type? LaunchedByType { get; set; }
    public WeaponFlag Flags { get; set; }
    public byte AdditionalDamage { get; set; }
    public byte AdditionalHit { get; set; }
    public byte Count { get; set; }
    public IList<DiceThrow> Damage { get; set; } = new List<DiceThrow>();
    public IList<DiceThrow> HurlDamage { get; set; } = new List<DiceThrow>();
    
    public bool IsLaunchedBy<T>() where T : IWeaponType
    {
        return this.LaunchedByType == typeof(T);
    }
}