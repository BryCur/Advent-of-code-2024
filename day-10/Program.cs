using System.IO;
using System.Runtime.CompilerServices;
using aocUtils;
using aocUtils.IO;
using day_10;

public class Day10
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    
    private string InputFile;
    
    private List<string> Input = new List<string>();
    private Day10Node[,] Grid;
    private List<Day10Node> StartingPoints = new List<Day10Node>();
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
        
        Day10 day10 = new Day10(input);
        
        day10.part1();
        day10.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day10(String inputFile)
    {
        this.InputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {
        TextFileReader.readFile(InputFile, parseLine);
        
        Grid = new Day10Node[Input.Count,Input[0].Length];
        for(int i=0;i<Input.Count;i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                Grid[i, j] = new Day10Node(Input[i][j]-'0');
                
                if(j > 0) { Grid[i,j].AddNeighbor(Grid[i,j-1]); }
                if(i > 0) { Grid[i,j].AddNeighbor(Grid[i-1,j]); }
                
                if(Grid[i,j].GetValue() == 0) { StartingPoints.Add(Grid[i,j]); }
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
        HashSet<Day10Node> visitMap = new HashSet<Day10Node>();
        foreach (Day10Node node in StartingPoints)
        {
            visitMap.Clear();
            getTrailScore(node, visitMap);
            result += visitMap.Count(n => n.GetValue() == 9);
        }
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        int result = 0;
        HashSet<Day10Node> visitMap = new HashSet<Day10Node>();
        foreach (Day10Node node in StartingPoints)
        {
            result += getTrailScore(node, visitMap);
        }
        Console.WriteLine($"part 2 solution: {result}");
    }

    private int getTrailScore(Day10Node node, HashSet<Day10Node> visited)
    {
        
        visited.Add(node);
        
        
        if (node.GetValue() == 9)
        {
            return 1;
        }

        int result = 0;
        foreach (var nextNode in node.GetNextNodes())
        {
            result += getTrailScore(nextNode, visited);
        }
        return result;
        
    }
}


