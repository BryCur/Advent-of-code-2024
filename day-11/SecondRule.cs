namespace day_11;

public class SecondRule : IRule
{
    public bool IsApplicable(long value)
    {
        return (long)(Math.Log10(value)+1) % 2L == 0;
    }

    public void Apply(int index, List<long> stones)
    {
        long value = stones[index];

        long digitCount = (long)(Math.Log10(value) + 1);

        long left = value / (long)Math.Pow(10, digitCount/2);
        long right = value % (long)Math.Pow(10, digitCount/2);
        
        stones[index] = right;
        stones.Insert(index, left);
    }
}