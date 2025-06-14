using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Utils;

public interface IContentPrinter<T>
{
    IList<T>? Items { get; set; }
        
    void DrawCells(IEnumerable<ICell<T>> cells, Vector startCellVector, string title, bool drawItems = false);

    void DrawCellItems(IEnumerable<ICell<T>> cells);
    
    void DrawSingleCellAtPosition(IEnumerable<ICell<T>> cells, Vector startMazeVector, Vector position);

    void DrawCellItemAtPosition(IEnumerable<ICell<T>> cells, Vector position);
    
    void ClearScreen();
}