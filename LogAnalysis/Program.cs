using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Zadejte cestu k souboru s logy (nebo 'exit' pro ukončení): ");
            string filePath = Console.ReadLine();

            if (filePath.ToLower() == "exit")
            {
                break;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Soubor neexistuje. Zkuste to prosím znovu.");
                continue;
            }

            try
            {
                List<LogEntry> logs = ReadLogs(filePath);
                if (logs.Count == 0)
                {
                    Console.WriteLine("Soubor neobsahuje žádné platné záznamy logů.");
                    continue;
                }
                AnalyzeLogs(logs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nastala chyba při analýze logů: {ex.Message}");
            }

            Console.WriteLine("\nStiskněte libovolnou klávesu pro pokračování...");
            Console.ReadKey();
            Console.Clear();
        }

        Console.WriteLine("Děkujeme za použití analyzátoru logů. Na shledanou!");
    }

    static List<LogEntry> ReadLogs(string filePath)
    {
        List<LogEntry> logs = new List<LogEntry>();

        foreach(string line in File.ReadAllLines(filePath))
        { 
            try
            {
                string[] parts = line.Split(new[] { ' ' }, 4);
                if (parts.Length < 4) continue;

                DateTime timestamp = DateTime.Parse(parts[0] + " " + parts[1]);
                string level = parts[2].Trim('[',']');
                string message = parts[3];

                logs.Add(new LogEntry
                {
                    Timestamp = timestamp,
                    Level = level,
                    Message = message
                });
            }
            catch(Exception e) 
            {
                Console.WriteLine($"Chyba při čtení řádků: {line}. Chyba: {e.Message}");
            }
        }
        return logs;
    }

    static void AnalyzeLogs(List<LogEntry> logs)
    {
        // Počítání výskytů každé úrovně logu
        var levelCounts = logs.GroupBy(l => l.Level)
                              .ToDictionary(g => g.Key, g => g.Count());

        Console.WriteLine("Počet výskytů jednotlivých úrovní logu: ");
        foreach (var level in levelCounts) 
        { 
            Console.WriteLine($"{level.Key}: {level.Value}");
        }

        // Identifikace hodiny s nejvíce chybovými záznamy
        var errorsByHour = logs.Where(l => l.Level == "ERROR")
                               .GroupBy(l => l.Timestamp.Hour)
                               .OrderByDescending(g => g.Count())
                               .FirstOrDefault();

        if (errorsByHour != null) 
        {
            Console.WriteLine($"\nHodina s nejvíce chybovými záznamy: {errorsByHour.Key}:00 - {errorsByHour.Key}:59, Počet chyb: {errorsByHour.Count()}");
        }

        // 5 nejčastějších chybových zpráv
        var topErrors = logs.Where(l => l.Level == "ERORR")
                            .GroupBy(l => l.Message)
                            .OrderByDescending(g => g.Count())
                            .Take(5);

        Console.WriteLine("\n5 nejčastějších chybových zpráv: ");
        foreach(var error in topErrors) 
        {
            Console.WriteLine($"Počet výskytů: {error.Count()}, Zpráva: {error.Key}");
        }
    }

    class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
    }
}