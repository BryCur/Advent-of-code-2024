using System.Transactions;
using aocUtils;

namespace day_12;

public class Day12Node : LinkedNode<char>
{
    Coordinate2D position;

    public Day12Node(char val, Coordinate2D pos) : base(val){ position = pos; }
    public Day12Node(char val, int x, int y) : base(val){ position = new Coordinate2D(x, y); }

    public bool AddNeighbor(Day12Node node)
    {
        if (node.GetValue() == GetValue())
        {
            node.AddAdjacentNode(this);
            return AddAdjacentNode(node);
        }

        return false;
    }

    public int getCountablePerimeter()
    {
        return 4 - AdjacentNodes.Count;
    }
    
    public Coordinate2D getPosition() { return position; }
}