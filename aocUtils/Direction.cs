using System.Diagnostics;
using aocUtils;

namespace day_06;

public class Direction
{
    public static Direction UP = new Direction('^', new Coordinate2D(-1,0));
    public static Direction RIGHT = new Direction('>', new Coordinate2D(0,1));
    public static Direction DOWN = new Direction('v', new Coordinate2D(1,0));
    public static Direction LEFT = new Direction('<', new Coordinate2D(0,-1));
    private static List<Direction> ALL_DIRECTIONS = new(){UP, RIGHT, DOWN, LEFT};

    private char character;
    private Coordinate2D vector;
    
    private Direction(char character, Coordinate2D vector)
    {
        this.character = character;
        this.vector = vector;
    }       
    
    public Coordinate2D getVector() => vector;
    public char getCharacter() => character;

    public static Direction GetDirection(char dir) => ALL_DIRECTIONS.Single(d => d.character == dir);

    public static bool isDirectionChar(char tested) =>
        ALL_DIRECTIONS.Select(d => d.character).ToList().Contains(tested);

    public static Direction rotate90DegreeClockwise(Direction dir)
    {
        int dirPos = ALL_DIRECTIONS.IndexOf(dir);
        return ALL_DIRECTIONS[(dirPos+1) % ALL_DIRECTIONS.Count];
    }
    
    public static Direction rotate180Degrees(Direction dir)
    {
        int dirPos = ALL_DIRECTIONS.IndexOf(dir);
        return ALL_DIRECTIONS[(dirPos+2) % ALL_DIRECTIONS.Count];
    }
    
    public static Direction rotate90DegreeCounterClockwise(Direction dir)
    {
        int dirPos = ALL_DIRECTIONS.IndexOf(dir);
        return ALL_DIRECTIONS[(dirPos+3) % ALL_DIRECTIONS.Count];
    }
    
    public Direction rotate90DegreeClockwise()
    {
        return Direction.rotate90DegreeClockwise(this);
    }
    
    public Direction rotate180Degrees()
    {
        return Direction.rotate180Degrees(this);
    }
    
    public Direction rotate90DegreeCounterClockwise()
    {
        return Direction.rotate90DegreeCounterClockwise(this);
    }

}