using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.SimpleConsoleSample.Battles;

public class BattleArena : IBattleArena<char>
{
    private readonly Random random = new Random();
    private byte additionalDamage;
    private byte additionalHit;
    private IList<DiceThrow> damage = new List<DiceThrow>();
    
    public void Fight(ICreature<char> attacker, ICreature<char> defender)
    {
        if (attacker is Player<char> player)
        {
            this.SetAdditionalDamageAndHit(player);
        }
        
        foreach (var diceThrow in attacker.Damage)
        {
            var strengthCorrector = this.additionalHit + this.CalculateStrengthCorrector(attacker.Strength);
            if (IsAttackSuccessful(attacker, defender, strengthCorrector))
            {
                ushort rollResult = diceThrow.Dice.Roll(diceThrow.Tries);

                ushort attackResult = (ushort)(rollResult + this.additionalDamage + this.CalculateDamageCorrector(attacker.Strength));
                
                defender.HitPoints -= Math.Max((ushort)0, attackResult);
            }
        }
    }

    public void Fight(IList<ICreature<char>> attackerGroup, IList<ICreature<char>> defenderGroup)
    {
        //TODO
        throw new NotImplementedException();
    }
    
    private byte CalculateStrengthCorrector(ushort strength)
    {
        byte add = 4;

        if (strength < 8) return (byte)(strength - 7);
        
        if (strength < 31) add--;
        if (strength < 21) add--;
        if (strength < 19) add--;
        if (strength < 17) add--;
        
        return add;
    }
    
    private byte CalculateDamageCorrector(ushort strength)
    {
        byte add = 6;

        if (strength < 8) return (byte)(strength - 7);
        
        if (strength < 31) add--;
        if (strength < 22) add--;
        if (strength < 20) add--;
        if (strength < 18) add--;
        if (strength < 17) add--;
        if (strength < 16) add--;
        
        return add;
    }
    
    private bool IsAttackSuccessful(ICreature<char> attacker, ICreature<char> defender, int attackerHitBonus)
    {
        var res = this.random.Next(1,21);
        var need = 20 - attacker.ExperienceLevel - defender.AmorClass;

        return res + attackerHitBonus >= need;
    }

    private void SetAdditionalDamageAndHit(Player<char> attacker)
    {
        if (attacker.ActiveWeapon != null)
        {
            this.additionalDamage = attacker.ActiveWeapon.AdditionalDamage;
            this.additionalHit = attacker.ActiveWeapon.AdditionalHit;
            this.damage = attacker.ActiveWeapon.Damage;
        }
    }
}