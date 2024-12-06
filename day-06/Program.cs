using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using aocUtils;
using aocUtils.IO;
using day_06;

public class Day06
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const char OBSTACLE = '#';
    private const char VISITED_SPOT = 'X';
    
    private string inputFile;
    
    private List<string> lines = new List<string>();
    private char[,] mappedArea;
    private Coordinate2D currentPosition;
    private DirectionHelper.Direction currentDirection;
    private HashSet<Coordinate2D> visited = new HashSet<Coordinate2D>();
    
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
        
        Day06 day06 = new Day06(input);
        day06.part1();
        day06.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day06(String inputFile)
    {
        this.inputFile = inputFile;
        parseInput();
        mappedArea = initTable();
    }

    private void parseInput()
    {
        TextFileReader.readFile(inputFile, parseLine);
        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {
        char[] startingPoint = line.Where(Direction.isDirectionChar).ToArray();
        
        if (startingPoint.Length > 1)
        {
            throw new Exception($"Invalid input: {line}");
        } 
        
        if (startingPoint.Length == 1)
        {
            currentPosition = new Coordinate2D(lines.Count, line.IndexOf(startingPoint[0]));
            currentDirection = Direction.GetDirection(startingPoint[0]);
        }
        
        lines.Add(line);
        
    }

    public void part1()
    {
        visited.Add(currentPosition);
        while (!isGoingOutOfArea(currentPosition))
        {
            Coordinate2D nextStep = currentPosition + currentDirection.getVector();

            if (mappedArea[nextStep.getX(), nextStep.getY()] == OBSTACLE)
            {
                // rotate 90 deg
            }
            else
            {
                visited.Add(nextStep);
                mappedArea[nextStep.getX(), nextStep.getY()] = currentDirection.getCharacter();
                mappedArea[currentDirection.getX(), currentDirection.getY()] = VISITED_SPOT;
                
                currentPosition = nextStep;
            }
        }
        int result = 0;
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        int result = 0;
        Console.WriteLine($"part 2 solution: {result}");
    }

    private char[,] initTable()
    {
        return lines
            .Select(s => s.ToCharArray())
            .ToArray()
            .To2D();
    }
    
    

    private bool isGoingOutOfArea()
    {
        if(currentPosition.getX() < 0|| currentPosition.getY() < 0 ||  currentPosition.getX() >= mappedArea.GetLength(0)|| currentPosition.getY() >= mappedArea.GetLength(1) )
            throw new ArgumentException($"invalid coordinate: {currentPosition}");

        
        return currentPosition.getX() == 0 && currentDirection == Direction.UP
            || currentPosition.getX() == mappedArea.GetLength(0)-1 && currentDirection == Direction.DOWN
            || currentPosition.getY() == 0 && currentDirection == Direction.LEFT
            || currentPosition.getY() == mappedArea.GetLength(1)-1 && currentDirection == Direction.RIGHT
            ;
    }
}