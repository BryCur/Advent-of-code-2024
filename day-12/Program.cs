using System.IO;
using System.Runtime.CompilerServices;
using aocUtils;
using aocUtils.IO;
using day_10;
using day_12;

public class Day12
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    
    private string InputFile;
    
    private List<string> Input = new List<string>();
    private Day12Node[,] Grid;
    private List<GardenGroup> GardenGroups = new List<GardenGroup>();
    private int? LineLength;
    
    public static void Main(string[] args)
    {
        string input;
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: dayXX <input file>");
            Console.WriteLine($"Using default input file: {DEFAULT_INPUT_FILE}");
            input = DEFAULT_INPUT_FILE;
        }
        else
        {
            input = args[0];
        }
        
        DateTime startTime = DateTime.Now;
        
        Day12 day12 = new Day12(input);
        
        day12.part1();
        day12.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day12(String inputFile)
    {
        this.InputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {
        TextFileReader.readFile(InputFile, parseLine);
        
        Grid = new Day12Node[Input.Count,Input[0].Length];
        for(int i=0;i<Input.Count;i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                Grid[i, j] = new Day12Node(Input[i][j], i, j);
                
                if(j > 0) { Grid[i,j].AddNeighbor(Grid[i,j-1]); }
                if(i > 0) { Grid[i,j].AddNeighbor(Grid[i-1,j]); }
            }
        }
        
        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {
        if (!LineLength.HasValue)
        {
            LineLength = line.Length;
        } else if (LineLength.Value != line.Length)
        {
            throw new InvalidDataException("Input file is invalid");
        } 
        
        
        Input.Add(line);
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


