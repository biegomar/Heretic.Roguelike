using System;
using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Creatures.Monsters;
using Xunit;

namespace Heretic.Roguelike.Tests
{
    public class MonsterFactoryTests
    {
        private readonly IDictionary<MonsterBreed, string> _icons;

        public MonsterFactoryTests()
        {
            // Icons für alle Monster vorbereiten
            _icons = Enum.GetValues(typeof(MonsterBreed))
                .Cast<MonsterBreed>()
                .ToDictionary(breed => breed, breed => $"{breed}Icon");
        }

        public static IEnumerable<object[]> MonsterBreeds =>
            Enum.GetValues(typeof(MonsterBreed))
                .Cast<MonsterBreed>()
                .Select(breed => new object[] { breed });

        [Theory]
        [MemberData(nameof(MonsterBreeds))]
        public void CreateMonster_ShouldReturnValidMonster_ForEachMonsterBreed(MonsterBreed breed)
        {
            // Arrange
            var factory = new MonsterFactory<string>(_icons);

            // Act
            var monster = factory.CreateMonster(breed);

            // Assert
            Assert.NotNull(monster);

            // Grundlegende Eigenschaften überprüfen
            Assert.Equal(breed, monster.Breed);
            Assert.NotNull(monster.Icon);
            Assert.Equal(_icons[breed], monster.Icon);
            
            // Schaden sollte nicht null oder leer sein (Ausnahme: VenusFlytrap)
            Assert.NotNull(monster.Damage);
            if (breed != MonsterBreed.VenusFlytrap)
            {
                Assert.NotEmpty(monster.Damage);
            }

            // Armor Class sollte in einem sinnvollen Bereich liegen: z.B., -2 bis 10
            Assert.InRange(monster.AmorClass, -2, 10);
        }

        
    }
}