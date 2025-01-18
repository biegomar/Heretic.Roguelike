using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.SimpleConsoleSample.GamePlay;

public class GameController : IGameController<char, Cell<char>>
{
    public IGameAssembler<char, Cell<char>> GameAssembler { get; set; }
    public IInputController InputController { get; set; }
    public Landscape<char, Cell<char>> Landscape { get; set; }
    public IEnumerable<Monster<char>> Monsters { get; set; }
    public Player<char> Player { get; set; }

    public GameController(IGameAssembler<char, Cell<char>> gameAssembler)
    {
        this.GameAssembler = gameAssembler;
    }
    
    public void AssembleGame(GameLoop<char, Cell<char>> gameLoop)
    {
        var gamePreparation = this.GameAssembler.AssembleGame(gameLoop);
        this.InputController = gamePreparation.InputController;
        this.Player = gamePreparation.Player;
        this.Landscape = gamePreparation.Landscape;
        this.Monsters = gamePreparation.Monsters;
        
        this.Landscape.Draw(Vector.Zero);
        this.Landscape.DrawCellItems();
    }

    public void ProcessInput()
    {
        this.InputController.ProcessInput();
    }
}