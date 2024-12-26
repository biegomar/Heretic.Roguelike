using Heretic.Roguelike.Creatures;

namespace Heretic.Roguelike.Battles;

public interface IBattleArena<T>
{
    public void Fight(ICreature<T> attacker, ICreature<T> defender);
}