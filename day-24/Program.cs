using aocUtils;
using aocUtils.IO;
using day_24;

public class Day24
{
    private const string DEFAULT_INPUT_FILE = "./inputs/fixed-input.txt";

    private string InputFile;
    private Dictionary<string, bool?> wires = new Dictionary<string, bool?>();
    private List<Instructions> instructions = new List<Instructions>();
        
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
        
        Day24 day24 = new Day24(input);
        
        day24.part1();
        day24.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed} ms");
        
    }

    public Day24(String inputFile)
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
        if (line.Contains(":"))
        {
            string[] parts = line.Split(':');
            wires.Add(parts[0].Trim(), parts[1].Trim() == "1");
        }
        
        if (line.Contains("->"))
        {
            string[] parts = line.Split(' '); // format ref1 INS ref2 -> ref3
            
            if(!wires.ContainsKey(parts[0].Trim())) { wires.Add(parts[0].Trim(), null); }
            if(!wires.ContainsKey(parts[2].Trim())) { wires.Add(parts[2].Trim(), null); }
            if(!wires.ContainsKey(parts[4].Trim())) { wires.Add(parts[4].Trim(), null); }
            instructions.Add(new Instructions(parts[0].Trim(), parts[2].Trim(), parts[4].Trim(), parts[1].Trim()));
        }
    }


    public void part1()
    {
        while (instructions.Exists(ins => !ins.isDone() && ins.isOperationPossible(wires)))
        {
            var toPerform = instructions.Where(ins => !ins.isDone() && ins.isOperationPossible(wires)).ToList();
            foreach (var instruction in toPerform)
            {
                wires[instruction.getDest()] = instruction.performOperation(wires);
            }
        }
        
        long result = getZwireValue();
        Console.WriteLine($"part 1 solution: {result}"); // 58740594706150
    }

    /**
     * need to be done outside the code. Run the code with real-input, then import the generated file in Mermaid
     * this will output a graph of the logic gates, look for inconsistencies
     * the difference printed should give a hint where to look for (need to be adapted to match the leaast significant bit) :)
     * cc real-input into a new file and work on the new input file and run again to see the progress
     */
    public void part2()
    {
        long result = 0;
        long xVal = getTotalWireValue("x");
        long yVal = getTotalWireValue("y");
        long zVal = getTotalWireValue("z");
        long supposedResult = xVal + yVal;
        long diff = Math.Abs(zVal ^ supposedResult);
        printInstructionToFile2();
        
        Console.WriteLine($"current: {Convert.ToString(zVal,2)}");
        Console.WriteLine($"expected: {Convert.ToString(supposedResult,2)}");
        Console.WriteLine($"difference: {Convert.ToString(diff,2)}");
        Console.WriteLine($"part 2 solution: {zVal == supposedResult}"); // cvh,dbb,hbk,kvn,tfn,z14,z18,z23
    }

    private long getZwireValue()
    {
        return getTotalWireValue("z");
    }
    
    private long getTotalWireValue(string wireStart)
    {
        long result = 0;
        List<string> wireKeys = wires.Keys.Where(k => k.StartsWith(wireStart)).Order().ToList();

        for (int i = 0; i < wireKeys.Count; i++)
        {
            if (wires[wireKeys[i]].HasValue && wires[wireKeys[i]].Value)
            {
                result += (long)Math.Pow(2, i);
            }
        }
        
        return result;
    }
    private void printInstructionToFile2()
    {
        string docPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"day24_instructions_forMermaid2.txt")))
        {
            outputFile.WriteLine("%% Nodes");
            foreach (string key in wires.Keys)
            {
                outputFile.WriteLine($"{key}({key} - {wires[key]})");
            }
            
            foreach (var inst in instructions)
            {
                   outputFile.WriteLine($"{inst.getRef1()} -- {inst.getOperation()} --> {inst.getDest()}");
                   outputFile.WriteLine($"{inst.getRef2()} -- {inst.getOperation()} --> {inst.getDest()}");
            }
        }
    }
}


