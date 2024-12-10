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

    public int this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                throw new IndexOutOfRangeException();
            }
            return GetFibonacciNumber(index);
        }
    }

    private int GetFibonacciNumber(int index)
    {
        if (index == 0)
        {
            return 0;
        }

        if (index == 1)
        {
            return 1;
        }

        return GetFibonacciNumber(index - 1) + GetFibonacciNumber(index - 2);
    }
}
