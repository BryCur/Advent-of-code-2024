namespace day_24;

public class Instructions
{
    private string ref1;
    private string ref2;
    private string dst;
    private string operation;
    private bool done = false;

    public Instructions(string ref1, string ref2, string dest, string operation)
    {
        this.ref1 = ref1;       
        this.ref2 = ref2;       
        this.dst = dest;       
        this.operation = operation;     
    }

    public string getRef1() => ref1;
    public string getRef2() => ref2;
    public string getDest() => dst;
    public string getOperation() => operation;
    public bool isDone() => done;

    public bool isOperationPossible(Dictionary<string, bool?> references)
    {
        return references.ContainsKey(ref1)
               && references[ref1].HasValue
               && references.ContainsKey(ref2)
               && references[ref2].HasValue;
    }

    public bool performOperation(Dictionary<string, bool?> references)
    {
        if(!isOperationPossible(references)) throw new Exception("Invalid operation");
        done = true;

        bool result;
        bool a = references[ref1].Value;
        bool b = references[ref2].Value;

        switch (operation.ToUpper())
        {
            case "AND": result = a && b; break;
            case "OR": result = a || b; break;
            case "XOR": result = a != b; break;
            default: throw new Exception("Invalid operation");
        }

        return result;
    }
}