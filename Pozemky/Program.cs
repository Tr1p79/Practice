using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Security;

namespace Pozemky
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var transactions = ParseFile("C:\\02_ReposCore\\09_Practice\\Practice\\Pozemky\\test.txt");
                
                while (true) 
                {
                    Console.WriteLine("\nZadejte datum pro zobrazení transakcí (ve formátu DD.MM.YYYY):");
                    var input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input)) break;

                    if(DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture,
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
            catch (Exception e) 
            {
                Console.WriteLine($"Došlo k chybě: {e.Message}");
            }

        }

        static List<Transaction> ParseFile(string filePath)
        {
            var transactions = new List<Transaction>();
            var lines = File.ReadAllLines(filePath);

            Transaction currentTransaction = null;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                // Detekce hlavičky transakce (obsahuje tabulátor)
                if (line.Contains('\t'))
                {
                    var parts = line.Split('\t');
                    currentTransaction = new Transaction
                    {
                        Type = parts[0],
                        Date = DateTime.ParseExact(parts[1], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    };
                    transactions.Add(currentTransaction);
                }
                // Zpracování záznamu o pozemku
                else if (currentTransaction != null)
                {
                    var recordParts = line.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (recordParts.Length == 3)
                    {
                        currentTransaction.Records.Add(new LandRecord
                        {
                            Id = recordParts[0],
                            Area = decimal.Parse(recordParts[1], CultureInfo.InvariantCulture),
                            Share = decimal.Parse(recordParts[2], CultureInfo.InvariantCulture)
                        });
                    }
                }
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
    }

    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Type { get; set; } //Nakup,Prodej

        public List<LandRecord> Records { get; set; }

        public Transaction() 
        {
            Records = new List<LandRecord>();
        }
    }

    public class LandRecord
    {
        public string Id { get; set; }
        public decimal Area {  get; set; }
        public decimal Share { get; set; }

    }

}
