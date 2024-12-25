using aocUtils;
using aocUtils.IO;

public class Day23
{
    private const string DEFAULT_INPUT_FILE = "./inputs/real-input.txt";

    private Dictionary<string, Day23Node> listOfPcs = new Dictionary<string, Day23Node>();
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
        
        Day23 day23 = new Day23(input);
        
        day23.part1();
        day23.part2();
        
        double timeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Console.WriteLine($"Time elapsed: {timeElapsed} ms");
        
    }

    public Day23(String inputFile)
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
        var splitted = line.Split('-');
        Day23Node PC1;
        Day23Node PC2;

        if (listOfPcs.ContainsKey(splitted[0]))
        {
            PC1 = listOfPcs[splitted[0]];
        }
        else
        {
            PC1 = new Day23Node(splitted[0]);
            listOfPcs.Add(splitted[0], PC1);
        }
        
        if (listOfPcs.ContainsKey(splitted[1]))
        {
            PC2 = listOfPcs[splitted[1]];
        }
        else
        {
            PC2 = new Day23Node(splitted[1]);
            listOfPcs.Add(splitted[1], PC2);
        }
        
        PC1.AddNeighbor(PC2);
    }


    public void part1()
    {
        long result = 0;
        HashSet<string> triadList = new HashSet<string>();
        foreach (Day23Node currentPc in listOfPcs.Values.Where(pc => pc.GetValue().StartsWith("t")))
        {
            foreach (var neighbor in currentPc.GetAdjacentNodes())
            {
                var currentNeighbourList = currentPc.GetAdjacentNodes();
                var neighborNeighbourList = neighbor.GetAdjacentNodes();

                foreach (var third in currentNeighbourList.Intersect(neighborNeighbourList))
                {
                    List<string> triad = new List<string>()
                    {
                        currentPc.GetValue(),
                        neighbor.GetValue(),
                        third.GetValue()
                    }.Order().ToList();
                    
                    triadList.Add(string.Join(",", triad));
                }
            }
            
        }
        Console.WriteLine($"part 1 solution: {triadList.Count}");
    }

    public void part2()
    {
        long result = 0;
        List<string> accumulator = new List<string>();
        BronKerbosch(new HashSet<string>(), new HashSet<string>(listOfPcs.Keys), new HashSet<string>(), accumulator);
        accumulator.OrderByDescending(s => s.Length);
        Console.WriteLine($"part 2 solution: {accumulator[0]}"); 
    }
    
    void BronKerbosch(HashSet<string> R, HashSet<string> P, HashSet<string> X, List<string> accumulator)
    {
        if (P.Count == 0 && X.Count == 0)
        {
            // Clique maximale trouvée
            accumulator.Add(string.Join(",", R.Order()));
            return;
        }

        // Copie des nœuds de P pour éviter des modifications concurrentes
        foreach (var v in P.ToList())
        {
            
            var currentNodeNeighbours = listOfPcs[v].GetAdjacentNodes().Select(n => n.GetValue()).ToHashSet();
            // Appel récursif en ajoutant le nœud courant à R
            BronKerbosch(
                new HashSet<string>(R) { v },                                    // R ∪ {v}
                new HashSet<string>(P.Intersect(currentNodeNeighbours)),                     // P ∩ voisins(v)
                new HashSet<string>(X.Intersect(currentNodeNeighbours)),                     // X ∩ voisins(v)
                accumulator
            );

            // Mise à jour des ensembles P et X
            P.Remove(v);
            X.Add(v);
        }
    }
}


