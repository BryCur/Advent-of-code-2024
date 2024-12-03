using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using aocUtils.IO;

public class Day03
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const string MUL_PATTERN = @"mul\([0-9]{1,3},[0-9]{1,3}\)|do\(\)|don't\(\)";
    private const string DISABLE_FLAG = "don't()";
    private const string ENABLE_FLAG = "do()";
    
    private string inputFile;
    private List<string> mulMatches = new List<string>();
    
    public static void Main(string[] args)
    {
        string input;
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: day02 <input file>");
            Console.WriteLine($"Using default input file: {DEFAULT_INPUT_FILE}");
            input = DEFAULT_INPUT_FILE;
        }
        else
        {
            input = args[0];
        }
        
        DateTime startTime = DateTime.Now;
        
        Day03 day03 = new Day03(input);
        day03.part1();
        day03.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day03(String inputFile)
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
        // split by space
        Regex matcher = new Regex(MUL_PATTERN);
        MatchCollection matches = matcher.Matches(line);
        foreach (Match match in matches)
        {
            mulMatches.Add(match.Value);
        }
    }

    public void part1()
    {
        int result = mulMatches.Where(match => match.Contains("mul")).ToList().ConvertAll(processMul).Sum();
        Console.WriteLine($"#of matches: {mulMatches.Count}"); 
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        int result = interpretMatches(mulMatches);
        Console.WriteLine($"part 2 solution: {result}");
    }

    public int interpretMatches(List<string> instructions)
    {
        int result = 0;
        bool enabled = true;

        foreach (string instruction in instructions)
        {
            if (instruction.Equals(DISABLE_FLAG))
            {
                enabled = false;
            } else if (instruction.Equals(ENABLE_FLAG))
            {
                enabled = true;
            } else if (enabled)
            {
                result += processMul(instruction);
            }
        }

        return result;

    }

    public int processMul(string input)
    {
        int start = input.IndexOf("(") +1;
        int length = input.IndexOf(")") - start;
        string substr = input.Substring(start, length);
        string[] splitted = substr.Split(",");
        int[] parsedSplitted = Array.ConvertAll(splitted, int.Parse);

        if (parsedSplitted.Length != 2)
        {
            throw new ArgumentException("input does not follow mul(int,int) format");
        }
        
        return parsedSplitted[0] * parsedSplitted[1];
    }
}