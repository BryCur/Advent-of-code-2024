using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using aocUtils.IO;

public class Day04
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const string WANTED_WORD = "XMAS";
    
    private string inputFile;
    
    private List<string> lines = new List<string>();
    private List<(int x, int y)> xPostitions = new List<(int, int)>();
    private List<(int x, int y)> aPostitions = new List<(int, int)>();
    private char[,] table;
    
    public static void Main(string[] args)
    {
        string input;
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: day02 <input file>");
            Console.WriteLine($"Using default input file: {DEFAULT_INPUT_FILE}");
            input = DEFAULT_INPUT_FILE;
        }
        else
        {
            input = args[0];
        }
        
        DateTime startTime = DateTime.Now;
        
        Day04 day04 = new Day04(input);
        day04.part1();
        day04.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day04(String inputFile)
    {
        this.inputFile = inputFile;
        parseInput();
        table = initTable();
    }

    private void parseInput()
    {
        TextFileReader.readFile(inputFile, parseLine);
        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {
        int xPos = lines.Count;
        addPositionsOfCharToList(line, WANTED_WORD[0], xPostitions, xPos);
        addPositionsOfCharToList(line, 'A', aPostitions, xPos);
        lines.Add(line);
    }

    private void addPositionsOfCharToList(string line, char seeked, List<(int, int)> positionList, int xPos)
    {
        int yPos = 0;
        while (line.IndexOf(seeked, yPos) != -1)
        {
            yPos = line.IndexOf(seeked, yPos);
            positionList.Add((xPos, yPos));
            yPos++;
        }
    }

    public void part1()
    {
        int result = 0;
        foreach ((int, int) xPos in xPostitions)
        {
            result += checkRight(xPos)? 1:0;
            result += checkLeft(xPos)? 1:0;
            result += checkUp(xPos)? 1:0;
            result += checkDown(xPos)? 1:0;
            result += checkDiagTopRight(xPos)? 1:0;
            result += checkDiagBottomRight(xPos)? 1:0;
            result += checkDiagTopLeft(xPos)? 1:0;
            result += checkDiagBottomLeft(xPos)? 1:0;
        }
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        int result = 0;

        foreach ((int x, int y) pos in aPostitions)
        {
            result += checkCrossMAS(pos)? 1:0;
        }
        Console.WriteLine($"part 2 solution: {result}");
    }

    private char[,] initTable()
    {
        return lines
            .Select(s => s.ToCharArray())
            .ToArray()
            .To2D();
    }


    private bool checkLeft((int x, int y) pos)
    {
        if(pos.y + 3 < table.GetLength(1))
        {
            char[] tested = new []
            {
                table[pos.x, pos.y], 
                table[pos.x, pos.y+1],
                table[pos.x, pos.y+2], 
                table[pos.x, pos.y+3]
            };   
            
            return new string(tested).Equals(WANTED_WORD);
        }
        return false;
    }
    
    private bool checkRight((int x, int y) pos)
    {
        if(pos.y - 3 >= 0)
        {
            char[] tested = new []
            {
                table[pos.x, pos.y], 
                table[pos.x, pos.y-1],
                table[pos.x, pos.y-2], 
                table[pos.x, pos.y-3]
            };   
            
            return new string(tested).Equals(WANTED_WORD);
        }
        return false;
    }
    
    private bool checkUp((int x, int y) pos)
    {
        if(pos.x - 3 >= 0)
        {
            char[] tested = new []
            {
                table[pos.x, pos.y], 
                table[pos.x-1, pos.y],
                table[pos.x-2, pos.y], 
                table[pos.x-3, pos.y]
            };   
            
            return new string(tested).Equals(WANTED_WORD);
        }
        return false;
    }
    
    private bool checkDown((int x, int y) pos)
    {
        if(pos.x + 3 < table.GetLength(0))
        {
            char[] tested = new []
            {
                table[pos.x, pos.y], 
                table[pos.x+1, pos.y],
                table[pos.x+2, pos.y], 
                table[pos.x+3, pos.y]
            };   
            
            return new string(tested).Equals(WANTED_WORD);
        }
        return false;
    }
    
    private bool checkDiagTopLeft((int x, int y) pos)
    {
        if(pos.x - 3 >= 0 && pos.y - 3 >= 0)
        {
            char[] tested = new []
            {
                table[pos.x, pos.y], 
                table[pos.x-1, pos.y-1],
                table[pos.x-2, pos.y-2], 
                table[pos.x-3, pos.y-3]
            };   
            
            return new string(tested).Equals(WANTED_WORD);
        }
        return false;
    }
    
    private bool checkDiagTopRight((int x, int y) pos)
    {
        if(pos.x - 3 >= 0 && pos.y + 3 < table.GetLength(1))
        {
            char[] tested = new []
            {
                table[pos.x, pos.y], 
                table[pos.x-1, pos.y+1],
                table[pos.x-2, pos.y+2], 
                table[pos.x-3, pos.y+3]
            };   
            
            return new string(tested).Equals(WANTED_WORD);
        }
        return false;
    }
    
    private bool checkDiagBottomRight((int x, int y) pos)
    {
        if(pos.x + 3 < table.GetLength(0) && pos.y + 3 < table.GetLength(1))
        {
            char[] tested = new []
            {
                table[pos.x, pos.y], 
                table[pos.x+1, pos.y+1],
                table[pos.x+2, pos.y+2], 
                table[pos.x+3, pos.y+3]
            };   
            
            return new string(tested).Equals(WANTED_WORD);
        }
        return false;
    }
    
    private bool checkDiagBottomLeft((int x, int y) pos)
    {
        if(pos.x + 3 < table.GetLength(0) && pos.y - 3 >= 0)
        {
            char[] tested = new []
            {
                table[pos.x, pos.y], 
                table[pos.x+1, pos.y-1],
                table[pos.x+2, pos.y-2], 
                table[pos.x+3, pos.y-3]
            };   
            
            return new string(tested).Equals(WANTED_WORD);
        }
        return false;
    }

    private bool checkCrossMAS((int x, int y) pos)
    {
        HashSet<string> validSequence = new HashSet<string>() { "MMSS", "SMSM", "SSMM", "MSMS" };
        if (0 < pos.x && pos.x < table.GetLength(0) - 1 && 0 < pos.y && pos.y < table.GetLength(1) - 1)
        {
            char[] tested = new []
            {
                table[pos.x-1, pos.y-1], 
                table[pos.x-1, pos.y+1],
                table[pos.x+1, pos.y-1], 
                table[pos.x+1, pos.y+1]
            };   
            
            return validSequence.Contains(new string(tested));
        }

        return false;
    }
}