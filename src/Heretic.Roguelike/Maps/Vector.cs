namespace Heretic.Roguelike.Maps;

public record Vector(int X, int Y, int Z)
{
    public override string ToString()
    {
        return $"[{this.X}, {this.Y}, {this.Z}]";
    }

    public static Vector Zero => new Vector(0, 0, 0);
    public static Vector One => new Vector(1, 1, 1);
    public static Vector Up => new Vector(0, 1, 0);
    public static Vector Down => new Vector(0, -1, 0);
    public static Vector Left => new Vector(-1, 0, 0);
    public static Vector Right => new Vector(1, 0, 0);
}