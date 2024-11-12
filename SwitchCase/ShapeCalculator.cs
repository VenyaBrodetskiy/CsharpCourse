using BenchmarkDotNet.Attributes;

namespace SwitchCase;

[MemoryDiagnoser]
public class ShapeCalculator
{
    private readonly Shape[] _shapes =
    [
        new Circle(5),
        new Square(3),
        new Rectangle(4, 5),
    ];

    public static double CalculateAreaOld(Shape shape)
    {
        switch (shape)
        {
            case Circle circle:
                return Math.PI * Math.Pow(circle.Radius, 2);
            case Square square:
                return Math.Pow(square.Side, 2);
            case Rectangle rectangle:
                return rectangle.Length * rectangle.Width;
            default:
                return 0;
        }
    }

    public static double CalculateAreaNew(Shape shape)
    {
        return shape switch
        {
            Circle circle => Math.PI * Math.Pow(circle.Radius, 2),
            Square square => Math.Pow(square.Side, 2),
            Rectangle rectangle => rectangle.Length * rectangle.Width,
            _ => 0
        };
    }

    [Benchmark]
    public void SwitchCase()
    {
        foreach (var shape in _shapes)
        {
            ShapeCalculator.CalculateAreaOld(shape);
        }
    }

    [Benchmark]
    public void PatternMatching()
    {
        foreach (var shape in _shapes)
        {
            ShapeCalculator.CalculateAreaNew(shape);
        }
    }

    public static bool IsFitToAirplane(Rectangle rect)
    {
        return rect switch
        {
            { Length: > 10, Width: > 20 } or { Length: > 40 } => false,
            _ => true
        };
    }
}