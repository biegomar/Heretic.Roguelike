using System;
using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Players;

namespace Heretic.Roguelike.Utils;

public interface IContentPrinter<T, TK> where TK : ICell<T>
{
    IList<T>? Items { get; set; }
        
    void DrawCells(IList<TK> cells, Vector startCellVector, string title, bool drawItems = false);

    void DrawCellItems(IList<TK> cells);
    
    void DrawSingleCellAtPosition(IList<TK> cells, Vector startMazeVector, Vector position);

    void DrawCellItemAtPosition(IList<TK> cells, Vector position);
    
    void DrawDashboard(IList<TK> cells, Player<T> creature, int currentFloor);
    
    void DrawMessage(IList<TK> cells, string message);
    
    void DrawStartScreen();
    
    void DrawGameOverScreen();
    
    void DrawGameWonScreen();
    
    void DrawCreditsScreen();
    
    void DrawWelcomeScreen();
    
    void ClearMessage(IList<TK> cells);
    
    void ClearScreen();
}