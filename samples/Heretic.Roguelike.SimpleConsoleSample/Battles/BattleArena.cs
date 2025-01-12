using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.SimpleConsoleSample.Battles;

public class BattleArena(IExperienceCalculator<char> experienceCalculator) : IBattleArena<char>
{
    private readonly Dictionary<int, string> monsterHitsYouMessage = new Dictionary<int, string>
    {
        { 0, "scored an excellent hit on" },
        { 1, "hit" },
        { 2, "has injured" },
        { 3, "swings and hits" }
    };
    
    private readonly Dictionary<int, string> youHitMonsterMessage = new Dictionary<int, string>
    {
        { 0, "scored an excellent hit on" },
        { 1, "hit" },
        { 2, "have injured" },
        { 3, "swing and hit" }
    };
    
    private readonly Dictionary<int, string> monsterMissesYouMessage = new Dictionary<int, string>
    {
        { 0, "doesn't hit" },
        { 1, "misses" },
        { 2, "barely misses" },
        { 3, "swings and misses" }
    };
    
    private readonly Dictionary<int, string> youMissMonsterMessage = new Dictionary<int, string>
    {
        { 0, "don't hit" },
        { 1, "miss" },
        { 2, "barely miss" },
        { 3, "swing and miss" }
    };
    
    private readonly Random random = new Random();
    private byte additionalDamage;
    private byte additionalHit;
    private int armourValue;
    private IList<DiceThrow> damage = new List<DiceThrow>();

    public IExperienceCalculator<char> ExperienceCalculator { get; init; } = experienceCalculator;

    public void Fight(ICreature<char> attacker, ICreature<char> defender)
    {
        ArgumentNullException.ThrowIfNull(attacker);
        ArgumentNullException.ThrowIfNull(defender);

        this.SetAdditionalDamageAmourAndHit(attacker, defender);
        
        int i = 0;
        var isTheOpponentDead = defender.HitPoints == 0;

        while (i < this.damage.Count && !isTheOpponentDead)
        {
            var diceThrow = damage[i];
            uint strengthCorrector = this.additionalHit + this.CalculateStrengthCorrector(attacker.Strength);

            if (IsAttackSuccessful(attacker, strengthCorrector))
            {
                HitMessage(attacker, defender);
                var rollResult = diceThrow.Dice.Roll(diceThrow.Tries);

                var attackResult = Math.Max(0,
                    rollResult + this.additionalDamage + this.CalculateDamageCorrector(attacker.Strength));

                defender.HitPoints = (ushort)Math.Max(0, defender.HitPoints - attackResult);
                
                isTheOpponentDead = defender.HitPoints == 0;
            }
            else
            {
                MissMessage(attacker, defender);
            }

            i++;
        }
        
        if (attacker is Player<char> player && isTheOpponentDead)
        {
            this.TheMonsterIsDead(player, defender);
        }
    }
    
    public void Fight(IList<ICreature<char>> attackerGroup, IList<ICreature<char>> defenderGroup)
    {
        //TODO
        throw new NotImplementedException();
    }

    public Action<string>? MessageHandler { get; set; }

    private void HitMessage(ICreature<char> attacker, ICreature<char> defender)
    {
        var index = random.Next(0, 4);
        if (attacker is Player<char>)
        {
            var monster = defender as Monster<char>;
            MessageHandler?.Invoke($"You {this.youHitMonsterMessage[index]} {monster?.Breed}.");
        }
        else
        {
            var monster = attacker as Monster<char>;
            MessageHandler?.Invoke($"The {monster?.Breed} {this.monsterHitsYouMessage[index]} you.");
        }
    }
    
    private void MissMessage(ICreature<char> attacker, ICreature<char> defender)
    {
        var index = random.Next(0, 4);
        if (attacker is Player<char>)
        {
            var monster = defender as Monster<char>;
            MessageHandler?.Invoke($"You {this.youMissMonsterMessage[index]} {monster?.Breed}.");
        }
        else
        {
            var monster = attacker as Monster<char>;
            MessageHandler?.Invoke($"The {monster?.Breed} {this.monsterMissesYouMessage[index]} you.");
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

    private uint CalculateStrengthCorrector(uint strength)
    {
        uint add = 4;

        // original implementation - strange...
        //if (strength < 8) return strength - 7;
        
        if (strength < 31) add--;
        if (strength < 21) add--;
        if (strength < 19) add--;
        if (strength < 17) add--;
        
        return add;
    }
    
    private uint CalculateDamageCorrector(uint strength)
    {
        uint add = 6;

        // original implementation - strange...
        //if (strength < 8) return strength - 7;
        
        if (strength < 31) add--;
        if (strength < 22) add--;
        if (strength < 20) add--;
        if (strength < 18) add--;
        if (strength < 17) add--;
        if (strength < 16) add--;
        
        return add;
    }
    
    private bool IsAttackSuccessful(ICreature<char> attacker, uint attackerHitBonus)
    {
        var res = this.random.Next(1,21);
        var need = 20 - attacker.ExperienceLevel - this.armourValue;

        return res + attackerHitBonus >= need;
    }

    private void SetAdditionalDamageAmourAndHit(ICreature<char> attacker, ICreature<char> defender)
    {
        this.damage = attacker.Damage;
        this.additionalDamage = 0;
        this.additionalHit = 0;

        if (attacker is Player<char> { ActiveWeapon: not null } player)
        {
            this.additionalDamage = player.ActiveWeapon.AdditionalDamage;
            this.additionalHit = player.ActiveWeapon.AdditionalHit;
            this.damage = player.ActiveWeapon.Damage;
        }

        this.armourValue = defender.AmourClass;
        if (defender is Player<char> { ActiveArmour: not null } armouredPlayer)
        {
            this.armourValue = armouredPlayer.ActiveArmour.AmorClass; 
        }
        
        // TODO - if defender is not running
        this.additionalHit += 4;
    }
}