using System.Diagnostics;
using aocUtils;

namespace day_06;

public class Direction
{
    public static Direction UP = new Direction('^', new Coordinate2D(-1,0));
    public static Direction RIGHT = new Direction('>', new Coordinate2D(0,1));
    public static Direction DOWN = new Direction('v', new Coordinate2D(+1,0));
    public static Direction LEFT = new Direction('<', new Coordinate2D(0,-1));
    private static HashSet<Direction> ALL_DIRECTIONS = new(){UP, RIGHT, DOWN, LEFT};

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

}