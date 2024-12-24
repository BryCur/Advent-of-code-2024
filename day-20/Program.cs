using System.Runtime.InteropServices.JavaScript;
using aocUtils;
using aocUtils.IO;

public class Day20
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const char WALL_SYMBOL = '#';
    private const char EMPTY_SYMBOL = '.';
    private const int TARGET_GAIN = 100;

    private string InputFile;
    private List<String> Input = new List<String>();
    private Day20Node[,] mapGrid;
    private Coordinate2D startPos;
    private Coordinate2D endPos;


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

        Day20 day20 = new Day20(input);

        day20.part1();
        day20.part2();

        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");

    }

    public Day20(String inputFile)
    {
        this.InputFile = inputFile;
        parseInput();
    }

    private void parseInput()
    {
        TextFileReader.readFile(InputFile, parseLine);
        initMapGrid();

        Console.WriteLine("input processed");
    }

    public void parseLine(string line)
    {

        Input.Add(line);
        if (Input[0].Length != line.Length)
        {
            throw new Exception("Invalid input");
        }

    }


    private void initMapGrid()
    {
        mapGrid = new Day20Node[Input.Count, Input[0].Length];

        for (int i = 0; i < Input.Count; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                mapGrid[i, j] = new Day20Node(i, j, Input[i][j] == WALL_SYMBOL);

                if (j > 0) { mapGrid[i, j].AddNeighbor(mapGrid[i, j - 1]); }

                if (i > 0) { mapGrid[i, j].AddNeighbor(mapGrid[i - 1, j]); }

                if (Input[i][j] == 'S') { startPos = new Coordinate2D(i, j); }

                if (Input[i][j] == 'E') { endPos = new Coordinate2D(i, j); }
            }
        }
    }

    public void part1()
    {
        Day20Node start = mapGrid[startPos.getX(), startPos.getY()];
        Day20Node end = mapGrid[endPos.getX(), endPos.getY()];
        List<Coordinate2D> path = shortestPath(start, end);
        long cutCount = 0;
        int maxCut = 2;


        for (int i = 0; i < path.Count - TARGET_GAIN - maxCut; i++)
        {
            Coordinate2D currentPos = path[i];

            int sliceStart = i + TARGET_GAIN + maxCut;
            var possibleCutDestination = path.Slice(sliceStart, path.Count- sliceStart);
            cutCount += possibleCutDestination
                .Count(destination => Coordinate2D.distanceBetween(currentPos,destination) <= maxCut);
        }
        

        Console.WriteLine($"part 1 solution: {cutCount}");
    }

    public List<Coordinate2D> shortestPath(Day20Node start, Day20Node end)
    {
        var queue = new Queue<(Day20Node Node, List<Coordinate2D> Path)>();
        queue.Enqueue((start, new List<Coordinate2D> { start.GetValue() }));

        // Ensemble des nœuds visités
        var visited = new HashSet<Day20Node>();
        visited.Add(start);

        while (queue.Count > 0)
        {
            var (currentNode, path) = queue.Dequeue();

            // Si nous avons atteint le nœud cible
            if (currentNode.Equals(end))
            {
                return path;
            }

            // Parcourir les voisins
            foreach (Day20Node neighbor in currentNode.GetAdjacentNodes())
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);

                    // Ajouter le voisin et le chemin mis à jour à la file d'attente
                    var newPath = new List<Coordinate2D>(path) { neighbor.GetValue() };
                    queue.Enqueue((neighbor, newPath));
                }
            }
        }

        // Si aucun chemin n'est trouvé, retourner une liste vide
        return new List<Coordinate2D>();
    }

    public void part2()
    {
        Day20Node start = mapGrid[startPos.getX(), startPos.getY()];
        Day20Node end = mapGrid[endPos.getX(), endPos.getY()];
        List<Coordinate2D> path = shortestPath(start, end);
        long cutCount = 0;
        int maxCut = 20;
        int minCut = 2;


        for (int i = 0; i < path.Count - TARGET_GAIN; i++)
        {
            Coordinate2D currentPos = path[i];

            int sliceStart = i + TARGET_GAIN;
            var possibleCutDestination = path.Slice(sliceStart, path.Count-sliceStart);
            cutCount += possibleCutDestination
                .Count(destination => Coordinate2D.distanceBetween(currentPos,destination) <= Math.Min(possibleCutDestination.IndexOf(destination), maxCut));
        }

        Console.WriteLine($"part 2 solution, check: {cutCount}");
    }

    public void renderPositions(List<Day20Node> path)
    {
        HashSet<Coordinate2D> pathSet = new HashSet<Coordinate2D>(path.Select(p => p.GetValue()));
        for (int i = 0; i < mapGrid.GetLength(0); i++)
        {
            String line = "";
            for (int j = 0; j < mapGrid.GetLength(1); j++)
            {
                line += mapGrid[i, j].getIsWall() ? WALL_SYMBOL : pathSet.Contains(new Coordinate2D(i, j)) ? 'X' : EMPTY_SYMBOL;
            }
            Console.WriteLine(line);
        }
        Console.WriteLine("---------------");
    }
}
 

