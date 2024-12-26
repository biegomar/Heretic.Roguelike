using System.Collections.Generic;
using Heretic.Roguelike.Maps;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Utils;

public interface IContentPrinter<T>
{
    public IList<T>? Items { get; set; }
        
    public void DrawCells(IList<Cell<T>> cells, Vector startCellVector, string title, bool drawItems = false);

    public void DrawCellItems(IList<Cell<T>> cells);

    public void DrawItemAtPosition(IList<Cell<T>> cells, Vector position, T item);
}