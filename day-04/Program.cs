using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using aocUtils.IO;

public class Day04
{
    private const string DEFAULT_INPUT_FILE = "./inputs/example.txt";
    private const string WANTED_WORD = "XMAS";
    
    private string inputFile;
    
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
        
        Day04 day04 = new Day04(input);
        day04.part1();
        day04.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day04(String inputFile)
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
        Console.WriteLine(line);
        line.in
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
}