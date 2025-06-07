using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Amours.Types;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Daemons;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Maps.ContentGeneration.Mazes;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.PickHandling;
using Heretic.Roguelike.SimpleConsoleSample.ArtificialIntelligence.Movements;
using Heretic.Roguelike.SimpleConsoleSample.Battles;
using Heretic.Roguelike.SimpleConsoleSample.Creatures;
using Heretic.Roguelike.SimpleConsoleSample.Utils;
using Heretic.Roguelike.Things.Common;
using Heretic.Roguelike.Things.Interfaces;
using Heretic.Roguelike.Things.Monsters;
using Heretic.Roguelike.Things.Monsters.Breeds;
using Heretic.Roguelike.Things.Players;
using Heretic.Roguelike.Utils;
using Heretic.Roguelike.Weapons;
using Heretic.Roguelike.Weapons.Types;

namespace Heretic.Roguelike.SimpleConsoleSample.GamePlay;

public class GameAssembler : IGameAssembler<char, Cell<char>>
{
    private readonly Vector landscapeDimensions = new (20, 10, 0);
    private readonly Random random = new();
    private readonly Vector startingPosition;

    public GameAssembler()
    {
        startingPosition = GenerateRandomPositionVector();
    }
    
    public GameAssembleResult<char, Cell<char>> AssembleGame(GameAssemblePreparation<char, Cell<char>> gameAssemblePreparation)
    {
        var experienceCalculator = CreateExperienceCalculator();
        var armourCalculator = CreateArmourCalculator();
        var contentPrinter = CreateConsoleMazePrinter(landscapeDimensions);
        var dashboard = CreateConsoleDashboard(armourCalculator);
        
        var landscape = CreateLandscape(contentPrinter, dashboard);
        var daemonHandler = CreateDaemonHandler();
        
        var playerInputHandler = CreatePlayerInputHandler();
        var outputHandler = CreateOutputHandler();
        var monsterInputHandler = CreateMonsterInputHandler();
        var inputController = CreateInputController();
        
        var battleArena = CreateBattleArena(landscape);
        
        var player = CreatePlayer(landscape, battleArena);
        SetupPlayerEventHandling(player, inputController, playerInputHandler);
        SetupGameEventHandling(playerInputHandler, monsterInputHandler, gameAssemblePreparation.GameLoop);
        
        var monsters = CreateMonsters(landscape, battleArena, armourCalculator);
        SetupMonsterEventHandling(monsters, inputController, monsterInputHandler);
        
        CreateAndSetExit(landscape);
        
        CreateGold(landscape);

        SetVisibilityOfStartingPositionSurrounding(landscape);

        var result = new GameAssembleResult<char, Cell<char>>(
            player, 
            landscape, 
            daemonHandler, 
            battleArena, 
            inputController, 
            outputHandler, 
            experienceCalculator, 
            contentPrinter, 
            dashboard,
            monsters);
        
        return result;
    }

    private void SetVisibilityOfStartingPositionSurrounding(Landscape<char, Cell<char>> landscape)
    {
        landscape.SetCellVisibility(new Vector(startingPosition.X, startingPosition.Y, 0), true);
        landscape.SetCellVisibility(new Vector(startingPosition.X - 1, startingPosition.Y, 0), true);
        landscape.SetCellVisibility(new Vector(startingPosition.X + 1, startingPosition.Y, 0), true);
        landscape.SetCellVisibility(new Vector(startingPosition.X, startingPosition.Y - 1, 0), true);
        landscape.SetCellVisibility(new Vector(startingPosition.X, startingPosition.Y + 1, 0), true);
    }

    private static IOutputHandler CreateOutputHandler()
    {
        return new ConsoleOutputHandler();        
    }
    
    private static ExperienceCalculator CreateExperienceCalculator()
    {
        var experienceCalculator = new ExperienceCalculator();
        return experienceCalculator;
    }

    private static AdvancedDungeonsDragonsArmourCalculator CreateArmourCalculator()
    {
        var armourCalculator = new AdvancedDungeonsDragonsArmourCalculator();
        return armourCalculator;
    }
    
    private static PassThruArmourCalculator CreatePassThruArmourCalculator()
    {
        var armourCalculator = new PassThruArmourCalculator();
        return armourCalculator;
    }

    private static DaemonHandler CreateDaemonHandler()
    {
        var daemonHandler = new DaemonHandler();
        return daemonHandler;
    }

    public void Restart()
    {
        // TODO
        throw new NotImplementedException();
    }

    private IBattleArena<char> CreateBattleArena(Landscape<char, Cell<char>> landscape)
    {
        var battleArena = new BattleArena()
        {
            MessageHandler = landscape.DrawMessage
        };

        return battleArena;
    }

    private void SetupGameEventHandling(IInputHandler inputHandler, CommonMonsterInputHandler commonMonsterInputHandler,GameLoop<char, Cell<char>> gameLoop)
    {
        inputHandler.OnQuitGame += () => gameLoop.IsGameFinished = true;
        inputHandler.OnQuitGame += () => commonMonsterInputHandler.IsQuitGame = true;
    }

    private static ConsoleMazePrinter CreateConsoleMazePrinter(Vector landscapeDimensions)
    {
        var contentPrinter = new ConsoleMazePrinter(landscapeDimensions);
        return contentPrinter;
    }

    private static ConsoleDashboard CreateConsoleDashboard(IArmourCalculator armourCalculator)
    {
        var dashboard = new ConsoleDashboard(armourCalculator);
        return dashboard;
    }

    private Landscape<char, Cell<char>> CreateLandscape(IContentPrinter<char, Cell<char>> contentPrinter, IDashboard<char, Cell<char>> dashboard)
    {
        var mazeGenerator = new AldousBroderMazeGenerator<char, Cell<char>>();
        
        var landscape = new Landscape<char, Cell<char>>(landscapeDimensions, mazeGenerator, contentPrinter, dashboard, "AldousBroder");
        
        return landscape;
    }

    private IInputController<char> CreateInputController()
    {
        var inputController = new KeyboardInputController();
        
        return inputController;
    }
    
    private IInputHandler CreatePlayerInputHandler()
    {
        var inputHandler = new KeyboardInputHandler();
        
        return inputHandler;
    }
    
    private CommonMonsterInputHandler CreateMonsterInputHandler()
    {
        var inputHandler = new CommonMonsterInputHandler();
        
        return inputHandler;
    }
    
    private Player<char> CreatePlayer(Landscape<char, Cell<char>> landscape, IBattleArena<char> battleArena)
    {
        var playerMovement = new PlayerMovement(landscape, battleArena, startingPosition);
        var playerPickController = CreatePlayerPickController(landscape);
        
        var armourCalculator = CreatePassThruArmourCalculator();
        
        WeaponFactory weaponFactory = new();
        ArmourFactory armorFactory = new(armourCalculator);
        
        DiceThrow diceThrow = new(1, new Dice(DiceType.D4));
        var mace = weaponFactory.CreateWeapon(nameof(Mace));
        mace.AdditionalHit = 1;
        mace.AdditionalDamage = 1;
        mace.Flags |= WeaponFlag.IsKnown;

        var bow = weaponFactory.CreateWeapon(nameof(Bow));
        bow.AdditionalHit = 1;
        bow.Flags |= WeaponFlag.IsKnown;

        var arrows = weaponFactory.CreateWeapon(nameof(Arrow));
        arrows.Count = (byte)(random.Next(16) + 25);
        arrows.Flags |= WeaponFlag.IsKnown;
        
        var armor = armorFactory.CreateArmour(nameof(RingMail));
        armor.AmorClass -= 1;
        
        ushort strength = 16;
        
        var result = new Player<char>(playerMovement, playerPickController)
        {
            Name = string.Empty,
            IsVisible = true,
            Strength = strength,
            MaxStrength = strength,
            Experience = 0,
            ExperienceLevel = 1,
            AmourClass = 10,
            HitPoints = 12,
            MaxHitPoints = 12,
            ActiveWeapon = mace,
            Weapons = new List<Weapon>() {mace, bow, arrows},
            ActiveArmour = armor,
            Icon = '@',
            Armours = new List<Armour>() {armor},
            Damage = new List<DiceThrow>() { diceThrow}
        };
        
        landscape.Player = result;
        
        return result;
    }

    private Vector GenerateRandomPositionVector()
    {
        return new Vector(random.Next(0, (int)landscapeDimensions.X), random.Next(0, (int)landscapeDimensions.Y), 0);
    }

    private PickController<char> CreatePlayerPickController(Landscape<char, Cell<char>> landscape)
    {
        var pickController = new PickController<char>();
        var goldPickHandler = new GoldPickHandler<char>()
        {
            MessageHandler = landscape.DrawMessage
        };
        
        pickController.RegisterHandler<Gold<char>>(goldPickHandler);
        
        return pickController;
    }

    private void SetupPlayerEventHandling(Player<char> player, IInputController<char> inputController, IInputHandler inputHandler)
    {
        inputController.RegisterHandler(inputHandler, player);
    }
    
    private IList<Monster<char>> CreateMonsters(Landscape<char, Cell<char>> landscape, IBattleArena<char> battleArena, IArmourCalculator armourCalculator)
    {
        var monsterFactory = new MonsterFactory<char>(new MotionControllerFactory(landscape, battleArena), armourCalculator, CreateIconsFromBreeds());
        var monsters = new List<Monster<char>>();

        var kestrel = CreateMonsterOfBreed(landscape, monsterFactory, nameof(Kestrel));
        monsters.Add(kestrel);
        
        var bat = CreateMonsterOfBreed(landscape, monsterFactory, nameof(Bat));
        monsters.Add(bat);
        
        return monsters;
    }

    private Monster<char> CreateMonsterOfBreed(Landscape<char, Cell<char>> landscape, MonsterFactory<char> monsterFactory, string breed)
    {
        var kestrelPosition = GetRandomFreeCell(landscape);
        var kestrel = monsterFactory.CreateMonster(breed, kestrelPosition);
        landscape.SetCellItem(new CellItem<char>(kestrel, kestrelPosition));
        return kestrel;
    }

    private Vector GetRandomFreeCell(Landscape<char, Cell<char>> landscape)
    {
        Vector position;
        var isFreeCell = false;
        
        do
        {
            position = GenerateRandomPositionVector();
            var cellItem = landscape.GetCellItem(position);
            isFreeCell = cellItem == null;
        } while (!isFreeCell);

        return position;
    }
    
    private void SetupMonsterEventHandling(IEnumerable<Monster<char>> monsters, IInputController<char> inputController, IInputHandler inputHandler)
    {
        foreach (var monster in monsters)
        {
            inputController.RegisterHandler(inputHandler, monster);
        }
    }

    private void CreateAndSetExit(Landscape<char, Cell<char>> landscape)
    {
        var position = GetRandomFreeCell(landscape);
        var exit = new Exit<char>(new SteadyState<char>(position))
            {
                Icon = '['
            };
        
        landscape.SetCellItem(new CellItem<char>(exit, position));
    }

    private void CreateGold(Landscape<char, Cell<char>> landscape)
    {
        var position = GetRandomFreeCell(landscape);
        var gold = new Gold<char>(new SteadyState<char>(position))
        {
            Icon = '*',
            ActualValue = 100
        };
        
        landscape.SetCellItem(new CellItem<char>(gold, position));
    }
    
    private IDictionary<string, char> CreateIconsFromBreeds()
    {
        return new Dictionary<string, char>
        {
            { nameof(Zombie), 'Z' },
            { nameof(Yeti), 'Y' },
            { nameof(Xeroc), 'X' },
            { nameof(Wraith), 'W' },
            { nameof(VenusFlytrap), 'V' },
            { nameof(Vampire), 'V' },
            { nameof(Urvile), 'U' },
            { nameof(Troll), 'T' },
            { nameof(Snake), 'S' },
            { nameof(Rattlesnake), 'R' },
            { nameof(Quagga), 'Q' },
            { nameof(Phantom), 'P' },
            { nameof(Orc), 'O' },
            { nameof(Nymph), 'N' },
            { nameof(Medusa), 'M' },
            { nameof(Leprechaun), 'L' },
            { nameof(Kestrel), 'K' },
            { nameof(Jabberwock), 'J' },
            { nameof(IceMonster), 'I' },
            { nameof(Hobgoblin), 'H' },
            { nameof(Griffin), 'G' },
            { nameof(Emu), 'E' },
            { nameof(Dragon), 'D' },
            { nameof(Centaur), 'C' },
            { nameof(Bat), 'B' },
            { nameof(Aquator), 'A' }
        };
    }
}