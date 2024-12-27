using System.Runtime.InteropServices.JavaScript;
using aocUtils;
using aocUtils.IO;
using day_15;

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
    private Coordinate2D initialRobotPosition;
    
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
                    initialRobotPosition = new Coordinate2D(i, j);
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
        List<Box> boxes = getAllBoxCoordinatesForScaledUpMap().Select(c => new Box(c)).ToList();
        List<Coordinate2D> walls = getAllWallsCoordinatesForScaledUpMap();
        
        Coordinate2D currentRobotPosition = new Coordinate2D(initialRobotPosition.getX(), initialRobotPosition.getY()*2);
        string docPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"day-15-output.txt")))
        {
            foreach (Direction direction in Directions)
            {
                renderScaledMapPositions(walls, boxes, currentRobotPosition, outputFile);
                outputFile.WriteLine("-------------------------------------------");
                outputFile.WriteLine($"direction: {direction.getCharacter()}");
                bool hasSpacetoMoveForward = hasSpaceToMoveInScaledUpMap(boxes, walls, currentRobotPosition, direction);
                if (!hasSpacetoMoveForward)
                {
                    continue;
                }
            
                Coordinate2D nextStep = currentRobotPosition + direction.getVector();
                if (boxes.Any(b => b.isPositionInBox(nextStep)))
                {
                    getAllContiguousBoxInDirection(boxes, currentRobotPosition, direction).ForEach(b => b.shiftBox(direction));
                }
            
                currentRobotPosition = nextStep;
            }
            renderScaledMapPositions(walls, boxes, currentRobotPosition, outputFile);

        }

        

        long result = boxes.Select(b => b.getReference())
            .Select(boxCoord => boxCoord.getX() * 100L + boxCoord.getY()).Sum();  
        
        Console.WriteLine($"part 2 solution, check: {result}"); // right answer is 7502
    }

    private void playDirectives()
    {
        Coordinate2D currentRobotPosition = initialRobotPosition;
        foreach (Direction direction in Directions)
        {
            //renderPositions();
            Coordinate2D? nextEmptySpot = getNextEmptySpot(currentRobotPosition, direction);
            if (nextEmptySpot is null) {continue;}
            
            Coordinate2D nextStep = currentRobotPosition + direction.getVector();
            if (!isNextStepFree(nextStep))
            {
                moveTo(nextStep, nextEmptySpot);
            }
            
            moveTo(currentRobotPosition, nextStep);
            currentRobotPosition = nextStep;
        }
        renderPositions();
    }

    private bool isNextStepFree(Coordinate2D nextStep)
    {
        return mapGrid[nextStep.getX(), nextStep.getY()] == EMPTY_SYMBOL;
    }
    
    private bool isNextStepFreeInScaledUpMap(Coordinate2D nextStep, List<Box> allBoxes, List<Coordinate2D> allWalls)
    {
        return !allBoxes.Any(b => b.isPositionInBox(nextStep)) && !allWalls.Contains(nextStep);
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
    
    
    public void renderScaledMapPositions(List<Coordinate2D> walls, List<Box> boxes, Coordinate2D currentRobotPosition, StreamWriter outputFile)
    {
        
        for (int i = 0; i < mapGrid.GetLength(0); i++)
        {
            String line = "";
            for (int j = 0; j < mapGrid.GetLength(1) * 2; j++)
            {
                if (walls.Contains(new Coordinate2D(i, j)))
                {
                    line += WALL_SYMBOL;
                } else if (boxes.Any(b => b.isPositionInBox(new Coordinate2D(i, j)) && boxes.Any(b => b.getReference() == new Coordinate2D(i, j))))
                {
                    line += '[';
                } else if (boxes.Any(b => b.isPositionInBox(new Coordinate2D(i, j))))
                {
                    line += ']';
                } else if (currentRobotPosition == new Coordinate2D(i, j))
                {
                    line += ROBOT_SYMBOL;
                }
                else
                {
                    line += '.';
                }
            }
            outputFile.WriteLine(line);
        }
    }
    private char[,] ScaleUpMapGrid()
    {
        char[,] scaledUpMap = new char[mapGrid.GetLength(0), mapGrid.GetLength(1)*2];
        
        for (int i = 0; i < Input.Count; i++)
        {
            for (int j = 0; j < Input[i].Length*2; j += 2)
            {
                scaledUpMap[i, j] = Input[i][j];
                if (Input[i][j] == ROBOT_SYMBOL)
                {
                    initialRobotPosition = new Coordinate2D(i, j);
                }
                else
                {
                    scaledUpMap[i,j+1] = Input[i][j];
                }
            }
        }
        
        return scaledUpMap;
    }

    private List<Coordinate2D> getAllBoxCoordinatesForScaledUpMap()
    {
        List<Coordinate2D> coordinates = new List<Coordinate2D>();

        for (int i = 0; i < Input.Count; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                if(Input[i][j] == BOX_SYMBOL)
                {
                    coordinates.Add(new Coordinate2D(i, j * 2));
                }
            }
        }
        
        return coordinates;
    }
    
    private List<Coordinate2D> getAllWallsCoordinatesForScaledUpMap()
    {
        List<Coordinate2D> coordinates = new List<Coordinate2D>();

        for (int i = 0; i < Input.Count; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                if(Input[i][j] == WALL_SYMBOL)
                {
                    coordinates.Add(new Coordinate2D(i, j * 2));
                    coordinates.Add(new Coordinate2D(i, j * 2+1));
                }
            }
        }
        
        return coordinates;
    }

    public List<Box> getAllContiguousBoxInDirection(List<Box> allBoxes, Coordinate2D pos, Direction direction)
    {
        List<Box> contiguousBoxes = new List<Box>();
        List<Coordinate2D> checkingPos = new List<Coordinate2D> () { pos + direction.getVector() };
        while (allBoxes.Any(b => b.isBoxInPosList(checkingPos)))
        {
            List<Box> affectedBoxes = allBoxes.Where(b => b.isBoxInPosList(checkingPos)).ToList();
            contiguousBoxes.AddRange(affectedBoxes);
            Coordinate2D shiftingVector = direction.getVector();
            checkingPos = affectedBoxes
                .SelectMany(box => box.getCoveredBoxCoordinates())
                .Select(c => c += shiftingVector)
                .Except(affectedBoxes.SelectMany(b => b.getCoveredBoxCoordinates()))
                .ToList();
        }
        
        return contiguousBoxes;
    }

    private bool hasSpaceToMoveInScaledUpMap(List<Box> allBoxes, List<Coordinate2D> allWalls, Coordinate2D currentPos, Direction direction)
    {
        // we go forward until we meet a free spot
        // a free spot is a spot that is neither a wall nor a box
        List<Coordinate2D> checkingPos = new List<Coordinate2D> () { currentPos + direction.getVector() };
        List<Box> countedBoxes = new List<Box>();

        while (!allWalls.Intersect(checkingPos).Any())
        {
            if (!allBoxes.Any(b => b.isBoxInPosList(checkingPos)))
            {
                return true;
            }
            List<Box> affectedBoxes = allBoxes.Where(b => b.isBoxInPosList(checkingPos)).ToList();
            countedBoxes.AddRange(affectedBoxes);
            Coordinate2D shiftingVector = direction.getVector(); // * (Direction.HORIZONTAL_DIRECTIONS.Contains(direction) ? 2 : 1);
            checkingPos = affectedBoxes
                .SelectMany(box => box.getCoveredBoxCoordinates())
                .Select(c => c += shiftingVector)
                .Except(countedBoxes.SelectMany(b => b.getCoveredBoxCoordinates()))
                .ToList();
        }
        
        return false;
    }
}

