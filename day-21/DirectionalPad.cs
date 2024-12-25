using aocUtils;

namespace day_21;

public class DirectionalPad
{
    public static Coordinate2D GAP   = new Coordinate2D(0, 0);
    public static Coordinate2D KEY_UP = new Coordinate2D(0, 1);
    public static Coordinate2D KEY_A = new Coordinate2D(0, 2);
    public static Coordinate2D KEY_LEFT = new Coordinate2D(1, 0);
    public static Coordinate2D KEY_DOWN = new Coordinate2D(1, 1);
    public static Coordinate2D KEY_RIGHT = new Coordinate2D(1, 2);
    
    private static Dictionary<char, Coordinate2D> keyMap = new Dictionary<char, Coordinate2D>()
    {
                                                     { Direction.UP.getCharacter(), KEY_UP },     { 'A', KEY_A },
        { Direction.LEFT.getCharacter(), KEY_LEFT }, { Direction.DOWN.getCharacter(), KEY_DOWN }, { Direction.RIGHT.getCharacter(), KEY_RIGHT },
    };

    public static string getMovementInstructionFromTo(Coordinate2D from, Coordinate2D to)
    {
        Coordinate2D distance = to - from;
        string instruction = "";

        // vertical axis
        Direction verticalDirection = distance.getX() < 0 ? Direction.UP : Direction.DOWN;
        Direction horizontalDirection = distance.getY() < 0 ? Direction.LEFT : Direction.RIGHT;

        for (int i = 0; i < Math.Abs(distance.getY()); i++)
        {
            instruction += horizontalDirection.getCharacter();
        }
        for (int i = 0; i < Math.Abs(distance.getX()); i++)
        {
            instruction += verticalDirection.getCharacter();
        }
        
        return instruction + 'A';
    }
    
    public static Coordinate2D getKeyCoordinate(char key)
    {
        return keyMap[key];
    }
}