using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using aocUtils;
using aocUtils.IO;

public class Day06
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const char OBSTACLE = '#';
    private const char VISITED_SPOT = 'X';
    
    private string inputFile;
    
    private List<string> lines = new List<string>();
    private char[,] mappedArea;
    private Coordinate2D currentPosition;
    private Direction currentDirection;
    private HashSet<Coordinate2D> visited = new HashSet<Coordinate2D>();
    private List<(Coordinate2D position, Direction direction)> StepHistory = new List<(Coordinate2D position, Direction direction)>();
    private Dictionary<Direction, HashSet<Coordinate2D>> visitedAxis = new Dictionary<Direction, HashSet<Coordinate2D>>();
    private int potentialLoopInstallment = 0;
    
    public static void Main(string[] args)
    {
        string input;
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: day06 <input file>");
            Console.WriteLine($"Using default input file: {DEFAULT_INPUT_FILE}");
            input = DEFAULT_INPUT_FILE;
        }
        else
        {
            input = args[0];
        }
        
        DateTime startTime = DateTime.Now;
        
        Day06 day06 = new Day06(input);
        day06.play();
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
        int result = visited.Count;
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        
        Console.WriteLine($"part 2 solution: {potentialLoopInstallment}");
    }

    public void play()
    {
        visited.Add(currentPosition);
        StepHistory.Add((currentPosition, currentDirection));
        
        while (!isGoingOutOfArea(currentPosition, currentDirection))
        {
            Coordinate2D nextStep = currentPosition + currentDirection.getVector();

            if (mappedArea[nextStep.getX(), nextStep.getY()] == OBSTACLE)
            {
                fillCurrentAxis(currentPosition, currentDirection);
                
                currentDirection = Direction.rotate90DegreeClockwise(currentDirection);
            }
            else
            {
                visited.Add(nextStep);
                mappedArea[nextStep.getX(), nextStep.getY()] = currentDirection.getCharacter();
                mappedArea[currentPosition.getX(), currentPosition.getY()] = VISITED_SPOT;

                if (visitedAxis.ContainsKey(Direction.rotate90DegreeClockwise(currentDirection)) 
                    && visitedAxis[Direction.rotate90DegreeClockwise(currentDirection)].Contains(nextStep))
                {
                    potentialLoopInstallment++;
                }
                
                currentPosition = nextStep;
                
                StepHistory.Add((currentPosition, currentDirection));
            }
        }
    }

    private char[,] initTable()
    {
        return lines
            .Select(s => s.ToCharArray())
            .ToArray()
            .To2D();
    }

    private void fillCurrentAxis(Coordinate2D position, Direction direction)
    {
        if (!visitedAxis.ContainsKey(direction))
        {
            visitedAxis[direction] = new HashSet<Coordinate2D>(){position};
        }

        Direction reverseDirection = Direction.rotate180Degrees(direction);
        Coordinate2D reverseVector = reverseDirection.getVector();
        Coordinate2D visitedAxisPos = position + reverseVector;
        while (
            mappedArea[visitedAxisPos.getX(), visitedAxisPos.getY()] != OBSTACLE &&
            !isGoingOutOfArea(visitedAxisPos, reverseDirection))
        {
            visitedAxis[direction].Add(visitedAxisPos);
            visitedAxisPos += reverseVector;
        }
    }

    private bool isGoingOutOfArea(Coordinate2D position, Direction direction)
    {
        if(position.getX() < 0|| position.getY() < 0 ||  position.getX() >= mappedArea.GetLength(0)|| position.getY() >= mappedArea.GetLength(1) )
            throw new ArgumentException($"invalid coordinate: {position}");

        
        return position.getX() == 0 && direction == Direction.UP
            || position.getX() == mappedArea.GetLength(0)-1 && direction == Direction.DOWN
            || position.getY() == 0 && direction == Direction.LEFT
            || position.getY() == mappedArea.GetLength(1)-1 && direction == Direction.RIGHT
            ;
    }
}