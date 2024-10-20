class Calculator
{
    // Method with return type and parameters
    public int Add(int a, int b)
    {
        return a + b;
    }

    // Void method
    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    // Method with optional parameter
    public double CalculateArea(double length, double width = 1.0)
    {
        return length * width;
    }

    // Method overloading
    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public double Multiply(double a, double b)
    {
        return a * b;
    }

    // Method with out parameter
    public void Divide(int dividend, int divisor, out int quotient, out int remainder)
    {
        quotient = dividend / divisor;
        remainder = dividend % divisor;
    }
}

class Program
{
    /*static void Main()
    {
        Calculator calc = new Calculator();

        // Calling methods
        int sum = calc.Add(5, 3);
        Console.WriteLine($"Sum: {sum}");

        calc.PrintMessage("Hello, World!");

        double area1 = calc.CalculateArea(5.0);
        double area2 = calc.CalculateArea(4.0, 3.0);
        Console.WriteLine($"Area 1: {area1}, Area 2: {area2}");

        int product1 = calc.Multiply(2, 3);
        double product2 = calc.Multiply(2.5, 3.0);
        Console.WriteLine($"Product 1: {product1}, Product 2: {product2}");

        calc.Divide(10, 3, out int quotient, out int remainder);
        Console.WriteLine($"10 divided by 3: Quotient = {quotient}, Remainder = {remainder}");
    }*/
}