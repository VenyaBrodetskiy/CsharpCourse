namespace SwitchCase;

public abstract class Shape
{
    public virtual void WhoAmI()
    {
        Console.WriteLine("I am a shape");
    }
}

public class Circle(double radius) : Shape
{
    public double Radius { get; } = radius;

    public override void WhoAmI()
    {
        Console.WriteLine("I am a circle");
    }
}

public class Square(double side) : Shape
{
    public double Side { get; } = side;

    public override void WhoAmI()
    {
        Console.WriteLine("I am a square");
    }
}

public class Rectangle(double length, double width) : Shape
{
    public double Length { get; } = length;
    public double Width { get; } = width;

    public override void WhoAmI()
    {
        Console.WriteLine("I am a rectangle");
    }
}