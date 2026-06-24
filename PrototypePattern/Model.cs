namespace PrototypePattern;

public abstract class Shape
{
    public int Width { get; set; }
    public int Height { get; set; }

    protected Shape() { }
    
    protected Shape(Shape shape)
    {
        Width = shape.Width;
        Height = shape.Height;
    }

    public abstract Shape Clone();
}

public class Rectangle : Shape
{
    public string Colour { get; set; }

    public Rectangle()
    {
    }

    private Rectangle(Rectangle source) : base(source)
    {
        Colour = source.Colour;
    }
    
    public override Rectangle Clone()
    {
        return new Rectangle(this);
    }
}