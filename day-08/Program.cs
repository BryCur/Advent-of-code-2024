using System.IO;
using System.Runtime.CompilerServices;
using aocUtils;
using aocUtils.IO;

public class Day08
{
    private const string DEFAULT_INPUT_FILE = "./inputs/example.txt";
    
    private string inputFile;
    
    private Dictionary<char, HashSet<Coordinate2D>> antennaMap = new Dictionary<char, HashSet<Coordinate2D>>();
    private HashSet<Coordinate2D> antinodePositions = new HashSet<Coordinate2D>();
    private List<string> input = new List<string>();
    private char[,] grid;
    
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
        
        Day08 day08 = new Day08(input);
        
        day08.part1();
        day08.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day08(String inputFile)
    {
        this.inputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {
        TextFileReader.readFile(inputFile, parseLine);
        grid = initTable();
        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {
        for(int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c != '.')
            {
                Coordinate2D position = new Coordinate2D(input.Count, i);
                
                if (!antennaMap.ContainsKey(c))
                {
                    antennaMap.Add(c, new HashSet<Coordinate2D>(){});
                }
                
                antennaMap[c].Add(position);
            }
        }
        
        input.Add(line);
        
    }

    public void part1()
    {
        buildAntinodeMap();
        int result = antinodePositions.Count;
        Console.WriteLine($"part 1 solution: {result}");
    }
    
    public void part2()
    {
        buildAntinodeMapPart2();
        int result = antinodePositions.Count;
        Console.WriteLine($"part 2 solution: {result}");
    }
    
    private char[,] initTable()
    {
        return input
            .Select(s => s.ToCharArray())
            .ToArray()
            .To2D();
    }

    private void buildAntinodeMap()
    {
        antinodePositions.Clear();

        foreach ((char c, HashSet<Coordinate2D> positions) in antennaMap)
        {

            List<Coordinate2D> positionsList = positions.ToList();
            for(int i = 0; i < positionsList.Count; i++)
            {
                Coordinate2D reference = positionsList[i];
                for(int j = 0; j < positionsList.Count; j++)
                {
                    if (i == j)
                    {
                        continue; 
                    }
                    
                    Coordinate2D sibling = positionsList[j];
                    
                    Coordinate2D difference = reference - sibling;
                    
                    Coordinate2D antinode1 = reference + difference;
                    Coordinate2D antinode2 = reference - (difference * 2);
                    
                    if(isCoordinateInGrid(antinode1)) antinodePositions.Add(antinode1);
                    if(isCoordinateInGrid(antinode2)) antinodePositions.Add(antinode2);
                }
            }
        }
        
        // renderMapWithAntinode();
    }

    private void buildAntinodeMapPart2()
    {
        antinodePositions.Clear();

        foreach ((char c, HashSet<Coordinate2D> positions) in antennaMap)
        {

            List<Coordinate2D> positionsList = positions.ToList();
            for(int i = 0; i < positionsList.Count; i++)
            {
                Coordinate2D reference = positionsList[i];
                for(int j = 0; j < positionsList.Count; j++)
                {
                    if (i == j)
                    {
                        continue; 
                    }
                    
                    Coordinate2D sibling = positionsList[j];
                    Coordinate2D difference = reference - sibling;

                    int factor = 0;
                    Coordinate2D antinode1 = reference + (difference * factor);
                    Coordinate2D antinode2 = reference - (difference * factor);
                    
                    do
                    {
                        if (isCoordinateInGrid(antinode1)) antinodePositions.Add(antinode1);
                        if (isCoordinateInGrid(antinode2)) antinodePositions.Add(antinode2);
                        
                        factor++;
                        
                        antinode1 = reference + (difference * factor);
                        antinode2 = reference - (difference * factor);
                    } while (isCoordinateInGrid(antinode1) || isCoordinateInGrid(antinode2));

                }
            }
        }
        
        // renderMapWithAntinode();
    }
    private bool isCoordinateInGrid(Coordinate2D coordinate)
    {
        return coordinate.getX() >= 0 & coordinate.getY() >= 0
            && coordinate.getX() < grid.GetLength(0) & coordinate.getY() < grid.GetLength(1);
    }

    private void renderMapWithAntinode()
    {
        char[,] toRender = new char[grid.GetLength(0), grid.GetLength(1)];
        Array.Copy(grid, toRender, grid.Length);

        foreach (Coordinate2D antinode in antinodePositions)
        {
                toRender[antinode.getX(), antinode.getY()] = '#';
        }

        for (int i = 0; i < toRender.GetLength(0); i++)
        {
            for (int j = 0; j < toRender.GetLength(1); j++)
            {
                Console.Write(toRender[i, j]);
            }
            Console.WriteLine();
        }
    }
    
}


