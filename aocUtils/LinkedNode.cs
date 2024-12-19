using System.Dynamic;

namespace aocUtils;

public class LinkedNode<T>(T val)
{
    private T Value => val;
    protected HashSet<LinkedNode<T>> AdjacentNodes = new HashSet<LinkedNode<T>>();
    
    public bool AddAdjacentNode(LinkedNode<T> node)
    {
        return this.AdjacentNodes.Add(node);
    }
    
    public HashSet<LinkedNode<T>> GetAdjacentNodes() => AdjacentNodes;
    
    public T GetValue() => this.Value;
}