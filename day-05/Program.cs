using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using aocUtils.IO;

public class Day05
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private string inputFile;
    
    private Dictionary<int, List<int>> precedenceRuleDictionary = new Dictionary<int, List<int>>();
    private List<List<int>>updates = new List<List<int>>();
    
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
        
        Day05 day05 = new Day05(input);
        day05.part1();
        day05.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day05(String inputFile)
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
        if (line.Contains('|'))
        {
            // rule line
            int[] splitted = Array.ConvertAll(line.Split("|"), int.Parse);
            if (precedenceRuleDictionary.ContainsKey(splitted[1]))
            {
                precedenceRuleDictionary[splitted[1]].Add(splitted[0]);
            }
            else
            {
                precedenceRuleDictionary.Add(splitted[1], new List<int>(){splitted[0]});
            }
        } else if (line.Contains(',')) {
            // update line
            updates.Add(Array.ConvertAll(line.Split(","), int.Parse).ToList());
        }
    } 

    public void part1()
    {
        int result = 0;
        foreach (List<int> update in updates)
        {
            if (isUpdateValid(update))
            {
                // Console.WriteLine($"valid update: {string.Join(',', update)}");
                if (update.Count % 2 == 0)
                {
                    Console.WriteLine($"update with even number of values...");
                }
                result += update[update.Count/2];
            }
        }
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        int result = 0;
        foreach (List<int> update in updates)
        {
            if (!isUpdateValid(update))
            {
                var ordered = orderPagesInUpdate(update);
                if (ordered.Count % 2 == 0)
                {
                    Console.WriteLine($"update with even number of values...");
                }
                result += ordered[ordered.Count/2];
            }
        }
        Console.WriteLine($"part 2 solution: {result}");
    }


    private bool isUpdateValid(List<int> update)
    {
        return !getFaultyPair(update).HasValue;
    }
    private (int pageBefore, int pageAfter)? getFaultyPair(List<int> update)
    {
        foreach (int page in update)
        {
            if (precedenceRuleDictionary.ContainsKey(page))
            {
                foreach (int precedingPage in precedenceRuleDictionary[page])
                {
                    if (update.IndexOf(page) < update.IndexOf(precedingPage))
                    {
                        return (precedingPage, page);
                    }
                }
                
            }
        }

        return null;
    }

    private List<int> orderPagesInUpdate(List<int> update)
    {
        List<int> orderedUpdate = new List<int>(update);
        var faultyPair = getFaultyPair(orderedUpdate);
        while (faultyPair.HasValue)
        {
            orderedUpdate.Remove(faultyPair.Value.pageBefore);
            int indexPageAfter = orderedUpdate.IndexOf(faultyPair.Value.pageAfter);
            orderedUpdate.Insert(indexPageAfter, faultyPair.Value.pageBefore);
            
            faultyPair = getFaultyPair(orderedUpdate);
        }
        
        return orderedUpdate;
    }
}