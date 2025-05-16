using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Things.Monsters;

namespace Heretic.Roguelike.Things.Interfaces;

public interface IMonsterBreed
{
    string Name { get; }
    
    Monster<T> Spawn<T>(IMotionController<T> motionController, T icon);    
}