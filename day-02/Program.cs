using System.IO;
using System.Runtime.CompilerServices;
using aocUtils.IO;

public class Day02
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const int MIN_DIFF = 1;
    private const int MAX_DIFF = 3;
    
    private string inputFile;
    
    List<List<int>> safeReports = new List<List<int>>();
    List<List<int>> safeReportsWithProblemDampener = new List<List<int>>();
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
        
        Day02 day02 = new Day02(input);
        day02.part1();
        day02.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day02(String inputFile)
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
        // split by space
        string[] splitted = line.Split(new char[] {' ','\t'}, StringSplitOptions.RemoveEmptyEntries);
        // parse to int
        List<int> report = Array.ConvertAll(splitted, int.Parse).ToList();
        
        if (isReportSafeWithDiffList(report))
        {
            safeReports.Add(report);
        }
        else
        {
            for (int i = 0; i < report.Count; i++)
            {
                List<int> testedReport = new List<int>(report);
                testedReport.RemoveAt(i);
                if (isReportSafeWithDiffList(testedReport))
                {
                    safeReportsWithProblemDampener.Add(report);
                    break;
                }
            }
        }
    }

    public void part1()
    {
        int result = safeReports.Count; 
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
       int result = safeReports.Count + safeReportsWithProblemDampener.Count;
        Console.WriteLine($"part 2 solution: {result}");
    }

    private bool isReportSafe(List<int> report)
    {
        return (isReportAllIncreasing(report) || isReportAllDecreasing(report)) && isConsecutiveElementDiffRuleRespected(report);
    }

    private bool isReportSafeWithDiffList(List<int> report)
    {
        List<int> diffList = getDiffList(report);
        
        int maxDiffProblemCount = diffList.Where(i => Math.Abs(i) > MAX_DIFF).ToList().Count;
        int minDiffProblemCount = diffList.Where(i => Math.Abs(i) < MIN_DIFF).ToList().Count;
        int misplaceCount = Math.Min(diffList.FindAll(n => n >= 0).Count, diffList.FindAll(n => n <= 0).Count);

        bool orderedCondition = misplaceCount == 0;
        bool minDiffCondition = minDiffProblemCount == 0;
        bool maxDiffCondition = maxDiffProblemCount == 0;
         

        return orderedCondition && minDiffCondition && maxDiffCondition;
    }

    private bool isReportAllIncreasing(List<int> report)
    {
        List<int> sortedList = new List<int>(report);
        sortedList.Sort();
        
        bool testIncreasing = report.SequenceEqual(sortedList);
        return testIncreasing;
    }
    private bool isReportAllDecreasing(List<int> report)
    {
        List<int> sortedList = new List<int>(report);
        sortedList.Sort();
        sortedList.Reverse();
        
        bool testDecreasing = report.SequenceEqual(sortedList);
        return testDecreasing;
    }
    private bool isConsecutiveElementDiffRuleRespected(List<int> report)
    {
        int? previous = null;
        foreach (var i in report)
        {
            if (!previous.HasValue)
            {
                previous = i;
            }
            else
            {
                int diff = Math.Abs(i - previous.Value);
                if (diff < MIN_DIFF || diff > MAX_DIFF)
                {
                    return false;
                }
                else
                {
                    previous = i;
                }
            }
        }

        return true;
    }

    private List<int> getDiffList(List<int> report)
    {
        int? previous = null;
        List<int> diffList = new List<int>();
        foreach (var i in report)
        {
            if (previous.HasValue)
            {
                diffList.Add(previous.Value - i);
            }
            previous = i;
        }

        return diffList;
    }
}


