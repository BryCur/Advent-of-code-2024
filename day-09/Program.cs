using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using aocUtils;
using aocUtils.IO;

public class Day09
{
    private const string DEFAULT_INPUT_FILE = "./inputs/example.txt";
    
    private string inputFile;
    
    private string input = "";
    
    public static void Main(string[] args)
    {
        string input;
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: day09 <input file>");
            Console.WriteLine($"Using default input file: {DEFAULT_INPUT_FILE}");
            input = DEFAULT_INPUT_FILE;
        }
        else
        {
            input = args[0];
        }
        
        DateTime startTime = DateTime.Now;
        
        Day09 day09 = new Day09(input);
        
        day09.part1();
        day09.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
    }

    public Day09(String inputFile)
    {
        this.inputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {
        TextFileReader.readFile(inputFile, parseLine);
        string interpretedInput = interpretInput();
        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {
        input = line;
    }

    public void part1()
    {
        int result = 0;
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        int result = 0;
        Console.WriteLine($"part 2 solution: {result}");
    }

    private string interpretInput()
    {
        return ""
    }

}


