namespace CSVAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Zadejte cestu k CSV souboru (nebo 'exit' pro ukončení): ");
                string filePath = Console.ReadLine();

                if (filePath.ToLower() == "exit")
                    break;

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Soubor neexistuje. Zkuste to prosím znovu.");
                    continue;
                }

                try
                {
                    var csvData = ReadCsvFile(filePath);
                    AnalyzeCsvData(csvData);

                    while (true)
                    {
                        Console.WriteLine("\nCo chcete udělat dál?");
                        Console.WriteLine("1. Filtrovat data");
                        Console.WriteLine("2. Provést agregaci");
                        Console.WriteLine("3. Exportovat data");
                        Console.WriteLine("4. Načíst nový soubor");
                        Console.Write("Vaše volba (1-4): ");

                        string choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "1":
                                FilterData(csvData);
                                break;
                            case "2":
                                AggregateData(csvData);
                                break;
                            case "3":
                                ExportData(csvData);
                                break;
                            case "4":
                                goto OuterLoop;
                            default:
                                Console.WriteLine("Neplatná volba. Zkuste to prosím znovu.");
                                break;
                        }
                    }
                OuterLoop:;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Nastala chyba při zpracování souboru: {ex.Message}");
                }
            }

            Console.WriteLine("Děkujeme za použití CSV analyzátoru. Na shledanou!");
        }

        static List<Dictionary<string, string>> ReadCsvFile(string filePath)
        {
            var csvData = new List<Dictionary<string, string>>();
            string[] headers = null;

            foreach (var line in File.ReadLines(filePath))
            {
                if (headers == null)
                {
                    headers = line.Split(',');
                    continue;
                }

                var values = line.Split(',');
                if (values.Length != headers.Length)
                {
                    Console.WriteLine($"Varování: Řádek '{line}' má nesprávný počet sloupců. Bude přeskočen.");
                    continue;
                }

                var row = new Dictionary<string, string>();
                for (int i = 0; i < headers.Length; i++)
                {
                    row[headers[i]] = values[i];
                }
                csvData.Add(row);
            }

            return csvData;
        }

        static void AnalyzeCsvData(List<Dictionary<string, string>> csvData)
        {
            Console.WriteLine($"Počet řádků: {csvData.Count}");
            Console.WriteLine($"Počet sloupců: {csvData[0].Keys.Count}");
            Console.WriteLine("Názvy sloupců: " + string.Join(", ", csvData[0].Keys));
        }

        static void FilterData(List<Dictionary<string, string>> csvData)
        {
            Console.Write("Zadejte název sloupce pro filtrování: ");
            string column = Console.ReadLine();

            if (!csvData[0].ContainsKey(column))
            {
                Console.WriteLine("Zadaný sloupec neexistuje.");
                return;
            }

            Console.Write("Zadejte hodnotu pro filtrování: ");
            string value = Console.ReadLine();

            var filteredData = csvData.Where(row => row[column].Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();

            Console.WriteLine($"Počet nalezených záznamů: {filteredData.Count}");
            foreach (var row in filteredData.Take(5))
            {
                Console.WriteLine(string.Join(", ", row.Select(kv => $"{kv.Key}: {kv.Value}")));
            }
            if (filteredData.Count > 5)
            {
                Console.WriteLine("...");
            }
        }

        static void AggregateData(List<Dictionary<string, string>> csvData)
        {
            Console.Write("Zadejte název numerického sloupce pro agregaci: ");
            string column = Console.ReadLine();

            if (!csvData[0].ContainsKey(column))
            {
                Console.WriteLine("Zadaný sloupec neexistuje.");
                return;
            }

            var numericValues = csvData.Select(row =>
            {
                if (double.TryParse(row[column], out double value))
                    return value;
                return (double?)null;
            }).Where(v => v.HasValue).Select(v => v.Value).ToList();

            if (numericValues.Count == 0)
            {
                Console.WriteLine("Zadaný sloupec neobsahuje žádné numerické hodnoty.");
                return;
            }

            Console.WriteLine($"Součet: {numericValues.Sum()}");
            Console.WriteLine($"Průměr: {numericValues.Average()}");
            Console.WriteLine($"Minimum: {numericValues.Min()}");
            Console.WriteLine($"Maximum: {numericValues.Max()}");
        }

        static void ExportData(List<Dictionary<string, string>> csvData)
        {
            Console.Write("Zadejte název souboru pro export: ");
            string fileName = Console.ReadLine();

            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine(string.Join(",", csvData[0].Keys));

                foreach (var row in csvData)
                {
                    writer.WriteLine(string.Join(",", row.Values));
                }
            }

            Console.WriteLine($"Data byla exportována do souboru '{fileName}'.");
        }
    }
}
