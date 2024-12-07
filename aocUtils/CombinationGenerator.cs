using System;
using System.Collections.Generic;

public static class CombinationGenerator
{
    public static IEnumerable<List<T>> GenerateCombinationsLazy<T>(List<T> elements, int length)
    {
        if (length == 0)
        {
            yield return new List<T>();
        }
        else
        {
            foreach (var element in elements)
            {
                foreach (var subCombination in GenerateCombinationsLazy(elements, length - 1))
                {
                    subCombination.Insert(0, element);
                    yield return subCombination;
                }
            }
        }
    }
}