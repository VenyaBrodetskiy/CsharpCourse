using EnumerableEnumerator;

var fibonacci = new FibonacciSequence(10);

Console.WriteLine("Fibonacci numbers");
foreach (var number in fibonacci)
{
    Console.WriteLine("Number: " + number);
}

Console.WriteLine("Access fibonacci number by index");
Console.WriteLine("Number at index 7: " + fibonacci[7]);

// using enumerator
Console.WriteLine("Fibonacci numbers using enumerator");
using var enumerator = fibonacci.GetEnumerator();
    while (enumerator.MoveNext())
    {
        Console.WriteLine("Number: " + enumerator.Current);
    }