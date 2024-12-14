using System.Transactions;
using aocUtils;

namespace day_10;

public class Day10Node : LinkedNode<int>
{
    private HashSet<Day10Node> nextNodes = new HashSet<Day10Node>();

    public Day10Node(int val) : base(val){}

    public bool AddNeighbor(Day10Node node)
    {
        if (node.GetValue() - 1 == this.GetValue())
        {
            nextNodes.Add(node);
        } else if (node.GetValue() + 1 == this.GetValue())
        {
            node.AddNeighbor(this);
        } 
        
        node.AddAdjacentNode(this);
        return base.AddAdjacentNode(node);
    }
    
    
    public HashSet<Day10Node> GetNextNodes() => nextNodes;
}