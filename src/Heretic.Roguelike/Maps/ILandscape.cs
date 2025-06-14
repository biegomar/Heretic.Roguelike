using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Players;

namespace Heretic.Roguelike.Maps;

public interface ILandscape<T>
{
    int Width { get; }
    int Height { get; }
    int Depth { get; }
    int CurrentFloor { get; set; }
    IEnumerable<ICell<T>> Cells { get; }
    string Title { get; }
    Player<T>? Player { get; set; }
    bool AddContentGenerator(MapTypes mapType, IProceduralContentGenerator<T> contentGenerator);
    void SetActiveContentGenerator(MapTypes mapType);
    void Draw(Vector startVector);
    void DrawCellItems();
    void DrawDashboard(Vector position);
    void DrawCellItemAtPosition(Vector position);
    void DrawSingleCellAtPosition(Vector startVector, Vector position);
    void QueueMessage(string message);
    void PrintMessages();
    void ClearMessage();
    void ClearLandscape();
    bool IsCellVisible(Vector position);
    void SetCellVisibility(Vector position, bool isVisible);
    void SetCellItem(CellItem<T> cellItem);
    CellItem<T>? GetCellItem(Vector position);
    void SetPlayerIntoCell(Player<T>? playerForCell);
    void RemoveCellItem(Vector position);
}