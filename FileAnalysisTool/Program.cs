namespace FileAnalysisTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true) 
            {
                Console.WriteLine("Enter the path to the text file (or 'exit' to quit app)");
                string filePath = Console.ReadLine();

                if(filePath.ToLower() == "exit")
                {
                    break;
                }

                //C:\02_ReposCore\09_Practice\Practice
                AnalyzeFile(filePath);
            }
        }
        
        static void AnalyzeFile(string filePath) 
        {
            try 
            { 
                string content = File.ReadAllText(filePath);
                string[] words = content.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                int totalWords = words.Length;
                int totalCharsWithSpaces = content.Length;
                int totalCharsWithoutSpaces = content.Replace(" ","").Length;
                int totalUniqueWords = words.Distinct().Count();
                double averageWordLength = words.Average(w => w.Length);//totalCharsWithSpaces / totalWords; 

                var wordFrequency = words
                    .GroupBy(w => w, StringComparer.OrdinalIgnoreCase)
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .ToDictionary(g => g.Key, g => g.Count());

                DisplayResult(totalWords, totalCharsWithSpaces, totalCharsWithoutSpaces, totalUniqueWords, averageWordLength, wordFrequency);
            } 
            catch (FileNotFoundException) 
            {
                Console.WriteLine("Error: File not found. Please check the file path and try again.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            

        }

        static void DisplayResult(int totalWords, int totalCharsWithSpaces, int totalCharsWithoutSpaces, 
                                  int uniqueWords, double averageWordLength, Dictionary<string, int> wordFrequency)
        {
            Console.WriteLine($"Total number of words: {totalWords}");
            Console.WriteLine($"Total number of characters (including spaces): {totalCharsWithSpaces}");
            Console.WriteLine($"Total number of characters (excluding spaces): {totalCharsWithoutSpaces}");
            Console.WriteLine($"Total number of unique words: {uniqueWords}");
            Console.WriteLine($"Average word length: {averageWordLength:F2}"); //:F2
            Console.WriteLine("Top 5 most frequent words: ");
            foreach ( var pair in wordFrequency) 
            {
                Console.WriteLine($" {pair.Key}: {pair.Value}");
            }
        }
    }
}
