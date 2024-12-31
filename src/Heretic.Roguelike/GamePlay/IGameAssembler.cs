using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.GamePlay;

public interface IGameAssembler<T, TK> where TK: ICell<T>
{
    public GamePreparation<T, TK> AssembleGame();

    public void Restart();
}