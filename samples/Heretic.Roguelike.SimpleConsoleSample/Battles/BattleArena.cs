using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.SimpleConsoleSample.Battles;

public class BattleArena(IExperienceCalculator<char> experienceCalculator) : IBattleArena<char>
{
    private readonly Random random = new Random();
    private byte additionalDamage;
    private byte additionalHit;
    private IList<DiceThrow> damage = new List<DiceThrow>();

    public IExperienceCalculator<char> ExperienceCalculator { get; init; } = experienceCalculator;

    public void Fight(ICreature<char> attacker, ICreature<char> defender)
    {
        var player = attacker as Player<char>; 
        if (player != null)
        {
            this.SetAdditionalDamageAndHit(player);
        }
        
        int i = 0;
        var isTheMonsterDead = false;

        while (i < attacker.Damage.Count && !isTheMonsterDead)
        {
            var diceThrow = attacker.Damage[i];
            var strengthCorrector = this.additionalHit + this.CalculateStrengthCorrector(attacker.Strength);

            if (IsAttackSuccessful(attacker, defender, strengthCorrector))
            {
                ushort rollResult = diceThrow.Dice.Roll(diceThrow.Tries);

                ushort attackResult = (ushort)(rollResult + this.additionalDamage + this.CalculateDamageCorrector(attacker.Strength));

                defender.HitPoints = (ushort)Math.Max(0, defender.HitPoints - attackResult);
                
                isTheMonsterDead = defender.HitPoints == 0;
            }

            i++;
        }
        
        if (player != null && isTheMonsterDead)
        {
            this.TheMonsterIsDead(player, defender);
        }
    }

    private void TheMonsterIsDead(Player<char> player, ICreature<char> defender)
    {
        this.IncreasePlayerExperience(player, defender);
    }

    private void IncreasePlayerExperience(Player<char> player, ICreature<char> defender)
    {
        player.Experience += this.ExperienceCalculator.GainExperienceFromOpponent(defender);
        player.ExperienceLevel += this.ExperienceCalculator.GetExperienceLevel(player.Experience);
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