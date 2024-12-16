namespace day_11;

public interface IRule
{
    public bool IsApplicable(long value);
    public void Apply(int index, List<long> stones);
}