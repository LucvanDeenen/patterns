namespace SimUDuck;

public interface IFlyable
{
    void Fly();
}

public class NoFly : IFlyable
{
    public void Fly()
    {
        Console.WriteLine("I can't fly");
    }
}

public class AnimalFly : IFlyable
{
    public void Fly()
    {
        Console.WriteLine("I'm flying");   
    }
}