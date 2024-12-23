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
        int result = designs.Count(d => testDesign(d));
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        long result = 0;
        Console.WriteLine("part 2 solution: " + result);
    }

    public bool testDesign(string design)
    {
        int length = 1;
        string remainingDesign = design;
        while (length < remainingDesign.Length)
        {
            string tested = remainingDesign.Substring(0, length);

            if (availableTowels.Contains(tested))
            {
                remainingDesign = remainingDesign.Substring(length);
                length = 1;
            } else
            {
                length++;
            }
        }
        
        return remainingDesign.Length == 0;
    }
}


