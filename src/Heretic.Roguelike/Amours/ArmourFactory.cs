using System;
using System.Collections.Generic;
using Heretic.Roguelike.Amours.Types;
using Heretic.Roguelike.Battles;

namespace Heretic.Roguelike.Amours;

public class ArmourFactory(IArmourCalculator armourCalculator)
{
    private readonly IDictionary<string, IArmourType> armourTypes = new Dictionary<string, IArmourType>()
    {
        {nameof(BandedMail), new BandedMail()},
        {nameof(ChainMail), new ChainMail()},
        {nameof(Leather), new Leather()},
        {nameof(PlateMail), new PlateMail()},
        {nameof(RingMail), new RingMail()},
        {nameof(ScaleMail), new ScaleMail()},
        {nameof(SplintMail), new SplintMail()},
        {nameof(StuddedLeather), new StuddedLeather()},
    };
    
    private readonly IDictionary<string, sbyte> armourClasses = new Dictionary<string, sbyte>()
    {
        {nameof(Leather), 8},
        {nameof(RingMail), 7},
        {nameof(StuddedLeather), 7},
        {nameof(ScaleMail), 6},
        {nameof(ChainMail), 5},
        {nameof(SplintMail), 4},
        {nameof(BandedMail), 4},
        {nameof(PlateMail), 3},
    };

    public Armour CreateArmour(string armourType)
    {
        if (!this.armourTypes.TryGetValue(armourType, out var armour))
        {
            throw new ArgumentOutOfRangeException(nameof(armourType), armourType, "Armour type not registered.");
        }

        if (!this.armourClasses.TryGetValue(armourType, out var armourClass))
        {
            throw new ArgumentOutOfRangeException(nameof(armourType), armourType, "Armour class not registered.");
        }
        
        return armour.Create((sbyte)armourCalculator.CalculateArmourFromArmourClass(armourClass));
    }
    
    public void RegisterArmourType(IArmourType armourType, sbyte armourClass)
    {
        this.armourTypes.TryAdd(armourType.Name, armourType);
        this.armourClasses.TryAdd(armourType.Name, armourClass);
    }
}