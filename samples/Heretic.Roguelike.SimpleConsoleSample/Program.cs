// See https://aka.ms/new-console-template for more information

using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.SimpleConsoleSample.GamePlay;

const int MinWidth = 80;
const int MinHeight = 25;

int currentWidth = Console.WindowWidth;
int currentHeight = Console.WindowHeight;

if (currentWidth < MinWidth || currentHeight < MinHeight)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Fehler: Die Konsole ist zu klein.");
    Console.WriteLine($"Erforderlich: mindestens {MinWidth}x{MinHeight} Zeichen.");
    Console.WriteLine($"Aktuell: {currentWidth}x{currentHeight} Zeichen.");
    Console.ResetColor();
    Environment.Exit(1); // Beendet das Programm mit Fehlercode 1
}

var gameLoop = new GameLoop<char, Cell<char>>(new GameController(new GameAssembler()));

gameLoop.Run();