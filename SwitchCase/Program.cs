using SwitchCase;

var shapes = new Shape[]
{
    new Circle(5),
    new Square(3),
    new Rectangle(4, 5),
};

var calculator = new ShapeCalculator();

foreach (var shape in shapes)
{
    var areaOld = ShapeCalculator.CalculateAreaOld(shape);
    var areaNew = ShapeCalculator.CalculateAreaNew(shape);
    shape.WhoAmI();
    Console.WriteLine($"Area old: {areaOld}");
    Console.WriteLine($"Area new: {areaNew}");
}

var rect1 = new Rectangle(5, 10);
var rect2 = new Rectangle(50, 10);

Console.WriteLine($"Is rect1 fit to airplane: {ShapeCalculator.IsFitToAirplane(rect1)}");
Console.WriteLine($"Is rect2 fit to airplane: {ShapeCalculator.IsFitToAirplane(rect2)}");