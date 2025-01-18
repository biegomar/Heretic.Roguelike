// See https://aka.ms/new-console-template for more information

using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.SimpleConsoleSample.GamePlay;

var gameLoop = new GameLoop<char, Cell<char>>(new GameController(new GameAssembler()));

gameLoop.Run();