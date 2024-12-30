// See https://aka.ms/new-console-template for more information

using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.SimpleConsoleSample.GamePlay;

var gameLoop = new GameLoop<char>(new GameAssembler());

gameLoop.Run();