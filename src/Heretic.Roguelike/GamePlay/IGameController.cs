using System;
using System.Collections.Generic;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Things.Monsters;
using Heretic.Roguelike.Things.Players;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.GamePlay;

public interface IGameController<T, TK> where TK : class, ICell<T>
{
    IGameAssembler<T, TK> GameAssembler { get; set; }
    IInputController<T> InputController { get; set; }
    
    IOutputHandler OutputHandler { get; set; }
    
    IBattleArena<T> BattleArena { get; set; }
    Landscape<T, TK> Landscape { get; set; }
    IContentPrinter<T, TK> ContentPrinter { get; set; }
    IList<Monster<T>> Monsters { get; set; }  
    Player<T> Player { get; set; }
    
    void AssembleGame(GameAssemblePreparation<T, TK> gameAssemblePreparation);
    void ProcessInput();

    void DrawWelcomeScreen();
    
    void DrawLandscape();
    
    void SetPlayerData();
}