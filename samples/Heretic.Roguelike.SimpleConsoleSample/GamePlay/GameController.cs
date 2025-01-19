using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.SimpleConsoleSample.GamePlay;

public class GameController : IGameController<char, Cell<char>>
{
    public IGameAssembler<char, Cell<char>> GameAssembler { get; set; }
    public IInputController<char> InputController { get; set; }
    public IBattleArena<char> BattleArena { get; set; }
    public IExperienceCalculator<char> ExperienceCalculator { get; set; }
    public Landscape<char, Cell<char>> Landscape { get; set; }
    public IList<Monster<char>> Monsters { get; set; }
    public Player<char> Player { get; set; }

    public GameController(IGameAssembler<char, Cell<char>> gameAssembler)
    {
        this.GameAssembler = gameAssembler;
    }
    
    public void AssembleGame(GameLoop<char, Cell<char>> gameLoop)
    {
        var gamePreparation = this.GameAssembler.AssembleGame(gameLoop);
        this.InputController = gamePreparation.InputController;
        this.Player = gamePreparation.Player;
        this.Landscape = gamePreparation.Landscape;
        this.Monsters = gamePreparation.Monsters.ToList();
        this.BattleArena = gamePreparation.BattleArena;
        this.ExperienceCalculator = gamePreparation.ExperienceCalculator;
        
        this.Landscape.Draw(Vector.Zero);
        this.Landscape.DrawCellItems();
        
        this.BattleArena.OnKillMonster += this.KillMonster;
    }

    public void ProcessInput()
    {
        this.InputController.ProcessInput();
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