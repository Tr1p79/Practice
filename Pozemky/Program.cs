using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace LandManagementSystem
{
    public class LandRecord
    {
        public string Id { get; set; }
        public decimal Area { get; set; }
        public decimal Share { get; set; }
    }

    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Type { get; set; } // "Nakup" nebo "Prodej"
        public List<LandRecord> Records { get; set; }

        public Transaction()
        {
            Records = new List<LandRecord>();
        }
    }

    public class Program
    {
        static List<Transaction> ParseFile(string filePath)
        {
            var transactions = new List<Transaction>();
            var lines = File.ReadAllLines(filePath);
            Transaction currentTransaction = null;

            foreach (var line in lines)
            {
                Console.WriteLine($"Zpracovávám řádek: '{line}'"); // Debug výpis

                if (string.IsNullOrWhiteSpace(line)) continue;

                // Kontrola, zda řádek začíná "Nakup" nebo "Prodej"
                if (line.StartsWith("Nakup") || line.StartsWith("Prodej"))
                {
                    var parts = line.Split('\t');
                    Console.WriteLine($"Našel jsem hlavičku transakce: {line}"); // Debug výpis

                    currentTransaction = new Transaction
                    {
                        Type = parts[0],
                        Date = DateTime.ParseExact(parts[1], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    };
                    transactions.Add(currentTransaction);
                }
                else if (currentTransaction != null)
                {
                    // Zpracování záznamu pozemku
                    var recordParts = line.Split(new[] { '\t'  }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine($"Parsování záznamu pozemku: {line}"); // Debug výpis
                    Console.WriteLine($"Počet částí: {recordParts.Length}"); // Debug výpis

                    if (recordParts.Length == 3)
                    {
                        try
                        {
                            var record = new LandRecord
                            {
                                Id = recordParts[0],
                                Area = decimal.Parse(recordParts[1], CultureInfo.InvariantCulture),
                                Share = decimal.Parse(recordParts[2], CultureInfo.InvariantCulture)
                            };
                            currentTransaction.Records.Add(record);
                            Console.WriteLine($"Úspěšně přidán záznam: ID={record.Id}, Area={record.Area}, Share={record.Share}"); // Debug výpis
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Chyba při parsování záznamu: {line}");
                            Console.WriteLine($"Detaily chyby: {ex.Message}");
                        }
                    }
                }
            }

            // Debug výpis pro kontrolu načtených dat
            foreach (var trans in transactions)
            {
                Console.WriteLine($"Transakce {trans.Type} z {trans.Date:yyyy-MM-dd} má {trans.Records.Count} záznamů");
            }

            return transactions;
        }

        static void DisplayTransactionDetails(Transaction transaction)
        {
            Console.WriteLine($"\nTyp transakce: {transaction.Type}");
            Console.WriteLine($"Datum: {transaction.Date:dd.MM.yyyy}");
            Console.WriteLine("\nSeznam pozemků:");
            Console.WriteLine("ID Pozemku\tVýměra (m²)\tPodíl");
            Console.WriteLine(new string('-', 50));

            foreach (var record in transaction.Records)
            {
                Console.WriteLine($"{record.Id}\t\t{record.Area}\t\t{record.Share:P0}");
            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Celková výměra: {transaction.Records.Sum(r => r.Area * r.Share):F2} m²");
            Console.WriteLine($"Počet pozemků: {transaction.Records.Count}");
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Začínám načítat soubor...");
                var transactions = ParseFile("C:\\02_ReposCore\\09_Practice\\Practice\\Pozemky\\test.txt");
                Console.WriteLine($"Načteno {transactions.Count} transakcí.");

                while (true)
                {
                    Console.WriteLine("\nZadejte datum pro zobrazení transakcí (ve formátu DD.MM.YYYY):");
                    var input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input)) break;

                    if (DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out DateTime searchDate))
                    {
                        var foundTransactions = transactions
                            .Where(t => t.Date.Date == searchDate.Date)
                            .ToList();

                        if (foundTransactions.Any())
                        {
                            foreach (var transaction in foundTransactions)
                            {
                                DisplayTransactionDetails(transaction);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Pro zadané datum nebyly nalezeny žádné transakce.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Neplatný formát data. Použijte formát DD.MM.YYYY");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Došlo k chybě: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }
    }
}