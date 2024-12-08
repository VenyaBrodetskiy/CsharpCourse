using EnumerableEnumerator;

var fibonacci = new FibonacciSequence(10);

Console.WriteLine("Fibonacci numbers");
foreach (var number in fibonacci)
{
    Console.WriteLine("Number: " + number);
}

// using enumerator
Console.WriteLine("Fibonacci numbers using enumerator");
using var enumerator = fibonacci.GetEnumerator();
    while (enumerator.MoveNext())
    {
        Console.WriteLine("Number: " + enumerator.Current);
    }
}