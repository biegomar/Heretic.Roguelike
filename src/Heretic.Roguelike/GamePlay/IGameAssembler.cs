namespace Heretic.Roguelike.GamePlay;

public interface IGameAssembler<T>
{
    public GamePreparation<T> AssembleGame();

    public void Restart();
}