using System.Collections.Generic;
using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;

namespace Heretic.Roguelike.GamePlay;

public interface IGameController<T, TK> where TK : class, ICell<T>
{
    IGameAssembler<T, TK> GameAssembler { get; set; }
    IInputController InputController { get; set; }
    
    Landscape<T, TK> Landscape { get; set; }
    IEnumerable<Monster<T>> Monsters { get; set; }  
    Player<T> Player { get; set; }

    void AssembleGame(GameLoop<T, TK> gameLoop);
    void ProcessInput();
}