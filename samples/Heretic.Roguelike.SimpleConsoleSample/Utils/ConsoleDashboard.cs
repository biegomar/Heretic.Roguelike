using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things;
using Heretic.Roguelike.Things.Players;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.SimpleConsoleSample.Utils;

public class ConsoleDashboard(IArmourCalculator armourCalculator) : IDashboard<char, Cell<char>>
{
    private int drawColumn;
    
    public void DrawDashboard(IList<Cell<char>> cells, Player<char> player, int currentFloor, Vector startMazeVector)
    {
        this.drawColumn = (int)startMazeVector.X;
        var height = cells.Max(cell => cell.Y) + 1;

        var level = $"Level:{currentFloor}".PadRight(12);
        var hits = $"Hits:{player.HitPoints}({player.MaxHitPoints})".PadRight(12);
        var strength = $"Str:{player.Strength}({player.MaxStrength})".PadRight(12);
        var gold = $"Gold:{player.Gold}".PadRight(12);
        var armourValue = armourCalculator.CalculateArmourFromArmourClass(player.ActiveArmour?.AmorClass ?? player.AmourClass);
        var armour = $"Armor:{armourValue}".PadRight(12);
        var experience = $"Exp:{ExperienceLevels.GetExperienceLevelName(player.ExperienceLevel)} ({player.Experience})".PadRight(12);
        var position = $"(X: {player.ActualPosition.X}, Y: {player.ActualPosition.Y})";


        var oldX = Console.CursorLeft;
        var oldY = Console.CursorTop;
        var screenPositionX = this.drawColumn;
        var screenPositionY = (height + 2) * 2;

        Console.SetCursorPosition((int)screenPositionX, (int)screenPositionY);
        Console.Write($"{level}{hits}{strength}{gold}{armour}{experience}{position}");
        Console.SetCursorPosition(oldX, oldY);
    }
}