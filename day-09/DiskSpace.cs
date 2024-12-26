namespace day_09;

public class DiskSpace
{
    private int _size;
    private int _value;
    private bool _isEmpty;
    public int Size{ get => _size; set => _size = value; }
    public int Value { get => _value; set => _value = value; }
    public bool IsEmpty { get => _isEmpty; set => _isEmpty = value; }

    public DiskSpace(int size, int value, bool isEmpty)
    {
        _size = size;
        _value = value;
        _isEmpty = isEmpty;
    }
}