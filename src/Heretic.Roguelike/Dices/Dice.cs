using System;

namespace Heretic.Roguelike.Dices
{
    public class Dice
    {
        private readonly Random random;
    
        public DiceType TypeOfDice { get; }
        public byte Sides => (byte)this.TypeOfDice;

        public Dice(DiceType typeOfDice)
        {
            this.random = new();
            this.TypeOfDice = typeOfDice;
        }
    
        public Dice(DiceType typeOfDice, Random random)
        {
            this.random = random;
            this.TypeOfDice = typeOfDice;
        }
    
        public ushort Roll(byte tries)
        {
            ushort result = 0;

            for (sbyte i = 0; i < tries; i++)
            {
                result += (ushort)random.Next(1, this.Sides + 1);
            }

            return result;
        }
        
        public ushort RollAndGetBestTry(byte tries)
        {
            ushort result = 0;

            for (sbyte i = 0; i < tries; i++)
            {
                result = Math.Max(result, (ushort)random.Next(1, this.Sides + 1));
            }

            return result;
        }
        
        public ushort RollAndGetLowestTry(byte tries)
        {
            var result = (ushort)(this.Sides + 1);

            for (sbyte i = 0; i < tries; i++)
            {
                result = Math.Min(result, (ushort)random.Next(1, this.Sides + 1));
            }

            return result;
        }
        
        public static ushort Roll(DiceThrow diceThrow)
        {
            return diceThrow.Dice.Roll(diceThrow.Tries);
        }
        
        public static Dice D0 => new(DiceType.D0);
        public static Dice D1 => new(DiceType.D1);
        public static Dice D2 => new(DiceType.D2);
        public static Dice D3 => new(DiceType.D3);
        public static Dice D4 => new(DiceType.D4);
        public static Dice D5 => new(DiceType.D5);
        public static Dice D6 => new(DiceType.D6);
        public static Dice D8 => new(DiceType.D8);
        public static Dice D10 => new(DiceType.D10);
        public static Dice D12 => new(DiceType.D12);
        public static Dice D20 => new(DiceType.D20);
        public static Dice D100 => new(DiceType.D100);
    }
}