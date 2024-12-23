using System.Runtime.InteropServices.JavaScript;
using aocUtils;
using aocUtils.IO;
public class Day18
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";
    private const char WALL_SYMBOL = '#';
    private const int GRID_SIZE = 71;
    private const int WALL_LIMIT = 1024;
    private Coordinate2D STARTING_POINT = new Coordinate2D(0, 0);
    private Coordinate2D ENDING_POINT = new Coordinate2D(GRID_SIZE-1, GRID_SIZE-1);
    
    
    private string InputFile;
    private List<Coordinate2D> Input = new List<Coordinate2D>();
    private Day18Node[,] mapGrid = new Day18Node[GRID_SIZE, GRID_SIZE];
    
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
        
        Day18 day18 = new Day18(input);
        
        day18.part1();
        day18.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed}");
        
    }

    public Day18(String inputFile)
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
        string[] split = line.Split(",");
        Coordinate2D pos = new Coordinate2D(int.Parse(split[1]), int.Parse(split[0]));
        Input.Add(pos);
        
    }
    
    private void initMapGrid()
    {
        for (int i = 0; i < GRID_SIZE; i++)
        {
            for (int j = 0; j < GRID_SIZE; j++)
            {
                Coordinate2D pos = new Coordinate2D(i, j);
                mapGrid[i, j] = new Day18Node(i, j, Input.Slice(0, WALL_LIMIT).Contains(pos));
                
                if(j > 0) { mapGrid[i,j].AddNeighbor(mapGrid[i,j-1]); }
                if(i > 0) { mapGrid[i,j].AddNeighbor(mapGrid[i-1,j]); }
            }
        }
    }
    
    public void part1()
    {
        Day18Node start = mapGrid[STARTING_POINT.getX(), STARTING_POINT.getY()];
        Day18Node end = mapGrid[ENDING_POINT.getX(), ENDING_POINT.getY()];
        long result = shortestPath(start, end).Count-1;
        
        Console.WriteLine($"part 1 solution: {result}");
    }
    public void part2()
    {
        long result = 0;
        Day18Node start = mapGrid[STARTING_POINT.getX(), STARTING_POINT.getY()];
        Day18Node end = mapGrid[ENDING_POINT.getX(), ENDING_POINT.getY()];
        List<Coordinate2D> path = shortestPath(start, end);
        
        Coordinate2D? firstBlocker = null;
        
        for (int i = WALL_LIMIT-1; i < Input.Count; i++)
        {
            Coordinate2D wall = Input[i];
            mapGrid[wall.getX(), wall.getY()].transformToWall();

            if (shortestPath(start, end).Count == 0)
            {
                firstBlocker = wall;
                break;
            }
        }
        
        renderPositions(path, firstBlocker ?? new Coordinate2D(-1,-1));
        Console.WriteLine($"part 2 solution {firstBlocker}"); 
    }

    public List<Coordinate2D> shortestPath(Day18Node start, Day18Node end)
    {
        var queue = new Queue<(Day18Node Node, List<Coordinate2D> Path)>();
        queue.Enqueue((start, new List<Coordinate2D> { start.GetValue() }));

        // Ensemble des nœuds visités
        var visited = new HashSet<Day18Node>();
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
            foreach (Day18Node neighbor in currentNode.GetAdjacentNodes())
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

    public void renderPositions(List<Coordinate2D> path, Coordinate2D point)
    {
        HashSet<Coordinate2D> pathSet = new HashSet<Coordinate2D>(path);
        for (int i = 0; i < mapGrid.GetLength(0); i++)
        {
            String line = "";
            for (int j = 0; j < mapGrid.GetLength(1); j++)
            {
                line += mapGrid[i, j].getIsWall() ? WALL_SYMBOL : point.Equals(i,j) ? '=' : pathSet.Contains(new Coordinate2D(i, j)) ? 'X' : '.';
            }
            Console.WriteLine(line);
        }
        Console.WriteLine("---------------");
    }
}