using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Creatures.Monsters.Breeds;
using Heretic.Roguelike.Numerics;
using Moq;

namespace Heretic.Roguelike.Tests
{
    public class MonsterFactoryTests
    {
        private readonly Mock<IMotionControllerFactory<string>> motionControllerFactoryMock =
            new Mock<IMotionControllerFactory<string>>();

        private readonly Mock<IMotionController<string>> motionControllerMock = new Mock<IMotionController<string>>();
        private readonly Mock<IMonsterBreed> zombieMock = new Mock<IMonsterBreed>();
        private readonly IDictionary<string, string> _icons;

        // Konstruktor für Initialisierung
        public MonsterFactoryTests()
        {
            // Icons für alle Monster basierend auf deren Namen vorbereiten
            _icons = typeof(Zombie).Assembly
                .GetTypes()
                .Where(type => typeof(IMonsterBreed).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .ToDictionary(
                    breedType => ((IMonsterBreed)Activator.CreateInstance(breedType)).Name,
                    breedType => $"{((IMonsterBreed)Activator.CreateInstance(breedType)).Name}Icon"
                );

            // Zombie-Mock vorbereiten
            zombieMock.Setup(z => z.Name).Returns("Zombie");
            zombieMock
                .Setup(z => z.Spawn(It.IsAny<IMotionController<string>>(), It.IsAny<string>()))
                .Returns((IMotionController<string> controller, string icon) => new Monster<string>(controller)
                {
                    Breed = "Zombie",
                    Icon = icon
                });
        }

        // MemberData für dynamischen Test
        public static IEnumerable<object[]> MonsterBreeds =>
            typeof(Zombie).Assembly
                .GetTypes()
                .Where(type => typeof(IMonsterBreed).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(type => new object[] { (IMonsterBreed)Activator.CreateInstance(type) });

        [Theory]
        [MemberData(nameof(MonsterBreeds))]
        public void CreateMonster_ShouldReturnValidMonster_ForEachMonsterBreed(IMonsterBreed breed)
        {
            // Arrange
            motionControllerFactoryMock
                .Setup(factory => factory.CreateMonsterMotionController(It.IsAny<IMonsterBreed>(), It.IsAny<Vector>()))
                .Returns(motionControllerMock.Object);

            var factory = new MonsterFactory<string>(motionControllerFactoryMock.Object, _icons);

            // Monster-Typ in der Factory registrieren
            factory.RegisterMonsterBreed(breed);

            // Act
            var monster = factory.CreateMonster(breed.Name, Vector.Zero);

            // Assert
            Assert.NotNull(monster);

            // Grundlegende Eigenschaften überprüfen
            Assert.Equal(breed.Name, monster.Breed);
            Assert.NotNull(monster.Icon);
            Assert.Equal(_icons[breed.Name], monster.Icon);

            // Rüstungswert prüfen
            Assert.InRange(monster.AmourClass, -2, 10);
            
            // Stärke prüfen
            var firstDice = monster.Damage.First();
            if (firstDice != null && breed.Name != "VenusFlytrap")
            {
                Assert.InRange(monster.Strength, 0, firstDice.Dice.Sides * firstDice.Tries);        
            }

            // Schaden prüfen (falls passend)
            Assert.NotNull(monster.Damage);
            if (breed.Name != "VenusFlytrap")
            {
                Assert.NotEmpty(monster.Damage);
            }
        }

        [Fact]
        public void CreateMonster_ShouldCreateZombie()
        {
            // Arrange
            motionControllerFactoryMock
                .Setup(factory => factory.CreateMonsterMotionController(It.IsAny<IMonsterBreed>(), It.IsAny<Vector>()))
                .Returns(motionControllerMock.Object);

            var factory = new MonsterFactory<string>(motionControllerFactoryMock.Object, _icons);
            factory.RegisterMonsterBreed(zombieMock.Object);

            // Act
            var monster = factory.CreateMonster("Zombie", Vector.Zero);

            // Assert
            Assert.NotNull(monster);
            Assert.Equal("Zombie", monster.Breed);
            Assert.Equal("ZombieIcon", monster.Icon);
        }

        [Fact]
        public void CreateMonster_ShouldThrowException_ForUnregisteredMonster()
        {
            // Arrange
            var factory = new MonsterFactory<string>(motionControllerFactoryMock.Object, _icons);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => factory.CreateMonster("UnregisteredMonster", Vector.Zero));
        }

        [Fact]
        public void CreateMonster_ShouldUseDefaultIcon_IfNoIconIsDefinedForMonster()
        {
            // Arrange
            var missingIconBreedMock = new Mock<IMonsterBreed>();
            missingIconBreedMock.Setup(breed => breed.Name).Returns("NoIconMonster");
            missingIconBreedMock
                .Setup(z => z.Spawn(It.IsAny<IMotionController<string>>(), It.IsAny<string>()))
                .Returns((IMotionController<string> controller, string icon) => new Monster<string>(controller)
                {
                    Breed = "NoIconMonster"
                });

            var factory = new MonsterFactory<string>(motionControllerFactoryMock.Object, _icons);

            // Monster-Typ in der Factory registrieren
            factory.RegisterMonsterBreed(missingIconBreedMock.Object);

            // Act
            var monster = factory.CreateMonster("NoIconMonster", Vector.Zero);

            // Assert
            Assert.NotNull(monster);
            Assert.Equal("NoIconMonster", monster.Breed);

            // Standardwert von T verwenden (in diesem Fall `default(string)`, also `null`)
            Assert.Null(monster.Icon);
        }
    }
}