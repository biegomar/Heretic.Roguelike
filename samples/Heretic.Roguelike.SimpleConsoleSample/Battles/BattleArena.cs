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
    private sbyte armourClass;
    private IList<DiceThrow> damage = new List<DiceThrow>();

    public IExperienceCalculator<char> ExperienceCalculator { get; init; } = experienceCalculator;

    public void Fight(ICreature<char> attacker, ICreature<char> defender)
    {
        ArgumentNullException.ThrowIfNull(attacker);
        ArgumentNullException.ThrowIfNull(defender);

        this.SetAdditionalDamageAmourAndHit(attacker);
        
        int i = 0;
        var isTheOpponentDead = false;

        while (i < this.damage.Count && !isTheOpponentDead)
        {
            var diceThrow = damage[i];
            var strengthCorrector = this.additionalHit + this.CalculateStrengthCorrector(attacker.Strength);

            if (IsAttackSuccessful(attacker, strengthCorrector))
            {
                var rollResult = diceThrow.Dice.Roll(diceThrow.Tries);

                var attackResult = Math.Max(0,
                    rollResult + this.additionalDamage + this.CalculateDamageCorrector(attacker.Strength));

                defender.HitPoints = (ushort)Math.Max(0, defender.HitPoints - attackResult);
                
                isTheOpponentDead = defender.HitPoints == 0;
            }

            i++;
        }
        
        if (attacker is Player<char> player && isTheOpponentDead)
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
    
    private int CalculateStrengthCorrector(int strength)
    {
        var add = 4;

        if (strength < 8) return strength - 7;
        
        if (strength < 31) add--;
        if (strength < 21) add--;
        if (strength < 19) add--;
        if (strength < 17) add--;
        
        return add;
    }
    
    private int CalculateDamageCorrector(int strength)
    {
        var add = 6;

        if (strength < 8) return strength - 7;
        
        if (strength < 31) add--;
        if (strength < 22) add--;
        if (strength < 20) add--;
        if (strength < 18) add--;
        if (strength < 17) add--;
        if (strength < 16) add--;
        
        return add;
    }
    
    private bool IsAttackSuccessful(ICreature<char> attacker, int attackerHitBonus)
    {
        var res = this.random.Next(1,21);
        var need = 20 - attacker.ExperienceLevel - this.armourClass;

        return res + attackerHitBonus >= need;
    }

    private void SetAdditionalDamageAmourAndHit(ICreature<char> attacker)
    {
        this.damage = attacker.Damage;
        this.additionalDamage = 0;
        this.additionalHit = 0;
        this.armourClass = attacker.AmorClass;

        if (attacker is Player<char> { ActiveWeapon: not null } player)
        {
            this.additionalDamage = player.ActiveWeapon.AdditionalDamage;
            this.additionalHit = player.ActiveWeapon.AdditionalHit;
            this.damage = player.ActiveWeapon.Damage;
            if (player.ActiveArmor != null)
            {
                this.armourClass = player.ActiveArmor.AmorClass;
            }
        }
        
        // TODO - if defender is not running
        this.additionalHit += 4;
    }
}