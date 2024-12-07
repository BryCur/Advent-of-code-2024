using System.IO;
using System.Runtime.CompilerServices;
using aocUtils.IO;

public class Day07
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    
    private string inputFile;
    private Dictionary<long, List<long>> equations = new Dictionary<long, List<long>>();
    private long part1Result;
    
    static private Func<long, long, long> addition = (long a, long b) => a + b;
    static private Func<long, long, long> multiplication = (long a, long b) => a * b;
    static private Func<long, long, long> concatenation = (long a, long b) => long.Parse(String.Join("", a.ToString(), b.ToString()));
    private List<Func<long, long, long>> operations = new List<Func<long, long, long>>() {addition, multiplication, concatenation};
    
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
        
        Day07 day07 = new Day07(input);
        day07.part1();
        day07.part2();
    }

    public Day07(String inputFile)
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
        string[] firstSplit = line.Split(':');
        long result = long.Parse(firstSplit[0]);
        List<long> numbers = Array.ConvertAll(firstSplit[1].Split(new char[] {' ','\t'}, StringSplitOptions.RemoveEmptyEntries), long.Parse).ToList();
        
        
        var opCombinationsIterator = CombinationGenerator.GenerateCombinationsLazy(operations, numbers.Count - 1);
        foreach (var opCombination in opCombinationsIterator)
        {
            if (result == getResult(numbers, opCombination))
            {
                part1Result += result;
                break;
            }
        }
    }

    public void part1()
    {
        // sum all diffs
        Console.WriteLine($"part 1 solution: {part1Result}");
    }
    
    public void part2()
    {
        long result = 0;
        Console.WriteLine("part 2 solution: " + result);
    }

    public long getResult(List<long> numbers, List<Func<long, long, long>> operations)
    {
        if (numbers.Count != operations.Count + 1)
        {
            throw new Exception("Operation list should have one less item than the number list");
        }
        
        long accumulator = numbers[0];
        for (int i = 0; i < operations.Count; i++)
        {
            accumulator = operations[i](accumulator, numbers[i+1]);
        }
        
        return accumulator;
    }
}


