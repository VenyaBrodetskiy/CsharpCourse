using BenchmarkDotNet.Attributes;
// ReSharper disable UnusedVariable

namespace ParallelFor;

[MemoryDiagnoser]
public class LoopCompare
{
    private readonly List<int> _data = Enumerable.Range(0, 1000).ToList();

    private static void Calculate(int item)
    {
        //var result = Math.Sqrt(item);
        double result = 0;
        for (int i = 0; i < 1000; i++)
        {
            result += Math.Sqrt(item) * Math.Sin(i);
        }
    }

    [Benchmark]
    public void ForLoop()
    {
        foreach (var item in _data)
        {
            Calculate(item);
        }
    }

    [Benchmark]
    public void ParallelForLoop()
    {
        Parallel.ForEach(_data, Calculate);
    }

    [Benchmark]
    public async Task TaskWhenAll()
    {
        var tasks = _data.Select(item => 
            Task.Run(() => Calculate(item)));
        await Task.WhenAll(tasks);
    }
}
