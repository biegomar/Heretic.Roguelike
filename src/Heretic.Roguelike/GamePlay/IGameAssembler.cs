using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.GamePlay;

public interface IGameAssembler<T, TK> where TK: class, ICell<T>
{
    public GameAssembleResult<T, TK> AssembleGame(GameAssemblePreparation<T, TK> gameAssemblePreparation);

    public void Restart();
}