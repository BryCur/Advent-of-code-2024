using aocUtils;

namespace day_12;

public class GardenGroup(char gardenGroup)
{
    private char groupChar;
    HashSet<Day12Node> TilesInGarden = new HashSet<Day12Node>();
    
    public long getPerimeter()
    {
        return TilesInGarden.Aggregate(0, (i, node) => i += node.getCountablePerimeter());
    }

    public long getArea()
    {
        return TilesInGarden.Count;
    }

    public bool isPositionInGarden(Coordinate2D position)
    {
        return TilesInGarden.Any(tile => tile.getPosition().Equals(position));
    }
}