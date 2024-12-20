﻿using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using aocUtils;
using aocUtils.IO;

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
        int result = 0;
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

        
        Console.WriteLine($"string representation: {rpz}");
        return accumulator;
    }

    void fillQueue(Queue<long> queue, long value, int count)
    {
        for (int i = 0; i < count; i++)
        {
            queue.Enqueue(value);
        }
    }
}


