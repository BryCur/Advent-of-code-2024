using aocUtils;

namespace day_21;

public class NumericPad
{
    public static Coordinate2D KEY_7 = new Coordinate2D(0, 0);
    public static Coordinate2D KEY_8 = new Coordinate2D(0, 1);
    public static Coordinate2D KEY_9 = new Coordinate2D(0, 2);
    public static Coordinate2D KEY_4 = new Coordinate2D(1, 0);
    public static Coordinate2D KEY_5 = new Coordinate2D(1, 1);
    public static Coordinate2D KEY_6 = new Coordinate2D(1, 2);
    public static Coordinate2D KEY_1 = new Coordinate2D(2, 0);
    public static Coordinate2D KEY_2 = new Coordinate2D(2, 1);
    public static Coordinate2D KEY_3 = new Coordinate2D(2, 2);
    public static Coordinate2D GAP   = new Coordinate2D(3, 0);
    public static Coordinate2D KEY_0 = new Coordinate2D(3, 1);
    public static Coordinate2D KEY_A = new Coordinate2D(3, 2);

    private static Dictionary<char, Coordinate2D> keyMap = new Dictionary<char, Coordinate2D>()
    {
        { '7', KEY_7 }, { '8', KEY_8 }, { '9', KEY_9 },
        { '4', KEY_4 }, { '5', KEY_5 }, { '6', KEY_6 },
        { '1', KEY_1 }, { '2', KEY_2 }, { '3', KEY_3 },
                        { '0', KEY_0 }, { 'A', KEY_A }
    };

    public static string getMovementInstructionFromTo(Coordinate2D from, Coordinate2D to)
    {
        Coordinate2D distance = to - from;
        string instruction = "";
        
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