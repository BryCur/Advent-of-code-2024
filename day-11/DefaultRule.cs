using System.Runtime.CompilerServices;

namespace day_11;

public class DefaultRule : IRule
{
    public bool IsApplicable(long value)
    {
        return true;
    }

    public void Apply(int index, List<long> stones)
    {
        stones[index] *= 2024;
    }
}