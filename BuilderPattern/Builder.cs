namespace BuilderPattern;

// Builder interface with multiple implementations for the different models
// Add customization in each of the implementing interfaces to facilitate the model
public interface ShapeBuilder
{
    Shape GetProduct();
    void SetArea(int length);
    void SetColour(string color);
}

public class SquareBuilder : ShapeBuilder
{
    private readonly Shape _shape = new Square();

    public Shape GetProduct()
    {
        return _shape;
    }

    public void SetArea(int length)
    {
        _shape.Length = length;
    }

    public void SetColour(string color)
    {
        _shape.Colour = color;
    }
}

public class RectangleBuilder : ShapeBuilder
{
    private readonly Shape _shape = new Rectangle();

    public Shape GetProduct()
    {
        return _shape;
    }

    public void SetArea(int length)
    {
        _shape.Length = length;
    }

    public void SetColour(string color)
    {
        _shape.Colour = color;
    }
}