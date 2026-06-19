public interface ICarFactory
{
    Car CreateSedan();
    
    Car CreateConvertable();
}

public class DutchCarFactory : ICarFactory
{
    public Car CreateConvertable()
    {
        return new Convertable();
    }

    public Car CreateSedan()
    {
        return new Sedan();
    }
}

public class UsaCarFactory : ICarFactory
{
    public Car CreateConvertable()
    {
        return new Convertable();
    }

    public Car CreateSedan()
    {
        return new Sedan();
    }
}