using System;
using System.Collections.Generic;
using Heretic.Roguelike.Amours;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Monsters.Breeds;

namespace Heretic.Roguelike.Things.Monsters;

public class MonsterFactory<T>
{
    private readonly IMotionControllerFactory<T> motionControllerFactory;
    private readonly IArmourCalculator armourCalculator;
    private readonly IDictionary<string, T> icons;
    private IDictionary<string, IMonsterBreed> monsterBreeds;
    
    
    public MonsterFactory(IMotionControllerFactory<T> motionControllerFactory, IArmourCalculator armourCalculator, IDictionary<string, T> icons)
    {
        this.motionControllerFactory = motionControllerFactory;
        this.armourCalculator = armourCalculator;
        this.icons = icons;
        
        GenerateMonsterDictionary();
    }

    private void GenerateMonsterDictionary()
    {
        this.monsterBreeds = new Dictionary<string, IMonsterBreed>
        {
            { nameof(Zombie), new Zombie() },
            { nameof(Yeti), new Yeti() },
            { nameof(Xeroc), new Xeroc()},
            { nameof(Wraith), new Wraith()},
            { nameof(VenusFlytrap), new VenusFlytrap()},
            { nameof(Vampire), new Vampire()},
            { nameof(Urvile), new Urvile()},
            { nameof(Troll), new Troll()},
            { nameof(Snake), new Snake()},
            { nameof(Rattlesnake), new Rattlesnake()},
            { nameof(Quagga), new Quagga()},
            { nameof(Phantom), new Phantom()},
            { nameof(Orc), new Orc()},
            { nameof(Nymph), new Nymph()},
            { nameof(Medusa), new Medusa()},
            { nameof(Leprechaun), new Leprechaun()},
            { nameof(Kestrel), new Kestrel()},
            { nameof(Jabberwock), new Jabberwock()},
            { nameof(IceMonster), new IceMonster()},
            { nameof(Hobgoblin), new Hobgoblin()},
            { nameof(Griffin), new Griffin()},
            { nameof(Emu), new Emu()},
            { nameof(Dragon), new Dragon()},
            { nameof(Centaur), new Centaur()},
            { nameof(Bat), new Bat()},
            { nameof(Aquator), new Aquator()}
            
            // Some more ideas:
            // { nameof(Ogre), new Ogre()},
            // { nameof(Pixie), new Pixie()},
            // { nameof(Ghost), new Ghost()},
            // { nameof(Hydra), new Hydra()},
            // { nameof(Golem), new Golem()},
            // { nameof(Cyclops), new Cyclops()},
            // { nameof(Manticore), new Manticore()},
            // { nameof(Basilisk), new Basilisk()},
            // { nameof(Harpy), new Harpy()},
            // { nameof(Chimera), new Chimera()},
            
        };
    }

    public Monster<T> CreateMonster(string monsterName, Vector startingPosition)
    {
        if (!this.monsterBreeds.TryGetValue(monsterName, out var monsterBreed))
        {
            throw new ArgumentOutOfRangeException(nameof(monsterName), monsterName, "Monster breed not registered.");
        }

        if (!this.icons.TryGetValue(monsterName, out T icon))
        {
            icon = default!;
        }
        
        return monsterBreed.Spawn(motionControllerFactory.CreateMonsterMotionController(monsterBreed, startingPosition), icon);
    }
    
    public void RegisterMonsterBreed(IMonsterBreed monsterBreed)
    {
        this.monsterBreeds.TryAdd(monsterBreed.Name, monsterBreed);
    }
}