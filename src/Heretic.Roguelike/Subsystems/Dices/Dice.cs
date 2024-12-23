using System;

namespace Heretic.Roguelike.Subsystems.Dices
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
    }
}