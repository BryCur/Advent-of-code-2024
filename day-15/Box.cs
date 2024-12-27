using aocUtils;

namespace day_15;

public class Box
{
    private Coordinate2D reference;
    private int boxLength = 1;
    
    public Box(Coordinate2D referenceCoordinates)
    {
        reference = referenceCoordinates;
    }

    public bool isPositionInBox(Coordinate2D coordinate)
    {
        return coordinate.getX() == reference.getX() && coordinate.getY() >= reference.getY() && coordinate.getY() <= reference.getY() +boxLength;
    }

    public void shiftBox(Direction direction)
    {
        reference += direction.getVector();
    }
}