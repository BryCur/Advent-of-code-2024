using aocUtils;
using aocUtils.IO;

public class Day22
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";

    private List<long> input = new List<long>();
    private string InputFile;
        
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
        
        Day22 day22 = new Day22(input);
        
        day22.part1();
        day22.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed} ms");
        
    }

    public Day22(String inputFile)
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
        input.Add(long.Parse(line));
    }


    public void part1()
    {
        long result = 0;
        foreach (long initialValue in input)
        {
            long nextSecret = initialValue;
            for (int i = 0; i < 2000; i++)
            {
                nextSecret = getNextSecret(nextSecret);
            }
            result += nextSecret;
        }
        Console.WriteLine($"part 1 solution: {result}");
    }

    public void part2()
    {
        Queue<long>  changeHistory = new Queue<long>();
        Dictionary<string, List<int>> SequenceFirstAppearanceValues = new Dictionary<string, List<int>>();
        // Dictionary<int, HashSet<string>> SequenceFirstAppearancePerBuyer = new Dictionary<int, HashSet<string>>();
        for(int j=0; j<input.Count; j++)
        {
            long currentSecret = input[j]; // initialValue
            
            HashSet<string> sequencesForCurrentBuyer = new HashSet<string>();
            for (int i = 0; i < 2000; i++)
            {
                long nextSecret = getNextSecret(currentSecret);
                long diff = (nextSecret % 10) - (currentSecret % 10);
                
                changeHistory.Enqueue(diff);
                if (changeHistory.Count > 4)
                {
                    changeHistory.Dequeue();
                }

                if (changeHistory.Count == 4)
                {
                    string key = string.Join(",", changeHistory);

                    if (sequencesForCurrentBuyer.Add(key))
                    {
                        if (SequenceFirstAppearanceValues.ContainsKey(key))
                        {
                            
                            SequenceFirstAppearanceValues[key].Add((int)(nextSecret % 10));
                        }
                        else
                        {
                            SequenceFirstAppearanceValues[key] = new List<int>() { (int)(nextSecret % 10) };
                        }
                    }
                }
                
                currentSecret = nextSecret;
            }
        }
        
        long result = SequenceFirstAppearanceValues.Values.Select(list => list.Sum()).Max();
        Console.WriteLine($"part 2 solution, check: {result}"); 
        // NOT 4...
    }

    public long getNextSecret(long secret)
    {
        long intermediate = evolutionStep1(secret);
        intermediate= evolutionStep2(intermediate);
        return evolutionStep3(intermediate);
    }

    public long evolutionStep1(long secret)
    {
        long intermediate = secret * 64;
        intermediate = mixResult(intermediate, secret);
        return pruneSecret(intermediate);
    }

    public long evolutionStep2(long secret)
    {
        long intermediate = (long)Math.Floor(secret / 32d);
        intermediate = mixResult(intermediate, secret);
        return pruneSecret(intermediate);
    }

    public long evolutionStep3(long secret)
    {
        long intermediate = secret * 2048;
        intermediate = mixResult(intermediate, secret);
        return pruneSecret(intermediate);
    }

    private long mixResult(long res, long secret)
    {
        return res ^ secret;
    }
    
    private long pruneSecret(long secret)
    {
        return secret % 16777216L;
    }

   
}


