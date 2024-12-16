namespace day_11;

public class FirstRule : IRule
{
    public bool IsApplicable(long value)
    {
        return value == 0;
    }

    public void Apply(int index, List<long> stones)
    {
        stones[index] = 1;
    }
}