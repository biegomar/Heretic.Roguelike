using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Maps.ContentGeneration.Mazes;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.SimpleConsoleSample.ArtificialIntelligence.Movements;
using Heretic.Roguelike.SimpleConsoleSample.Battles;
using Heretic.Roguelike.SimpleConsoleSample.Creatures.Players;
using Heretic.Roguelike.SimpleConsoleSample.Utils;

namespace Heretic.Roguelike.SimpleConsoleSample.GamePlay;

public class GameAssembler : IGameAssembler<char, Cell<char>>
{
    private readonly Vector landscapeDimensions = new (80, 25, 0);
    
    public GamePreparation<char, Cell<char>> AssembleGame()
    {
        var player = CreatePlayer();
        var landscape = CreateLandscape();
        var inputController = CreateInputController();
        var battleArena = CreateBattleArena();

        var result = new GamePreparation<char, Cell<char>>(player, landscape, battleArena, inputController);
        
        return result;
    }

    public void Restart()
    {
        // TODO
        throw new NotImplementedException();
    }

    private IBattleArena<char> CreateBattleArena()
    {
        var battleArena = new BattleArena();
        
        return battleArena;
    }

    private Landscape<char, Cell<char>> CreateLandscape()
    {
        var contentPrinter = new ConsoleMazePrinter();
        var mazeGenerator = new AldousBroderMazeGenerator<char, Cell<char>>();
        
        var landscape = new Landscape<char, Cell<char>>(landscapeDimensions, mazeGenerator, contentPrinter, "AldousBroder");
        
        return landscape;
    }

    private IMotionControllerFactory<char> CreateMotionControllerFactory(Landscape<char, Cell<char>> landscape)
    {
        var controllerFactory = new MotionControllerFactory(landscape);
        
        return controllerFactory;
    }

    private IInputController CreateInputController()
    {
        var inputController = new KeyboardInputController();
        var inputHandler = new KeyboardInputHandler();
        
        inputController.RegisterHandler(inputHandler);
        
        return inputController;
    }
    
    private Player<char> CreatePlayer()
    {
        var playerMovement = new PlayerMovement(Vector.Zero, '@');
        var experienceCalculator = new ExperienceCalculator();
        
        var result = new Player<char>(playerMovement, experienceCalculator);
        return result;
    }
}