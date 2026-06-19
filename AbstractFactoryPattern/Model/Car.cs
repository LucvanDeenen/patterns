
public abstract class Car
{
    public string Color { get; set; }
    
    public void Drive() 
    {
        Console.WriteLine("Vroom");
    }
    
    public abstract void DoSomething();
}

public class Convertable : Car
{
    public override void DoSomething()
    {
        Console.WriteLine("Convertable lowers roof");
    }
}

public class Sedan : Car
{
    public override void DoSomething()
    {
        Console.WriteLine("Sedan opens trunk");
    }
}