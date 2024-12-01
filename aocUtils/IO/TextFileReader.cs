namespace aocUtils.IO;

public class TextFileReader
{
    public static void readFile(string filname, Action<string> lineProcessor)
    {
        string line;
        try
        {
            StreamReader sr = new StreamReader(filname);
            line = sr.ReadLine();
            while (line != null)
            {
                // Console.WriteLine(line);
                    
               lineProcessor(line);
                    
                line = sr.ReadLine();
            }
            sr.Close();
        } catch(Exception e) {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
}