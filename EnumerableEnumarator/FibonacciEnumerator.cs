using System.Collections;

namespace EnumerableEnumerator;

internal class FibonacciEnumerator : IEnumerator<int>
{
    private readonly int _count;
    private int _currentIndex = -1;
    private int _prev;
    private int _current;

    public FibonacciEnumerator(int count)
    {
        _count = count;
    }

    public int Current => _current;

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        _currentIndex++;

        if (_currentIndex >= _count)
        {
            return false;
        }

        if (_currentIndex == 0)
        {
            _current = 0;
            return true;
        }

        if (_currentIndex == 1)
        {
            _current = 1;
            return true;
        }

        var next = _prev + _current;
        _prev = _current;
        _current = next;
        return true;
    }

    public void Reset()
    {
        _currentIndex = -1;
        _prev = 0;
        _current = 1;
    }

    public void Dispose()
    {
    }
}
