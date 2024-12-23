using System.IO;
using System.Runtime.CompilerServices;
using aocUtils.IO;

public class Day19
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    
    private string inputFile;
    private List<string> designs = new List<string>();
    
    private HashSet<string> availableTowels = new HashSet<string>();
    
    public static void Main(string[] args)
    {
        string input;
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: day01 <input file>");
            Console.WriteLine("Using default input file: real-input.txt");
            input = DEFAULT_INPUT_FILE;
        }
        else
        {
            input = args[0];
        }
        
        Day19 day19 = new Day19(input);
        day19.part1();
        day19.part2();
    }

    public Day19(String inputFile)
    {
        this.inputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {

        TextFileReader.readFile(inputFile, parseLine);
        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {
        if (line.Contains(","))
        {
            availableTowels = availableTowels.Concat(line.Split(",").Select(str => str.Trim())).ToHashSet();
        } else if (line.Length > 0)
        {
            designs.Add(line);
        }
    }

    public void part1()
    {
        // sum all diffs
        int result = designs.Count(testDesign_withcombination);
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        long result = designs.Select(countPossibleCombinations).Aggregate(0, ((aggr, item) => aggr + item));
        Console.WriteLine("part 2 solution: " + result);
    }

    public int countPossibleCombinations(string design)
    {
        int count = 0;
        foreach (List<string> combination in GenerateTowelCombinationsLazy(design))
        {
            if (string.Join("", combination).Equals(design))
            {
                count++;
            }
        }
        
        Console.WriteLine($"design {design} can be made with {count} combinations");
        return count;
    }

    public bool testDesign_withcombination(string design)
    {
        
        foreach (List<string> combination in GenerateTowelCombinationsLazy(design))
        {
            if (string.Join("", combination).Equals(design))
            {
                return true;
            }
        }
        return false;
    }
    
    public IEnumerable<List<string>> GenerateTowelCombinationsLazy(string goal)
    {
        if (goal.Length == 0)
        {
            yield return new List<String>();
        }
        else
        {
            foreach (var element in availableTowels.Where(goal.StartsWith))
            {
                string remainingGoal = goal.Substring(element.Length);
                foreach (var subCombination in GenerateTowelCombinationsLazy(remainingGoal))
                {
                    subCombination.Insert(0, element);
                    yield return subCombination;
                }
            }
        }
    }
}


