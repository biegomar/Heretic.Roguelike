using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Weapons;

public class Weapon
{
    public WeaponType Type { get; set; }
    public WeaponType? LaunchedByType { get; set; }
    public WeaponFlag Flag { get; set; }
    public byte AdditionalDamage { get; set; }
    public byte AdditionalHit { get; set; }
    public byte Count { get; set; }
    public IList<DiceThrow> Damage { get; set; } = new List<DiceThrow>();
    public IList<DiceThrow> HurlDamage { get; set; } = new List<DiceThrow>();
}