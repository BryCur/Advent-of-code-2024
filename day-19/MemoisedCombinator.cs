namespace day_19;

public class MemoisedCombinator
{
    private Dictionary<string, IEnumerable<List<string>>> memoCache = new Dictionary<string, IEnumerable<List<string>>>();
    private HashSet<string> availableTowels;

    public MemoisedCombinator(HashSet<string> towels)
    {
        availableTowels = towels;
    }

    public IEnumerable<List<string>> GenerateTowelCombinationsLazy(string goal)
    {
        // Vérifie si le résultat pour cet objectif existe déjà dans le cache
        if (memoCache.ContainsKey(goal))
        {
            return memoCache[goal];
        }

        // Calcul des combinaisons
        IEnumerable<List<string>> result = Generate(goal);
        
        // Mémorisation du résultat pour cette chaîne
        memoCache[goal] = result;

        return result;
    }

    private IEnumerable<List<string>> Generate(string goal)
    {
        if (goal.Length == 0)
        {
            yield return new List<string>();
        }
        else
        {
            foreach (var element in availableTowels.Where(goal.StartsWith))
            {
                string remainingGoal = goal.Substring(element.Length);
                foreach (var subCombination in GenerateTowelCombinationsLazy(remainingGoal))
                {
                    var combination = new List<string>(subCombination);
                    combination.Insert(0, element);
                    yield return combination;
                }
            }
        }
    }
}