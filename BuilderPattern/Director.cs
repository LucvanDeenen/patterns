namespace BuilderPattern;

public class Director
{
    public Shape ConstructSquare(ShapeBuilder builder)
    {
        builder.SetColour("Red");
        builder.SetArea(5);
        return builder.GetProduct();
    } 
    
    public Shape ConstructRectangle(ShapeBuilder builder)
    {
        builder.SetColour("Blue");
        builder.SetArea(20);
        return builder.GetProduct();
    } 
}