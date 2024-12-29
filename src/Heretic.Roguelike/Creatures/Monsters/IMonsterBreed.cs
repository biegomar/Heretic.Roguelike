using Heretic.Roguelike.ArtificialIntelligence.Movements;

namespace Heretic.Roguelike.Creatures.Monsters;

public interface IMonsterBreed
{
    string Name { get; }
    
    Monster<T> Spawn<T>(IMotionController<T> motionController, T icon);    
}