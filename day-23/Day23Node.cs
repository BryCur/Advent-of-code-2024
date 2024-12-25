using aocUtils;

public class Day23Node: LinkedNode<string>
{
    public Day23Node(string val) : base(val) {}

    public void AddNeighbor(Day23Node neighbor)
    {
        neighbor.AddAdjacentNode(this);
        AddAdjacentNode(neighbor);
    }
}