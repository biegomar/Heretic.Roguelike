using System.Collections.Generic;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Utils;

public interface IContentPrinter<T, TK> where TK : ICell<T>
{
    IList<T>? Items { get; set; }
        
    void DrawCells(IList<TK> cells, Vector startCellVector, string title, bool drawItems = false);

    void DrawCellItems(IList<TK> cells);

    void DrawItemAtPosition(IList<TK> cells, Vector position, T item);
    
    void DrawDashboard(IList<TK> cells, Player<T> creature, ushort currentFloor);
}