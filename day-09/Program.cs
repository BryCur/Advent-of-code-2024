using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using aocUtils;
using aocUtils.IO;
using day_09;

public class Day09
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    
    private string inputFile;
    
    private string input = "";
    
    public static void Main(string[] args)
    {
        string input;
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: day09 <input file>");
            Console.WriteLine($"Using default input file: {DEFAULT_INPUT_FILE}");
            input = DEFAULT_INPUT_FILE;
        }
        else
        {
            input = args[0];
        }
        
        DateTime startTime = DateTime.Now;
        
        Day09 day09 = new Day09(input);
        
        day09.part1();
        day09.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
    }

    public Day09(String inputFile)
    {
        this.inputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {
        TextFileReader.readFile(inputFile, parseLine);
        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {
        input = line;
    }

    public void part1()
    {
        long result = interpretInput();
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        long result = interpretInput2();
        Console.WriteLine($"part 2 solution: {result}");
    }

    private long interpretInput()
    {
        int leftReaderPos = 0;
        int rightReaderPos = input.Length - (input.Length % 2 == 0?  2 : 1);

        int currentPos = 0;
        
        long accumulator = 0;
        long factor = 0;
        string rpz = "";

        Queue<long> rightQueue = new Queue<long>();
        fillQueue(rightQueue, rightReaderPos, input[rightReaderPos] - '0');
        
        
        // condition to stop is wrong...
        while (leftReaderPos <= rightReaderPos)
        {
            if (currentPos % 2 == 0 && leftReaderPos != rightReaderPos)
            {
                // should add numbers from the left
                int spots = input[currentPos] - '0';
                for (int i = 0; i < spots; i++)
                {
                    accumulator += factor++ * (leftReaderPos / 2L);
                    rpz += $"{(leftReaderPos / 2L)},";
                }

                currentPos++;
                leftReaderPos += 2;
            }
            else
            {
                // should add numbers from the right
                int spots = input[currentPos] - '0';
                
                for (int i = 0; i <  spots; i++)
                {
                    if (rightQueue.Count == 0)
                    {
                        rightReaderPos -= 2;
                        fillQueue(rightQueue, rightReaderPos, input[rightReaderPos] - '0');
                    }

                    if (rightReaderPos < leftReaderPos)
                    {
                        break;
                    }
                    long value = rightQueue.Dequeue();
                    accumulator += factor++ * (value / 2L);
                    rpz += $"{(value / 2L)},";
                }
                
                currentPos++;
                
            }
        }

        
        // Console.WriteLine($"string representation: {rpz}");
        return accumulator;
    }


    private long interpretInput2()
    {
        List<DiskSpace> disk = new List<DiskSpace>();

        // create disk state as list of objects
        for (int i = 0; i < input.Length; i++)
        {
            int size = input[i] - '0';
            bool isFile = i % 2 == 0;
            
            DiskSpace current = new DiskSpace(size, isFile ? i/2 : 0, !isFile);
            disk.Add(current);
        }

        // re-arrange files according to instructions 
        for (int i = disk.Count - 1; i > 0; i-- )
        {
            DiskSpace current = disk[i];
            if (current.IsEmpty) { continue; }
            
            // current is a file
            DiskSpace? firstFittingEmptySpace = disk
                .Slice(0, i)
                .Find(space => space.IsEmpty && space.Size >= current.Size);
            
            if(firstFittingEmptySpace == null) { continue; }

            int indexOfEmptySpace = disk.IndexOf(firstFittingEmptySpace);
            if (firstFittingEmptySpace.Size > current.Size)
            {
                // "split" empty space in a partition of the size of current, and a partition
                // of remaining space, then "swap" equal spaces
                
                // in practice: 
                // 1. create copy of current file
                DiskSpace copyOfFile = new DiskSpace(current.Size, current.Value, current.IsEmpty);
                
                // 2. insert copy of file in front of empty space 
                disk.Insert(indexOfEmptySpace, copyOfFile);
                i += 1;
                
                // 3. reduce emptySpace Size 
                firstFittingEmptySpace.Size -= copyOfFile.Size;
                
                // 4. set current as an empty space
                current.IsEmpty = true;
            }
            else
            {
                // empty space perfectly matches the current file ==> swap the properties
                firstFittingEmptySpace.Value = current.Value;
                current.Value = 0;
                firstFittingEmptySpace.IsEmpty = false;
                current.IsEmpty = true;
            }
        }

        long result = 0;
        long iterator = 0;
        // compute new checksum
        foreach (DiskSpace space in disk)
        {
            if (space.IsEmpty)
            {
                iterator += space.Size;
                continue;
            }

            for (int i = 0; i < space.Size; i++)
            {
                result += space.Value * iterator++;
            }
        }

        return result;
    }
    
    void fillQueue(Queue<long> queue, long value, int count)
    {
        for (int i = 0; i < count; i++)
        {
            queue.Enqueue(value);
        }
    }
}


