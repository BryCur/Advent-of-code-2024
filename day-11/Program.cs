using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using aocUtils;
using aocUtils.IO;
using day_11;

public class Day11
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const int MAX_BLINKS_PART1 = 25;
    private const int MAX_BLINKS_PART2 = 75;
    
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
        
        for (int i = 0; i < MAX_BLINKS_PART1; i++)
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
        // who cares about the list, since we don't care about ordering... Counting the stones are the important part
        // so a dictionary with the stone number as key, and the count of that number as value does the trick
        Dictionary<long, long> dictStones = new Dictionary<long, long>();
        foreach (long stone in Input)
        {
            if (!dictStones.ContainsKey(stone))
            {
                dictStones.Add(stone, 1);
            }
            else
            {
                dictStones[stone]++;
            }
        }

        for(int i = 0; i < MAX_BLINKS_PART2; i++)
        {
            Dictionary<long, long> nextState = new Dictionary<long, long>();

            foreach (KeyValuePair<long, long> kvp in dictStones)
            {
                long stoneNumber = kvp.Key;

                if (stoneNumber == 0)
                { // rule 1
                    AddToDictIfNotExist(nextState, 1, kvp.Value);
                } else if ((long)(Math.Log10(stoneNumber) + 1) % 2L == 0)
                { // rule 2
                    long digitCount = (long)(Math.Log10(stoneNumber) + 1);

                    long left = stoneNumber / (long)Math.Pow(10, digitCount/2);
                    long right = stoneNumber % (long)Math.Pow(10, digitCount/2);
                    
                    AddToDictIfNotExist(nextState, left, kvp.Value);
                    AddToDictIfNotExist(nextState, right, kvp.Value);

                }
                else
                { // rule 3
                    AddToDictIfNotExist(nextState, stoneNumber * 2024, kvp.Value);
                }
            }
            
            dictStones = nextState;
            
        }

        long result = dictStones.Values.Sum(); // sum all the values to have the count of stones

        Console.WriteLine($"part 2 solution: {result}");
    }

    private void AddToDictIfNotExist(Dictionary<long, long> dict, long key, long value)
    {
        if (!dict.ContainsKey(key))
        {
            dict.Add(key, value);
        }
        else
        {
            dict[key] += value;
        }
    }
    
}


