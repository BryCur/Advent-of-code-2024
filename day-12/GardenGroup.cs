using aocUtils;

namespace day_12;

public class GardenGroup(char gardenGroup)
{
    private char groupChar;
    private HashSet<Day12Node> TilesInGarden = new HashSet<Day12Node>();
    
    public long getPerimeter()
    {
        return TilesInGarden.Aggregate(0, (i, node) => i += node.getCountablePerimeter());
    }

    public long getArea()
    {
        return TilesInGarden.Count;
    }

    public long getSideCount()
    {
        
        return 4L;
    }

    public bool addTile(Day12Node tile)
    {
        tile.setGarden(this);
        return TilesInGarden.Add(tile);
    }

    public bool isPositionInGarden(Coordinate2D position)
    {
        return TilesInGarden.Any(tile => tile.getPosition().Equals(position));
    }
    
    public bool isPositionInGarden(int i, int j)
    {
        return isPositionInGarden(new Coordinate2D(i, j));
    }
}