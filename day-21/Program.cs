using aocUtils;
using aocUtils.IO;
using day_21;

public class Day21
{
    private const string DEFAULT_INPUT_FILE = "./inputs/example.txt";

    private List<string> input = new List<string>();
    private string InputFile;

    private Coordinate2D currentNumericPadPos = NumericPad.KEY_A;
    private Coordinate2D currentDirectionalPadPos_robot2 = DirectionalPad.KEY_A;
    private Coordinate2D currentDirectionalPadPos_robot1 = DirectionalPad.KEY_A;
    private Coordinate2D currentDirectionalPadPos_human = DirectionalPad.KEY_A;
        
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
        
        Day21 day21 = new Day21(input);
        
        day21.part1();
        day21.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed} ms");
        
    }

    public Day21(String inputFile)
    {
        this.InputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {
        TextFileReader.readFile(InputFile, input.Add);
        // parse the last machine bc the file finishes on a "prize" line
        
        Console.WriteLine("input processed");
    }

    public void part1()
    {
        long result = 0;
        foreach (String line in input)
        {
            string instruction = getDirectionalInstructionForHuman(line);
            int numericValue = int.Parse(line.Substring(0, line.Length - 1));
            Console.WriteLine($"{line}: {instruction} ({instruction.Length}) = {numericValue * instruction.Length}");
            result += numericValue * instruction.Length;
        }
        
        Console.WriteLine($"part 1 solution: {result}");
    }

    public void part2()
    {
        long result = 0;
        Console.WriteLine($"part 2 solution, check: {result}"); 
    }

    private IEnumerable<string> getDirectionalInstructionForNumericPad(string goal)
    {
        string instructions = "";
        List<List<char>> baseLists = new List<List<char>>();
        foreach (char c in goal)
        {
            Coordinate2D nextButton = NumericPad.getKeyCoordinate(c);
            baseLists.Add(NumericPad.getMovementInstructionFromTo(currentNumericPadPos, nextButton).ToList());
            currentNumericPadPos = nextButton;
        }
        
        

        return instructions;
    }
    
    
    private string getDirectionalInstructionForRobot2(string goal)
    {
        string actual_goal = getDirectionalInstructionForNumericPad(goal);
        
        string instructions = "";
        foreach (char c in actual_goal)
        {
            Coordinate2D nextButton = DirectionalPad.getKeyCoordinate(c);
            instructions += DirectionalPad.getMovementInstructionFromTo(currentDirectionalPadPos_robot2, nextButton);
            currentDirectionalPadPos_robot2 = nextButton;
        }

        return instructions;
    }
    
        
    private string getDirectionalInstructionForRobot1(string goal)
    {
        string actual_goal = getDirectionalInstructionForRobot2(goal);
        
        string instructions = "";
        foreach (char c in actual_goal)
        {
            Coordinate2D nextButton = DirectionalPad.getKeyCoordinate(c);
            instructions += DirectionalPad.getMovementInstructionFromTo(currentDirectionalPadPos_robot1, nextButton);
            currentDirectionalPadPos_robot1 = nextButton;
        }

        return instructions;
    }
    
            
    private string getDirectionalInstructionForHuman(string goal)
    {
        string actual_goal = getDirectionalInstructionForRobot1(goal);
        
        string instructions = "";
        foreach (char c in actual_goal)
        {
            Coordinate2D nextButton = DirectionalPad.getKeyCoordinate(c);
            instructions += DirectionalPad.getMovementInstructionFromTo(currentDirectionalPadPos_human, nextButton);
            currentDirectionalPadPos_human = nextButton;
        }

        return instructions;
    }
}


