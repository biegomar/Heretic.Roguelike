using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.ContentGeneration;

public record ExitConfiguration(bool IsInWall, bool IsHidden, Vector? ExitPosition);