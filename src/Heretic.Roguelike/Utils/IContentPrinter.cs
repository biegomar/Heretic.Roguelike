using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Utils;

public interface IContentPrinter<T, TK> where TK : ICell<T>
{
    public IList<T>? Items { get; set; }
        
    public void DrawCells(IList<TK> cells, Vector startCellVector, string title, bool drawItems = false);

    public void DrawCellItems(IList<TK> cells);

    public void DrawItemAtPosition(IList<TK> cells, Vector position, T item);
}