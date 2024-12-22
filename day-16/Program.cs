using System.Runtime.InteropServices.JavaScript;
using aocUtils;
using aocUtils.IO;
using day_16;

public class Day16
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const char WALL_SYMBOL = '#';
    private const char EMPTY_SYMBOL = '.';
    
    private string InputFile;
    private List<String> Input = new List<String>();
    private Day16Node[,] mapGrid;
    private Day16Node deerPosition;
    private Coordinate2D exitPosition;
    
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
        
        Day16 day16 = new Day16(input);
        
        day16.part1();
        day16.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day16(String inputFile)
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

        Input.Add(line);
        if (Input[0].Length != line.Length)
        {
            throw new Exception("Invalid input");
        }
        
    }


    private void initMapGrid()
    {
        mapGrid = new Day16Node[Input.Count, Input[0].Length];
        
        for (int i = 0; i < Input.Count; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                mapGrid[i, j] = new Day16Node(i, j, Input[i][j] == WALL_SYMBOL);
                
                if(j > 0) { mapGrid[i,j].AddNeighbor(mapGrid[i,j-1]); }
                if(i > 0) { mapGrid[i,j].AddNeighbor(mapGrid[i-1,j]); }
                
                if (Input[i][j] == 'S')
                {
                    deerPosition = mapGrid[i, j];
                }
                if (Input[i][j] == 'E')
                {
                    exitPosition = new Coordinate2D(i, j);
                }
            }
        }
    }
    public void part1()
    {
        List<Day16Node> history = new List<Day16Node>();
        long result = findLightestPath(deerPosition, Direction.RIGHT, history, 0, long.MaxValue);
        
        
        Console.WriteLine($"part 1 solution: {result}");
    }

    public long findLightestPath(Day16Node currentNode, Direction currentDirection, List<Day16Node> path, long currentWeigth, long currentMin)
    {
        path.Add(currentNode);
        //renderPositions(path);
        if (currentNode.GetValue() == exitPosition)
        { // exit found, return the current weight
            path.RemoveAt(path.Count - 1);
            return currentWeigth;
        }
        
        if (path.Count > 1 && currentNode.GetAdjacentNodes().Count == 1)
        { // no path forward
            path.RemoveAt(path.Count - 1);
            return long.MaxValue;
        }
        
        long returnPath = long.MaxValue;
        foreach (Day16Node neighbor in currentNode.GetAdjacentNodes())
        {
            if(path.Contains(neighbor)) { continue; }

            Direction nextStepDirection =
                Direction.GetDirectionFromVector(neighbor.GetValue() - currentNode.GetValue());
            
            long nextweigth = currentWeigth + 1 + (nextStepDirection == currentDirection ? 0 : 1000);
            long possiblePath = findLightestPath(neighbor, nextStepDirection, path, nextweigth);
            returnPath = Math.Min(returnPath, possiblePath);
        }
        
        path.RemoveAt(path.Count - 1);
        return returnPath;
    }
    public void part2()
    {
        long result = 0;
        
        Console.WriteLine($"part 2 solution, check: {result}"); // right answer is 7502
    }

    public void renderPositions(List<Day16Node> path)
    {
        HashSet<Coordinate2D> pathSet = new HashSet<Coordinate2D>(path.Select(p => p.GetValue()));
        for (int i = 0; i < mapGrid.GetLength(0); i++)
        {
            String line = "";
            for (int j = 0; j < mapGrid.GetLength(1); j++)
            {
                line += mapGrid[i, j].getIsWall() ? WALL_SYMBOL : pathSet.Contains(new Coordinate2D(i, j)) ? 'X' : EMPTY_SYMBOL;
            }
            Console.WriteLine(line);
        }
        Console.WriteLine("---------------");
    }
}


