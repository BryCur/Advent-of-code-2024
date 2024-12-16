using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using aocUtils;
using aocUtils.IO;
using day_11;

public class Day11
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const int MAX_BLINKS = 75;
    
    private string InputFile;
    
    private List<long> Input = new List<long>();
    private List<IRule> Rules = new List<IRule>() {new FirstRule(), new SecondRule(), new DefaultRule()};

    
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
        
        Day11 day11 = new Day11(input);
        
        day11.part1();
        day11.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day11(String inputFile)
    {
        this.InputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {
        TextFileReader.readFile(InputFile, parseLine);
        
        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {
        string[] splitted = line.Split(' ');
        foreach (var item in splitted)
        {
            Input.Add(long.Parse(item));
        }
    }

    public void part1()
    {
        
        List<long> result = new List<long>(Input);
        
        for (int i = 0; i < MAX_BLINKS; i++)
        {
            for (int j = 0; j < result.Count; j++)
            {
                IRule matchedRule = Rules.First(rule => rule.IsApplicable(result[j]));
                
                matchedRule.Apply(j, result);

                if (Rules.IndexOf(matchedRule) == 1)
                {
                    j++;
                }
            }
        }
        Console.WriteLine($"part 1 solution: {result.Count}");
    }
    
    public void part2()
    {
        int result = 0;

        Console.WriteLine($"part 2 solution: {result}");
    }
    
}


