using Heretic.Roguelike.Battles;
using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Interfaces;
using Heretic.Roguelike.Things.Monsters;
using Heretic.Roguelike.Things.Players;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.SimpleConsoleSample.GamePlay;

public class GameController : IGameController<char, Cell<char>>
{
    public IGameAssembler<char, Cell<char>> GameAssembler { get; set; }
    public IInputController<char> InputController { get; set; }
    public IOutputHandler OutputHandler { get; set; }
    public IBattleArena<char> BattleArena { get; set; }
    public IExperienceCalculator<char> ExperienceCalculator { get; set; }
    public Landscape<char, Cell<char>> Landscape { get; set; }
    public IMessagePrinter MessagePrinter { get; set; }
    public IList<Monster<char>> Monsters { get; set; }
    public IContentPrinter<char, Cell<char>> ContentPrinter { get; set; }
    public IDashboard<char, Cell<char>> Dashboard { get; set; }
    public Player<char> Player { get; set; }

    public GameController(IGameAssembler<char, Cell<char>> gameAssembler)
    {
        this.GameAssembler = gameAssembler;
    }
    
    public void AssembleGame(GameAssemblePreparation<char, Cell<char>> gameAssemblePreparation)
    {
        var gamePreparation = this.GameAssembler.AssembleGame(gameAssemblePreparation);
        this.InputController = gamePreparation.InputController;
        this.OutputHandler = gamePreparation.OutputHandler;
        this.Player = gamePreparation.Player;
        this.Landscape = gamePreparation.Landscape;
        this.Monsters = gamePreparation.Monsters.ToList();
        this.BattleArena = gamePreparation.BattleArena;
        this.ExperienceCalculator = gamePreparation.ExperienceCalculator;
        this.ContentPrinter = gamePreparation.ContentPrinter;
        this.Dashboard = gamePreparation.Dashboard;
        this.MessagePrinter = gamePreparation.MessagePrinter;
        
        this.BattleArena.OnKillMonster += this.KillMonster;
    }

    public void ProcessInput()
    {
        this.InputController.ProcessInput();
        this.MessagePrinter.PrintMessages();
    }

    public void DrawWelcomeScreen()
    {
        this.MessagePrinter.PrintWelcomeScreen();
    }

    public void DrawLandscape()
    {
        this.Landscape.ClearLandscape();
        this.Landscape.Draw(Vector.Zero);
        this.Landscape.DrawCellItems();
        this.Landscape.DrawDashboard(Vector.Zero);
    }

    public void SetPlayerData()
    {
        this.OutputHandler.OutputLine(string.Empty);
        this.OutputHandler.Output("Rogue's Name? ");
        this.OutputHandler.ResetOutputColor();
        this.Player.Name = this.InputController.GetPlayerInputLine();
        this.MessagePrinter.QueueMessage($"Hello {this.Player.Name}. Welcome to the Dungeons of Doom...");
    }

    private void KillMonster(Monster<char> monster)
    {
        this.IncreasePlayerExperience(this.Player, monster);
        this.Landscape.RemoveCellItem(monster.ActualPosition);
        this.Monsters.Remove(monster);
        this.InputController.UnregisterCreatureFromHandler(monster);
    }
    
    private void IncreasePlayerExperience(Player<char> player, ICreature<char> defender)
    {
        player.Experience += this.ExperienceCalculator.GainExperienceFromOpponent(defender);
        player.ExperienceLevel += this.ExperienceCalculator.GetExperienceLevel(player.Experience);
    }
}