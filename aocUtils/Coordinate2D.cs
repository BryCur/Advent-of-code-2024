using System.Reflection;

namespace aocUtils;

public class Coordinate2D
{
    private int x;
    private int y;

    public Coordinate2D(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public int getX() => x;
    public int getY() => y;
    public (int x, int y) getCoordinates() => (x, y);

    public static Coordinate2D operator +(Coordinate2D a, Coordinate2D b) => new Coordinate2D(a.x + b.x, a.y + b.y);
    public static Coordinate2D operator -(Coordinate2D a, Coordinate2D b) => new Coordinate2D(a.x - b.x, a.y - b.y);
    public static Coordinate2D operator *(Coordinate2D a, int b) => new Coordinate2D(a.x*b, a.y *b);
    public static bool operator ==(Coordinate2D a, Coordinate2D b) => a.x == b.x && a.y == b.y;
    public static bool operator !=(Coordinate2D a, Coordinate2D b) => a.x != b.x || a.y != b.y;
    public bool Equals(Coordinate2D other) => other == this;
    public override bool Equals(object? obj) => obj is Coordinate2D other && Equals(other);
    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();
}