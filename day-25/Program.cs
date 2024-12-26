using System.Runtime.InteropServices.JavaScript;
using aocUtils;
using aocUtils.IO;
public class Day15
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const char PIN_SYMBOL = '#';
    
    private string InputFile;
    private List<List<string>> Input = new List<List<string>>();
    private List<List<int>> keyholes = new List<List<int>>();
    private List<List<int>> keys = new List<List<int>>();
    private Coordinate2D robotPosition;
    
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
        
        List<string> current = new List<string>();
        TextFileReader.readFile(InputFile, (line) =>
        {
            if (line.Length == 0)
            {
                Input.Add(new List<string>(current));
                current.Clear();
            }
            else
            {
                current.Add(line);
            }
        });
        
        parseKeyHoles();
        parseKeys();
        Console.WriteLine("input processed");
    }

    public void parseKeyHoles()
    {
        foreach (List<string> holeInput in Input.Where(l => l[0].Contains(PIN_SYMBOL)))
        {
            List<int> currentHole = new List<int>(new int[holeInput[0].Length]);

            for (int i = 1; i < holeInput.Count-1; i++)
            {
                for (int j = 0; j < holeInput[i].Length; j++)
                {
                    currentHole[j] += holeInput[i][j] == PIN_SYMBOL ? 1 : 0;
                }
            }
            
            keyholes.Add(currentHole);
        }
    }
    
    public void parseKeys()
    {
        foreach (List<string> keyInput in Input.Where(l => l[l.Count-1].Contains(PIN_SYMBOL)))
        {
            List<int> currentKey = new List<int>(new int[keyInput[0].Length]);

            for (int i = 1; i < keyInput.Count-1; i++)
            {
                for (int j = 0; j < keyInput[i].Length; j++)
                {
                    currentKey[j] += keyInput[i][j] == PIN_SYMBOL ? 1 : 0;
                }
            }
            
            keys.Add(currentKey);
        }
    }
    
    public void part1()
    {
        long result = 0;
        foreach (List<int> hole in keyholes)
        {
            result += keys.Count(key => keyFitsHole(key, hole));
        }
        Console.WriteLine($"part 1 solution: {result}");
    }
    public void part2()
    {
        long result = 0;
        Console.WriteLine($"part 2 solution, check: {result}"); // right answer is 7502
    }

    private bool keyFitsHole(List<int> key, List<int> hole)
    {
        foreach (var couple in key.Zip(hole))
        {
            if(couple.First + couple.Second > 5) {
                return false;
            }
        }
        
        return true;
    }
}