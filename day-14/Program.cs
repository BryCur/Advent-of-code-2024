using aocUtils;
using aocUtils.IO;
using day_14.inputs;

public class Day14
{
    private const string DEFAULT_INPUT_FILE = "./inputs/example.txt";
    private const int MAX_HEIGHT = 7;
    private const int MAX_WIDTH = 11;
    
    private string InputFile;
    
    private List<string> Input = new List<string>();
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
        String[]splitted = line.Split(" ");
        string[] splittedPos = splitted[0].Trim().Substring(2).Split(",");
        string[] splittedVel = splitted[1].Trim().Substring(2).Split(",");
        
        Coordinate2D postition = new Coordinate2D(int.Parse(splittedPos[0]), int.Parse(splittedPos[1]));
        Coordinate2D velocity = new Coordinate2D(int.Parse(splittedVel[0]), int.Parse(splittedVel[1]));
        
        Robots.Add(new Robot(postition, velocity));
        Input.Add(line);
    }
    

    public void part1()
    {
        int seconds = 100;
        long result = Robots.Select(r =>
        {
            Coordinate2D movements = r.ComputePostitionAfterSeconds(seconds);
        });       
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        decimal result = 0;
        Console.WriteLine($"part 2 solution: {result}"); 
    }
}


