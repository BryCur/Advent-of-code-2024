using System.Transactions;
using aocUtils;

namespace day_12;

public class Day12Node : LinkedNode<char>
{
    private Coordinate2D position;
    protected GardenGroup? garden;
    public Day12Node(char val, Coordinate2D pos) : base(val){ position = pos; }
    public Day12Node(char val, int x, int y) : base(val){ position = new Coordinate2D(x, y); }

    public bool AddNeighbor(Day12Node neighbour)
    {
        if (neighbour.GetValue() == GetValue())
        {
            neighbour.AddAdjacentNode(this);
            return AddAdjacentNode(neighbour);
        }

        return false;
    }

    public int getCountablePerimeter()
    {
        return 4 - AdjacentNodes.Count;
    }
    
    public void setGarden(GardenGroup g) => this.garden = g;
    public bool hasGarden() => garden != null;
    
    public Coordinate2D getPosition() { return position; }
    public GardenGroup? getGarden() { return garden; }
}