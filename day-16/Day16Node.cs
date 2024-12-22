using aocUtils;

namespace day_16;

public class Day16Node: LinkedNode<Coordinate2D>
{

    private bool isWall;
    public Day16Node(Coordinate2D val, bool isWall) : base(val) { this.isWall = isWall; }
    public Day16Node(int x, int y, bool isWall) : base(new Coordinate2D(x, y)) { this.isWall = isWall; }

    public void AddNeighbor(Day16Node neighbor)
    {
        if (this.isWall || neighbor.isWall)
        {
            return;
        }
        neighbor.AddAdjacentNode(this);
        AddAdjacentNode(neighbor);
    }
    
    public bool getIsWall() => isWall;
    
    
}