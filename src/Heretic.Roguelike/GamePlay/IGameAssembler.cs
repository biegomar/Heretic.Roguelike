using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Maps.ContentGeneration;

namespace Heretic.Roguelike.GamePlay;

public interface IGameAssembler<T>
{
    public GamePreparationOutputStructure<T> AssembleGame(GamePreparationInputStructure<T> inputStructure);

    public void Restart();
}