using BuilderPattern;

// Setup director (this is optional for the pattern) but
// allows forced initialization order
var director = new Director();

// Utilize the builders
var squareBuilder = new SquareBuilder();
var firstShape = director.ConstructSquare(squareBuilder);
Console.WriteLine($"{firstShape.Colour} {firstShape.Type}");

var rectangleBuilder = new RectangleBuilder();
var secondShape = director.ConstructRectangle(rectangleBuilder);
Console.WriteLine($"{secondShape.Colour} {secondShape.Type}");
