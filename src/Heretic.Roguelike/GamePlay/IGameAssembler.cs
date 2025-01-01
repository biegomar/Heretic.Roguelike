using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.GamePlay;

public interface IGameAssembler<T, TK> where TK: class, ICell<T>
{
    public GamePreparation<T, TK> AssembleGame(GameLoop<T, TK> gameLoop);

    public void Restart();
}