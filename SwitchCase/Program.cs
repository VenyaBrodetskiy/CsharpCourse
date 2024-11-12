using SwitchCase;

var shapes = new Shape[]
{
    new Circle(5),
    new Square(3),
    new Rectangle(4, 5),
};

//var calculator = new ShapeCalculator();

foreach (var shape in shapes)
{
    shape.WhoAmI();
}