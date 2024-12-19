using aocUtils.IO;
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
                Day12Node node = new Day12Node(Input[i][j], i, j);
                Grid[i, j] = node;
                
                if(j > 0) { Grid[i,j].AddNeighbor(Grid[i,j-1]); }
                if(i > 0) { Grid[i,j].AddNeighbor(Grid[i-1,j]); }
            }
        }
        
        for(int i=0;i<Grid.GetLength(0);i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
               discoverGarden(Grid[i, j]);
            }
        }
        
        Console.WriteLine("input processed");
    }

    public void discoverGarden(Day12Node startNode)
    {
        if (startNode.getGarden() == null)
        {
            GardenGroup garden = new GardenGroup(startNode.GetValue());
            garden.addTile(startNode);
            startNode.setGarden(garden);
            GardenGroups.Add(garden);
        }
        
        foreach (Day12Node neighbour in startNode.GetAdjacentNodes())
        {
            discoverGarden(neighbour, startNode.getGarden());   
        }
    }
    public void discoverGarden(Day12Node startNode, GardenGroup gardenGroup)
    {
        if (startNode.getGarden() != null)
        {
            return;
        }
        
        startNode.setGarden(gardenGroup);
        gardenGroup.addTile(startNode);

        foreach (Day12Node neighbour in startNode.GetAdjacentNodes())
        {
            discoverGarden(neighbour, startNode.getGarden());   
        }
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
        long result = GardenGroups.Select(group => group.getArea() * group.getPerimeter()).Aggregate(0L, (x, y) => x + y);
        
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        int result = 0;

        Console.WriteLine($"part 2 solution: {result}");
    }
}


