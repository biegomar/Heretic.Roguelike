using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Utils;

public interface IContentPrinter<T, TK> where TK : ICell<T>
{
    IList<T>? Items { get; set; }
        
    void DrawCells(IList<TK> cells, Vector startCellVector, string title, bool drawItems = false);

    void DrawCellItems(IList<TK> cells);
    
    void DrawSingleCellAtPosition(IList<TK> cells, Vector startMazeVector, Vector position);

    void DrawCellItemAtPosition(IList<TK> cells, Vector position);
    
    void ClearScreen();
}