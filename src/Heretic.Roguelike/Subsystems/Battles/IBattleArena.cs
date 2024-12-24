using Heretic.Roguelike.Subsystems.Creatures;

namespace Heretic.Roguelike.Subsystems.Battles;

public interface IBattleArena<T>
{
    public void Fight(ICreature<T> attacker, ICreature<T> defender);
}