using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Amours.Types;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
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
using Heretic.Roguelike.SimpleConsoleSample.Creatures.Players;
using Heretic.Roguelike.SimpleConsoleSample.Utils;
using Heretic.Roguelike.Weapons;
using Heretic.Roguelike.Weapons.Types;

namespace Heretic.Roguelike.SimpleConsoleSample.GamePlay;

public class GameAssembler : IGameAssembler<char, Cell<char>>
{
    private readonly Vector landscapeDimensions = new (10, 10, 0);
    
    public GamePreparation<char, Cell<char>> AssembleGame()
    {
        var landscape = CreateLandscape();
        
        var player = CreatePlayer(landscape);
        var inputHandler = CreateInputHandler();
        var inputController = CreateInputController(inputHandler);
        SetupPlayerEventHandling(player, inputHandler, inputController);
        
        var monsters = CreateMonsters(landscape);
        
        var battleArena = CreateBattleArena();

        var result = new GamePreparation<char, Cell<char>>(player, landscape, battleArena, inputController, monsters);
        
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

    private IInputController CreateInputController(IInputHandler inputHandler)
    {
        var inputController = new KeyboardInputController();
        
        inputController.RegisterHandler(inputHandler);
        
        return inputController;
    }
    
    private IInputHandler CreateInputHandler()
    {
        var inputHandler = new KeyboardInputHandler();
        
        return inputHandler;
    }
    
    private Player<char> CreatePlayer(Landscape<char, Cell<char>> landscape)
    {
        var playerMovement = new PlayerMovement(landscape, new Vector(8, 8, 0), '@');
        var experienceCalculator = new ExperienceCalculator();
        
        Random random = new ();
        WeaponFactory weaponFactory = new();
        ArmourFactory armorFactory = new();
        
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
        
        var result = new Player<char>(playerMovement, experienceCalculator)
        {
            Name = "atogeib",
            Strength = 16,
            Experience = 0,
            ExperienceLevel = 1,
            AmorClass = 10,
            HitPoints = 12,
            MaxHitPoints = 12,
            ActiveWeapon = mace,
            Weapons = new List<Weapon>() {mace, bow, arrows},
            ActiveArmor = armor,
            Icon = '@',
            Armors = new List<Armour>() {armor},
            Damage = new List<DiceThrow>() { diceThrow}
        };
        return result;
    }

    private void SetupPlayerEventHandling(Player<char> player, IInputHandler inputHandler,
        IInputController inputController)
    {
        inputController.RegisterHandler(inputHandler);
        inputHandler.OnMovement += player.Translate;
    }
    
    private IEnumerable<Monster<char>> CreateMonsters(Landscape<char, Cell<char>> landscape)
    {
        var monsterFactory = new MonsterFactory<char>(new MotionControllerFactory(landscape), CreateIconsFromBreeds());
        var monsters = new List<Monster<char>>();
        
        var monster = monsterFactory.CreateMonster(nameof(Kestrel), new Vector(1, 1, 0));
        monsters.Add(monster);
        
        return monsters;
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