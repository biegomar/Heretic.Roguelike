using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.ContentGeneration.Dungeons;

public record struct DungeonProperties(Vector DungeonSize, Vector GridSize, ushort InvisibleRoomsCount);