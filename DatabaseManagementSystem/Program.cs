namespace DatabaseManagementSystem
{
    internal class Program
    {
        static Dictionary<string, Table> database = new Dictionary<string, Table>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write($"Enter a command (or 'exit' to quit): {10 % 5}");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    break;
                }

                ProcessCommand(input);
            }

            Console.WriteLine("See ya!");
        }

        static void ProcessCommand(string command) 
        {
            string[] parts = command.Split(' ');

            if (parts[0].ToUpper() == "CREATE" && parts[1].ToUpper() == "TABLE" ) 
            {
                CreateTable(command);
            }
            else if (parts[0].ToUpper() == "INSERT" && parts[1].ToUpper() == "INTO")
            {
                InsertInto(command);
            }
            else
            {
                Console.WriteLine("Unknown command. Please try again.");
            }
        }

        static void CreateTable(string command)
        {
            int openParenIndex = command.IndexOf('(');
            int closeParenIndex = command.LastIndexOf(")");

            if (openParenIndex == -1 || closeParenIndex == -1)
            {
                Console.WriteLine("Invalid CREATE TABLE syntax");
                return;
            }

            string tableName = command.Substring(13, openParenIndex - 13).Trim();
            string columnDefinitions = command.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1);
            string[] columns = columnDefinitions.Split(',');

            Table newTable = new Table();

            foreach (string col in columns)
            {
                string[] colParts = col.Trim().Split(' ');
                if (colParts.Length != 2)
                {
                    Console.WriteLine($"Invalid column definition: {col}");
                    return;
                }

                string colName = colParts[0];
                string colType = colParts[1].ToLower();
                Type dataType;

                switch(colType) 
                {
                    case "int":
                        dataType = typeof(int);
                        break;
                    case "float": 
                        dataType = typeof(float);
                        break;
                    case "double":
                        dataType = typeof(double);
                        break;
                    default:
                        Console.WriteLine($"Unsupported data type: {colType}");
                        return;
                }

                newTable.Columns.Add(new Column { Name = colName, DataType = dataType});
            }

            database[tableName] = newTable;
            Console.WriteLine($"Table '{tableName}' created successfully.");
        }

        static void InsertInto(string command) 
        {   
            int valuesIndex = command.ToUpper().IndexOf("VALUES");
            if (valuesIndex == -1)
            {
                Console.WriteLine("Invalid INSERT INTO syntax.");
                return;
            }

            string tableName = command.Substring(12, valuesIndex - 12).Trim();

            if(!database.ContainsKey(tableName)) 
            {
                Console.WriteLine($"Table '{tableName}' does not exist.");
                return;
            }

            Table table = database[tableName];

            string valuesStr = command.Substring(valuesIndex + 7).Trim();
            if (!valuesStr.StartsWith("(") || !valuesStr.EndsWith(")"))
            {
                Console.WriteLine("Invalid VALUES syntax.");
                return;
            }

            valuesStr = valuesStr.Substring(1, valuesStr.Length - 2);
            string[] values = valuesStr.Split(',');

            if (values.Length != table.Columns.Count)
            {
                Console.WriteLine("Number of values does not match number of columns.");
                return;
            }

            Dictionary<string, object> newRow = new Dictionary<string, object>();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                Column col = table.Columns[i];
                string value = values[i].Trim();

                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    value = value.Substring(1, value.Length - 2);
                }

                object parsedValue;

                if (col.DataType == typeof(int))
                {
                    if (!int.TryParse(value, out int intValue))
                    {
                        Console.WriteLine($"Invalid integer value for column '{col.Name}': {value}");
                        return;
                    }
                    parsedValue = intValue;
                }
                else if (col.DataType == typeof(double))
                {
                    if (!double.TryParse(value, out double doubleValue))
                    {
                        Console.WriteLine($"Invalid double value for column '{col.Name}': {value}");
                        return;
                    }
                    parsedValue = doubleValue;
                }
                else // string
                {
                    parsedValue = value;
                }

                newRow[col.Name] = parsedValue;
            }

            table.Rows.Add(newRow);
            Console.WriteLine("Row inserted successfully.");
        }
    }

    class Table
    {
        public List<Column> Columns { get; set; } = new List<Column>();
        public List<Dictionary<string, object>> Rows { get; set; }

        public Table()
        {   
            Columns = new List<Column>();
            Rows = new List<Dictionary<string, object>>();
        }
    }

    class Column
    {
        public string Name { get; set; } = string.Empty;
        public Type DataType { get; set; }
    }
}
