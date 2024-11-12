namespace SwitchCase;

public class ShapeCalculator
{
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

    public static bool IsFitToAirplane(Rectangle rect)
    {
        return rect switch
        {
            { Length: > 10, Width: > 20 } or { Length: > 40 } => false,
            _ => true
        };
    }
}