using aocUtils;
using aocUtils.IO;
using day_17;

public class Day17
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";

    private Registar reg = new Registar();
    private List<long> input = new List<long>();
    private List<long> output = new List<long>();
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
        
        Day17 day17 = new Day17(input);
        
        day17.part1();
        day17.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed} ms");
        
    }

    public Day17(String inputFile)
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
        if (line.Contains("Register A:"))
        {
            reg.setA(long.Parse(line.Split(":")[1].Trim()));
        }
        if (line.Contains("Register B:"))
        {
            reg.setB(long.Parse(line.Split(":")[1].Trim()));
        }
        if (line.Contains("Register C:"))
        {
            reg.setC(long.Parse(line.Split(":")[1].Trim()));
        }
        if (line.Contains("Program:"))
        {
            var program = line.Split(":")[1].Trim().Split(",").ToList().Select(long.Parse).ToList();
            input.AddRange(program);
        }
    }


    public void part1()
    {

        int instructionPointer = 0;
        while (instructionPointer < input.Count)
        {
            long instruction = input[instructionPointer];
            long operand = input[instructionPointer + 1];

            switch (instruction)
            {
                case 0: adv_opcode0(operand); break;
                case 1: bxl_opcode1(operand); break;
                case 2: bst_opcode2(operand); break;
                case 3: instructionPointer = jnz_opcode3(operand, instructionPointer); break;
                case 4: bxc_opcode4(operand); break;
                case 5: out_opcode5(operand); break;
                case 6: bdv_opcode6(operand); break;
                case 7: cdv_opcode7(operand); break;
            }

            if (input[instructionPointer] == instruction)
            {
                instructionPointer += 2;
            }
        }
        
        long result = 0;
        Console.WriteLine($"part 1 solution: {string.Join(",",output)}");
    }

    public void part2()
    {
        long result = 0;
        Console.WriteLine($"part 2 solution, check: {result}"); 
    }

    private void adv_opcode0(long operand)
    {
        Console.WriteLine($"ADV instruction, operand (combo): {operand}, register A: {reg.getA()}");
        long result = (long)(reg.getA() / Math.Pow(2, interpretComboOperands(operand)));
        reg.setA(result);
    }

    private void bxl_opcode1(long operand)
    {
        Console.WriteLine($"BXL instruction, operand (lit.): {operand}, register B: {reg.getB()}");
        long result = reg.getB() ^ operand;
        reg.setB(result);
    }

    private void bst_opcode2(long operand)
    {
        Console.WriteLine($"BST instruction, operand (combo): {operand}, register B: {reg.getB()}");
        long result = interpretComboOperands(operand) % 8;
        reg.setB(result);
    }

    private int jnz_opcode3(long operand, int instructionPointer)
    {
        Console.WriteLine($"JNZ instruction, operand (lit.): {operand}, register A: {reg.getA()}, instructionPointer: {instructionPointer}");
        if (reg.getA() != 0)
        {
            return (int)operand;
        }

        return instructionPointer;
    }

    private void bxc_opcode4(long operand)
    {
        Console.WriteLine($"BXC instruction, operand (ignored): {operand}, register B: {reg.getB()}, register C: {reg.getC()}");
        reg.setB(reg.getB() ^ reg.getC());
    }

    private void out_opcode5(long operand)
    {
        Console.WriteLine($"OUT instruction, operand (combo): {operand}");
        output.Add(interpretComboOperands(operand) % 8);
    }
    
    private void bdv_opcode6(long operand)
    {
        Console.WriteLine($"BDV instruction, operand (combo): {operand}, register A: {reg.getA()}");
        long result = (long)(reg.getA() / Math.Pow(2, interpretComboOperands(operand)));
        reg.setB(result);
    }
    
    private void cdv_opcode7(long operand)
    {
        Console.WriteLine($"BDV instruction, operand (combo): {operand}, register A: {reg.getA()}");
        long result = (long)(reg.getA() / Math.Pow(2, interpretComboOperands(operand)));
        reg.setC(result);
    }

    private long interpretComboOperands(long operand)
    {
        switch (operand)
        {
            case 4: return reg.getA();
            case 5: return reg.getB();
            case 6: return reg.getC();
            case 7: Console.WriteLine("THAT IS NOT SUPPOSED TO HAPPEN IN A VLID PROGRAM");
                return 1;
            default: return operand;
            
        }
    }
}


