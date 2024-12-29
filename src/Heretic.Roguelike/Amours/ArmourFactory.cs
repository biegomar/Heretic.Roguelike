using System;
using System.Collections.Generic;
using Heretic.Roguelike.Amours.Types;

namespace Heretic.Roguelike.Amours;

public class ArmourFactory
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

    public Armour CreateArmour(string armourType)
    {
        if (!this.armourTypes.TryGetValue(armourType, out var armour))
        {
            throw new ArgumentOutOfRangeException(nameof(armourType), armourType, "Armour type not registered.");
        }
        
        return armour.Create();
    }
    
    public void RegisterArmourType(IArmourType armourType)
    {
        this.armourTypes.TryAdd(armourType.Name, armourType);
    }
}