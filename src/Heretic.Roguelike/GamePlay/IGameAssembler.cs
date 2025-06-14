using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.GamePlay;

public interface IGameAssembler<T>
{
    public GameAssembleResult<T> AssembleGame(GameAssemblePreparation<T> gameAssemblePreparation);

    public void Restart();
}