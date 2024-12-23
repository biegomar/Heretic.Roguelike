﻿using System;

namespace Heretic.Roguelike.Subsystems.Amors;

public class ArmorFactory
{
  public Armor CreateArmor(ArmorType armorType)
    {
        return armorType switch
        {
            ArmorType.Leather => GetLeatherArmor(),
            ArmorType.RingMail => GetRingMailArmor(),
            ArmorType.StuddedLeather => GetStuddedLeatherArmor(),
            ArmorType.ScaleMail => GetScaleMailArmor(),
            ArmorType.ChainMail => GetChainMailArmor(),
            ArmorType.SplintMail => GetSplintMailArmor(),
            ArmorType.BandedMail => GetBandedMailArmor(),
            ArmorType.PlateMail => GetPlateMailArmor(),
            _ => throw new ArgumentOutOfRangeException(nameof(armorType), armorType, null)
        };
    }
    
    private Armor GetLeatherArmor()
    {
        return new Armor
        {
            Type = ArmorType.Leather,
            Flag = ArmorFlag.IsKnown, 
            Count = 1, 
            AmorClass = 8 
        };
    }
    
    private Armor GetRingMailArmor()
    {
        return new Armor
        {
            Type = ArmorType.RingMail,
            Flag = ArmorFlag.IsKnown, 
            Count = 1, 
            AmorClass = 7 
        };
    }
    
    private Armor GetStuddedLeatherArmor()
    {
        return new Armor
        {
            Type = ArmorType.StuddedLeather,
            Flag = ArmorFlag.IsKnown, 
            Count = 1, 
            AmorClass = 7 
        };
    }
    
    private Armor GetScaleMailArmor()
    {
        return new Armor
        {
            Type = ArmorType.ScaleMail,
            Flag = ArmorFlag.IsKnown, 
            Count = 1, 
            AmorClass = 6 
        };
    }
    
    private Armor GetChainMailArmor()
    {
        return new Armor
        {
            Type = ArmorType.ChainMail,
            Flag = ArmorFlag.IsKnown, 
            Count = 1, 
            AmorClass = 5 
        };
    }
    
    private Armor GetSplintMailArmor()
    {
        return new Armor
        {
            Type = ArmorType.SplintMail,
            Flag = ArmorFlag.IsKnown, 
            Count = 1, 
            AmorClass = 4 
        };
    }
    
    private Armor GetBandedMailArmor()
    {
        return new Armor
        {
            Type = ArmorType.BandedMail,
            Flag = ArmorFlag.IsKnown, 
            Count = 1, 
            AmorClass = 4 
        };
    }
    
    private Armor GetPlateMailArmor()
    {
        return new Armor
        {
            Type = ArmorType.PlateMail,
            Flag = ArmorFlag.IsKnown, 
            Count = 1, 
            AmorClass = 3 
        };
    }  
}