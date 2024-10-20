using System;
using System.Collections.Generic;
using System.Linq;

class Task
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
}

enum Priority
{
    Low,
    Medium,
    High
}

class TaskManager
{
    private List<Task> tasks = new List<Task>();

    public void AddTask(string name, string description, Priority priority, DateTime dueDate)
    {
        tasks.Add(new Task
        {
            Name = name,
            Description = description,
            Priority = priority,
            DueDate = dueDate,
            IsCompleted = false
        });
        Console.WriteLine("Úkol byl úspěšně přidán.");
    }

    public void DisplayTasks()
    {
        var sortedTasks = tasks.OrderByDescending(t => t.Priority).ThenBy(t => t.DueDate).ToList();
        foreach (var task in sortedTasks)
        {
            Console.WriteLine($"Název: {task.Name}");
            Console.WriteLine($"Popis: {task.Description}");
            Console.WriteLine($"Priorita: {task.Priority}");
            Console.WriteLine($"Termín: {task.DueDate.ToShortDateString()}");
            Console.WriteLine($"Stav: {(task.IsCompleted ? "Dokončeno" : "Nedokončeno")}");
            Console.WriteLine();
        }
    }

    public void MarkTaskAsCompleted(string name)
    {
        var task = tasks.FirstOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (task != null)
        {
            task.IsCompleted = true;
            Console.WriteLine("Úkol byl označen jako dokončený.");
        }
        else
        {
            Console.WriteLine("Úkol s tímto názvem nebyl nalezen.");
        }
    }

    public void SearchTasks(string keyword)
    {
        var matchingTasks = tasks.Where(t =>
            t.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

        if (matchingTasks.Any())
        {
            Console.WriteLine($"Nalezeno {matchingTasks.Count} úkolů:");
            foreach (var task in matchingTasks)
            {
                Console.WriteLine($"- {task.Name}");
            }
        }
        else
        {
            Console.WriteLine("Žádné úkoly nebyly nalezeny.");
        }
    }

    public void GenerateWeeklyReport()
    {
        var today = DateTime.Today;
        var endOfWeek = today.AddDays(7 - (int)today.DayOfWeek);
        var tasksThisWeek = tasks.Where(t => t.DueDate <= endOfWeek && !t.IsCompleted).ToList();

        Console.WriteLine("Týdenní přehled úkolů:");
        foreach (var task in tasksThisWeek.OrderBy(t => t.DueDate))
        {
            Console.WriteLine($"- {task.Name} (Termín: {task.DueDate.ToShortDateString()}, Priorita: {task.Priority})");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        TaskManager manager = new TaskManager();

        while (true)
        {
            Console.WriteLine("\nSprávce úkolů - Menu:");
            Console.WriteLine("1. Přidat nový úkol");
            Console.WriteLine("2. Zobrazit všechny úkoly");
            Console.WriteLine("3. Označit úkol jako dokončený");
            Console.WriteLine("4. Vyhledat úkoly");
            Console.WriteLine("5. Vygenerovat týdenní přehled");
            Console.WriteLine("6. Ukončit program");
            Console.Write("Vyberte možnost (1-6): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddNewTask(manager);
                    break;
                case "2":
                    manager.DisplayTasks();
                    break;
                case "3":
                    MarkTaskAsCompleted(manager);
                    break;
                case "4":
                    SearchTasks(manager);
                    break;
                case "5":
                    manager.GenerateWeeklyReport();
                    break;
                case "6":
                    Console.WriteLine("Děkujeme za použití Správce úkolů. Na shledanou!");
                    return;
                default:
                    Console.WriteLine("Neplatná volba. Zkuste to prosím znovu.");
                    break;
            }
        }
    }

    static void AddNewTask(TaskManager manager)
    {
        Console.Write("Název úkolu: ");
        string name = Console.ReadLine();

        Console.Write("Popis úkolu: ");
        string description = Console.ReadLine();

        Console.Write("Priorita (Low/Medium/High): ");
        Priority priority = Enum.Parse<Priority>(Console.ReadLine(), true);

        Console.Write("Termín (YYYY-MM-DD): ");
        DateTime dueDate = DateTime.Parse(Console.ReadLine());

        manager.AddTask(name, description, priority, dueDate);
    }

    static void MarkTaskAsCompleted(TaskManager manager)
    {
        Console.Write("Zadejte název úkolu k označení jako dokončený: ");
        string taskName = Console.ReadLine();
        manager.MarkTaskAsCompleted(taskName);
    }

    static void SearchTasks(TaskManager manager)
    {
        Console.Write("Zadejte klíčové slovo pro vyhledávání: ");
        string keyword = Console.ReadLine();
        manager.SearchTasks(keyword);
    }
}