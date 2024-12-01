using System.IO;
using System.Runtime.CompilerServices;
using aocUtils.IO;

public class Day01
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    
    private List<long> rawListLeft;
    private List<long> rawListRight;
    private string inputFile;
    
    
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
        
        Day01 day01 = new Day01(input);
        day01.part1();
        day01.part2();
    }

    public Day01(String inputFile)
    {
        this.inputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {
        rawListLeft = new List<long>();
        rawListRight = new List<long>();

        TextFileReader.readFile(inputFile, parseLine);
        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {
        string[] splitted = line.Split(new char[] {' ','\t'}, StringSplitOptions.RemoveEmptyEntries);

        rawListLeft.Add(long.Parse(splitted[0]));
        rawListRight.Add(long.Parse(splitted[1]));
    }

    public void part1()
    {
        List<long> left = new List<long>(rawListLeft);
        List<long> right = new List<long>(rawListRight);
        
        left.Sort();
        right.Sort();
        // absulute difference between items of both lists
        List<long> diffList = generateDifferenceList(left, right);
        
        // sum all diffs
        long difference = diffList.Sum();
        Console.WriteLine("part 1 solution: " + difference);
    }
    
    public void part2()
    {
        long result = 0;
        var map = new Dictionary<long, long>();
        // go through list 1 
        foreach (long l in rawListLeft)
        {
            if (!map.ContainsKey(l))
            {
                // count how many time l is in list2 
                long occurences = rawListRight.FindAll(x => x == l).Count;
                map.Add(l, occurences);
            }
            
            result += l * map[l];
        }
        Console.WriteLine("part 2 solution: " + result);
    }

    private List<long> generateDifferenceList(List<long> left, List<long> right)
    {
        if (left.Count != right.Count)
        {
            throw new ArgumentException("The lists do not have the same length.");
        }
        
        List<long> result = new List<long>();
        for (int i = 0; i < left.Count; i++)
        {
            result.Add(Math.Abs(left[i]-right[i]));
        }

        return result;
    }
}


