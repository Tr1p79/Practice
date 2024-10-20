using System;
using System.IO;

namespace Examples
{
    internal class FileIOOperations
    {
        public static void ReadWriteTextFile()
        {
            string filePath = "sample.txt";

            // Writing to a file
            string[] lines = { "First line", "Second line", "Third line" };
            File.WriteAllLines(filePath, lines);
            Console.WriteLine("File written successfully.");

            // Reading from a file
            string[] readLines = File.ReadAllLines(filePath);
            Console.WriteLine("File contents:");
            foreach (string line in readLines)
            {
                Console.WriteLine(line);
            }

            // Appending to a file
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine("Fourth line");
            }
            Console.WriteLine("Line appended to file.");

            // Reading a file using StreamReader
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                Console.WriteLine("File contents using StreamReader:");
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

            // File and directory operations
            string directoryPath = "SampleDirectory";
            Directory.CreateDirectory(directoryPath);
            File.Copy(filePath, Path.Combine(directoryPath, "sample_copy.txt"));
            Console.WriteLine("File copied to new directory.");

            // Exception handling for file operations
            try
            {
                string nonExistentFile = "nonexistent.txt";
                string content = File.ReadAllText(nonExistentFile);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An I/O error occurred: {ex.Message}");
            }
        }
    }
}
