using System.Runtime.InteropServices.JavaScript;
using aocUtils;
using aocUtils.IO;
public class Day18
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const char WALL_SYMBOL = '#';
    private const int GRID_SIZE = 6;
    private Coordinate2D STARTING_POINT = new Coordinate2D(0, 0);
    private Coordinate2D ENDING_POINT = new Coordinate2D(GRID_SIZE-1, GRID_SIZE-1);
    
    
    private string InputFile;
    private List<Coordinate2D> Input = new List<Coordinate2D>();
    private Day18Node[,] mapGrid = new Day18Node[GRID_SIZE, GRID_SIZE];
    
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
        
        Day18 day18 = new Day18(input);
        
        day18.part1();
        day18.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day18(String inputFile)
    {
        this.InputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {
        TextFileReader.readFile(InputFile, parseLine);
        initMapGrid();
        
        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {
        string[] split = line.Split(",");
        Coordinate2D pos = new Coordinate2D(int.Parse(split[0]), int.Parse(split[1]));
        Input.Add(pos);
        
    }
    
    private void initMapGrid()
    {
        for (int i = 0; i < GRID_SIZE; i++)
        {
            for (int j = 0; j < GRID_SIZE; j++)
            {
                Coordinate2D pos = new Coordinate2D(i, j);
                mapGrid[i, j] = new Day18Node(i, j, Input.Contains(pos));
                
                if(j > 0) { mapGrid[i,j].AddNeighbor(mapGrid[i,j-1]); }
                if(i > 0) { mapGrid[i,j].AddNeighbor(mapGrid[i-1,j]); }
            }
        }
    }
    
    public void part1()
    {
        long result = 0;
        
        Console.WriteLine($"part 1 solution: {result}");
    }
    public void part2()
    {
        long result = 0;
        
        Console.WriteLine($"part 2 solution, check: {result}"); // right answer is 7502
    }

    public void renderPositions()
    {
        for (int i = 0; i < mapGrid.GetLength(0); i++)
        {
            String line = "";
            for (int j = 0; j < mapGrid.GetLength(1); j++)
            {
                line += mapGrid[i, j];
            }
            Console.WriteLine(line);
        }
    }
}