namespace BuilderPattern;

public abstract class Shape
{
    public int Length { get; set; }
    public string Colour { get; set; }
    public abstract string Type { get; }
}

// More detailed classes with a range of different
// parameter assignments based on the Shape base class
public class Square : Shape
{
    public override string Type => "Square";
}

public class Rectangle : Shape
{
    public override string Type => "Rectangle";
}