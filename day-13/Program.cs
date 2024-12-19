using aocUtils;
using aocUtils.IO;
using day_13;

public class Day13
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    
    private string InputFile;
    
    private List<string> Input = new List<string>();
    private List<ClawMachine> ClawMachines = new List<ClawMachine>();

    private Coordinate2D currentAButton;
    private Coordinate2D currentBButton;
    private Coordinate2D currentTarget;
    
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
        
        Day13 day13 = new Day13(input);
        
        day13.part1();
        day13.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day13(String inputFile)
    {
        this.InputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {
        TextFileReader.readFile(InputFile, parseLine);
        // parse the last machine bc the file finishes on a "prize" line
        ClawMachines.Add(new ClawMachine(currentAButton, currentBButton, currentTarget));
        
        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {
        if (line.Contains("Button A:"))
        {
            currentAButton = parseCoordinate(line);
        } else if (line.Contains("Button B:"))
        {
            currentBButton = parseCoordinate(line);
        } else if (line.Contains("Prize:"))
        {
            currentTarget = parseCoordinate(line);
        }
        else
        {
            ClawMachines.Add(new ClawMachine(currentAButton, currentBButton, currentTarget));
        }
        
        Input.Add(line);
    }

    private Coordinate2D parseCoordinate(string line)
    {
        string splitted =  line.Split(":")[1].Trim();
        string[] coordinates = splitted.Split(",");

        int x = int.Parse(coordinates[0].Trim().Substring(2));
        int y = int.Parse(coordinates[1].Trim().Substring(2));

        return new Coordinate2D(x, y);
    }

    public void part1()
    {
        long result = ClawMachines
            .Where(c => c.IsTargetAccessible())
            .Aggregate(0L, (i, machine) => i += (long)machine.GetClickA()*3L + (long)machine.GetClickB());
        
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        long offset = 10000000000000; // 10'000'000'000'000 => 10 trillion (en) / 10 billion (fr)

        decimal result = ClawMachines
            .Select(c => c.GetSolutionWithOffset(offset))
            .Where(((decimal aCoins, decimal bCoins) t) => decimal.IsInteger(t.aCoins) && decimal.IsInteger(t.bCoins))
            .Aggregate(0L, (decimal i, (decimal aCoins, decimal bCoins) t) => i += (long)t.aCoins*3L + (long)t.bCoins);

        Console.WriteLine($"part 2 solution: {result}"); 
    }
}


