using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Amours.Types;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Creatures.Monsters.Breeds;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Maps.ContentGeneration.Mazes;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.SimpleConsoleSample.ArtificialIntelligence.Movements;
using Heretic.Roguelike.SimpleConsoleSample.Battles;
using Heretic.Roguelike.SimpleConsoleSample.Creatures;
using Heretic.Roguelike.SimpleConsoleSample.Utils;
using Heretic.Roguelike.Utils;
using Heretic.Roguelike.Weapons;
using Heretic.Roguelike.Weapons.Types;

namespace Heretic.Roguelike.SimpleConsoleSample.GamePlay;

public class GameAssembler : IGameAssembler<char, Cell<char>>
{
    private readonly Vector landscapeDimensions = new (10, 10, 0);
    private readonly Vector startingPosition = new (8, 8, 0);
    
    public GamePreparation<char, Cell<char>> AssembleGame(GameLoop<char, Cell<char>> gameLoop)
    {
        var experienceCalculator = CreateExperienceCalculator();
        var armourCalculator = CreateArmourCalculator();
        var contentPrinter = CreateConsoleMazePrinter(armourCalculator);
        var landscape = CreateLandscape(contentPrinter);
        
        var playerInputHandler = CreatePlayerInputHandler();
        var monsterInputHandler = CreateMonsterInputHandler();
        var inputController = CreateInputController();
        
        var battleArena = CreateBattleArena(landscape);
        
        var player = CreatePlayer(landscape, battleArena);
        SetupPlayerEventHandling(player, inputController, playerInputHandler);
        SetupGameEventHandling(playerInputHandler, monsterInputHandler, gameLoop);
        
        var monsters = CreateMonsters(landscape, battleArena, armourCalculator);
        SetupMonsterEventHandling(monsters, inputController, monsterInputHandler);

        var result = new GamePreparation<char, Cell<char>>(player, landscape, battleArena, inputController, experienceCalculator,  monsters);
        
        return result;
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

    private static ConsoleMazePrinter CreateConsoleMazePrinter(IArmourCalculator armourCalculator)
    {
        var contentPrinter = new ConsoleMazePrinter(armourCalculator);
        return contentPrinter;
    }

    private Landscape<char, Cell<char>> CreateLandscape(IContentPrinter<char, Cell<char>> contentPrinter)
    {
        var mazeGenerator = new AldousBroderMazeGenerator<char, Cell<char>>();
        
        var landscape = new Landscape<char, Cell<char>>(landscapeDimensions, mazeGenerator, contentPrinter, "AldousBroder");
        
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
        var armourCalculator = CreatePassThruArmourCalculator();
        
        Random random = new ();
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
        
        var result = new Player<char>(playerMovement)
        {
            Name = "atogeib",
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
        landscape.DrawDashboard();
        
        return result;
    }

    private void SetupPlayerEventHandling(Player<char> player, IInputController<char> inputController, IInputHandler inputHandler)
    {
        inputController.RegisterHandler(inputHandler, player);
    }
    
    private IList<Monster<char>> CreateMonsters(Landscape<char, Cell<char>> landscape, IBattleArena<char> battleArena, IArmourCalculator armourCalculator)
    {
        var monsterFactory = new MonsterFactory<char>(new MotionControllerFactory(landscape, battleArena), armourCalculator, CreateIconsFromBreeds());
        var monsters = new List<Monster<char>>();
        
        var kestrel = monsterFactory.CreateMonster(nameof(Kestrel), new Vector(1, 1, 0));
        var bat = monsterFactory.CreateMonster(nameof(Bat), new Vector(1, 9, 0));
        
        monsters.Add(kestrel);
        monsters.Add(bat);
        
        landscape.SetCellItem(new CellItem<char>(kestrel, new Vector(1, 1, 0)));
        landscape.SetCellItem(new CellItem<char>(bat, new Vector(1, 9, 0)));
        
        return monsters;
    }
    
    private void SetupMonsterEventHandling(IEnumerable<Monster<char>> monsters, IInputController<char> inputController, IInputHandler inputHandler)
    {
        foreach (var monster in monsters)
        {
            inputController.RegisterHandler(inputHandler, monster);
        }
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