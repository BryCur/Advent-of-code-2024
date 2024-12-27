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
        return coordinate.getX() == reference.getX() 
               && coordinate.getY() >= reference.getY() 
               && coordinate.getY() <= reference.getY() + boxLength;
    }

    public bool isBoxInPosList(List<Coordinate2D> coordinates)
    {
        List<Coordinate2D> posList = getCoveredBoxCoordinates();
        return coordinates.Intersect(posList).Any();
    }

    public void shiftBox(Direction direction)
    {
        reference = reference + direction.getVector();
    }
    
    public Coordinate2D getReference() => reference;
    public List<Coordinate2D> getCoveredBoxCoordinates() => new List<Coordinate2D>()
    {
        reference, new (reference.getX(), reference.getY()+boxLength)
    };
}