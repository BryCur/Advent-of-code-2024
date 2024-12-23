using aocUtils;

public class Day18Node: LinkedNode<Coordinate2D>
{

    private bool isWall;
    public Day18Node(Coordinate2D val, bool isWall) : base(val) { this.isWall = isWall; }
    public Day18Node(int x, int y, bool isWall) : base(new Coordinate2D(x, y)) { this.isWall = isWall; }

    public void AddNeighbor(Day18Node neighbor)
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