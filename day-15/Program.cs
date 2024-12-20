using System.Runtime.InteropServices.JavaScript;
using aocUtils;
using aocUtils.IO;
public class Day15
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const char ROBOT_SYMBOL = '@';
    private const char BOX_SYMBOL = 'O';
    private const char WALL_SYMBOL = '#';
    private const char EMPTY_SYMBOL = '.';
    
    private string InputFile;
    private List<String> Input = new List<String>();
    private List<Direction> Directions = new List<Direction>();
    private bool parsingMap = true;
    private char[,] mapGrid;
    private Coordinate2D robotPosition;
    
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
        
        Day15 day15 = new Day15(input);
        
        day15.part1();
        day15.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day15(String inputFile)
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
        if (line.Length < 1 && parsingMap)
        {
            parsingMap = false;
            return;
        }
        
        if (parsingMap)
        {
            Input.Add(line);
            if (Input[0].Length != line.Length)
            {
                throw new Exception("Invalid input");
            }
        }
        else
        {
            foreach (char c in line)
            {
                Directions.Add(Direction.GetDirection(c));
            }
        }
    }


    private void initMapGrid()
    {
        mapGrid = new char[Input.Count, Input[0].Length];
        
        for (int i = 0; i < Input.Count; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                mapGrid[i, j] = Input[i][j];
                if (Input[i][j] == ROBOT_SYMBOL)
                {
                    robotPosition = new Coordinate2D(i, j);
                }
            }
        }
    }
    public void part1()
    {
        playDirectives();
        long result = 0;
        foreach (Coordinate2D boxCoord in getAllBoxCoordinates())
        {
            result += boxCoord.getX() * 100 + boxCoord.getY();
        }
        
        Console.WriteLine($"part 1 solution: {result}");
    }
    public void part2()
    {
        long result = 0;
        
        Console.WriteLine($"part 2 solution, check: {result}"); // right answer is 7502
    }

    private void playDirectives()
    {
        foreach (Direction direction in Directions)
        {
            //renderPositions();
            Coordinate2D? nextEmptySpot = getNextEmptySpot(robotPosition, direction);
            if (nextEmptySpot is null) {continue;}
            
            Coordinate2D nextStep = robotPosition + direction.getVector();
            if (!isNextStepFree(nextStep))
            {
                moveTo(nextStep, nextEmptySpot);
            }
            
            moveTo(robotPosition, nextStep);
            robotPosition = nextStep;
        }
        renderPositions();
    }

    private bool isNextStepFree(Coordinate2D nextStep)
    {
        return mapGrid[nextStep.getX(), nextStep.getY()] == EMPTY_SYMBOL;
    }

    private Coordinate2D? getNextEmptySpot(Coordinate2D startingPos, Direction direction)
    {
        Coordinate2D pos = new Coordinate2D(startingPos.getX(), startingPos.getY());
        while (mapGrid[pos.getX(), pos.getY()] != WALL_SYMBOL)
        {
            if (mapGrid[pos.getX(), pos.getY()] == EMPTY_SYMBOL)
            {
                return pos;
            }
            pos += direction.getVector();
        }
        
        return null;
    }

    private void moveTo(Coordinate2D from, Coordinate2D to)
    {
        char fromChar = mapGrid[from.getX(), from.getY()];

        mapGrid[to.getX(), to.getY()] = fromChar;
        mapGrid[from.getX(), from.getY()] = EMPTY_SYMBOL;
    }

    private List<Coordinate2D> getAllBoxCoordinates()
    {
        List<Coordinate2D> coordinates = new List<Coordinate2D>();
        for (int i = 0; i < mapGrid.GetLength(0); i++)
        {
            for (int j = 0; j < mapGrid.GetLength(1); j++)
            {
                if (mapGrid[i,j] == BOX_SYMBOL)
                {
                    coordinates.Add(new Coordinate2D(i, j));
                }
            }
        }
        
        return coordinates;
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


