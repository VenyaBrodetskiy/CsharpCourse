using System.Collections;

namespace EnumerableEnumerator;

internal class FibonacciSequence : IEnumerable<int>
{
    private readonly int _count;

    public FibonacciSequence(int count)
    {
        _count = count;
    }

    public IEnumerator<int> GetEnumerator()
    {
        return new FibonacciEnumerator(_count);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
