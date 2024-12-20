using aocUtils;
using aocUtils.IO;
using day_14.inputs;

public class Day14
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const int MAX_HEIGHT = 103;
    private const int MAX_WIDTH = 101;
    
    private string InputFile;
    
    private List<Robot> Robots = new List<Robot>();
    
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
        
        Day14 day14 = new Day14(input);
        
        day14.part1();
        day14.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day14(String inputFile)
    {
        this.InputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {
        TextFileReader.readFile(InputFile, parseLine);
        // parse the last machine bc the file finishes on a "prize" line
        
        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {
        Robots.Add(ParseLineToRobot(line));
    }

    private static Robot ParseLineToRobot(string line)
    {
        String[]splitted = line.Split(" ");
        string[] splittedPos = splitted[0].Trim().Substring(2).Split(",");
        string[] splittedVel = splitted[1].Trim().Substring(2).Split(",");
        
        Coordinate2D postition = new Coordinate2D(int.Parse(splittedPos[0]), int.Parse(splittedPos[1]));
        Coordinate2D velocity = new Coordinate2D(int.Parse(splittedVel[0]), int.Parse(splittedVel[1]));
        return new Robot(postition, velocity);
    }


    public void part1()
    {
        int seconds = 100;
        List<Coordinate2D> newPos = Robots.Select(r => { return getCoordinateWithinGrid(r.ComputePostitionAfterSeconds(seconds)); }).ToList();
        
        long q1 = newPos.Count(np => np.getX() < (int)Math.Floor(MAX_WIDTH/2d) && np.getY() < (int)Math.Floor(MAX_HEIGHT/2d));
        long q2 = newPos.Count(np => np.getX() > (int)Math.Floor(MAX_WIDTH/2d) && np.getY() < (int)Math.Floor(MAX_HEIGHT/2d));
        long q3 = newPos.Count(np => np.getX() < (int)Math.Floor(MAX_WIDTH/2d) && np.getY() > (int)Math.Floor(MAX_HEIGHT/2d));
        long q4 = newPos.Count(np => np.getX() > (int)Math.Floor(MAX_WIDTH/2d) && np.getY() > (int)Math.Floor(MAX_HEIGHT/2d));
        long result = q1 * q2 * q3 * q4;
            //.Count(coordinate2D => coordinate2D.getX() != (int)Math.Floor(MAX_HEIGHT/2d) || coordinate2D.getY() != (int)Math.Floor(MAX_HEIGHT/2d));       
        Console.WriteLine($"part 1 solution: {result}");
    }

    protected Coordinate2D getCoordinateWithinGrid(Coordinate2D coordinate)
    {
        int seconds;
        (int x, int y) = coordinate.getCoordinates();
            
        x %= MAX_WIDTH;
        y %= MAX_HEIGHT;

        if (x < 0) {x += MAX_WIDTH;}
        if (y < 0) {y += MAX_HEIGHT;}

        return new Coordinate2D(x, y);
    }

    public void part2()
    {
        bool continueLoop = true;
        // Set a variable to the Documents path.
        string docPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        int step = 103;
        int journey = 1000;
        int start = 1631;
        int end = start + step*journey;

        // Write the string array to a new file named "WriteLines.txt".
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"day14_{start}_{end}.txt")))
        {
            for (int i = 0; i < journey; i++)
            {
                List<Coordinate2D> newPos = Robots
                    .Select(r => r.ComputePostitionAfterSeconds(start+ i*step))
                    .Select(getCoordinateWithinGrid)
                    .ToList();
                renderPositions(newPos, outputFile);
            
                outputFile.WriteLine($"Does that look like a tree ({start+ i*step} seconds)? ");
            }
        }
        
        Console.WriteLine($"part 2 solution, check: {docPath}"); // right answer is 7502
    }

    public void renderPositions(List<Coordinate2D> positions, StreamWriter outputFile)
    {
        bool[,] render = new bool[MAX_HEIGHT, MAX_WIDTH];

        foreach (Coordinate2D pos in positions)
        {
            render[pos.getY(), pos.getX()] = true;
        }

        for (int i = 0; i < render.GetLength(0); i++)
        {
            String line = "";
            for (int j = 0; j < render.GetLength(1); j++)
            {
                line += render[i, j] ? '#' : '.';
            }
            outputFile.WriteLine(line);
        }
    }
}


