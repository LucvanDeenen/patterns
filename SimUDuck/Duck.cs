namespace SimUDuck;

public abstract class Program
{
    private static readonly List<Duck> DuckCollection = [
        new RedheadDuck(),
        new RubberDuck()
    ];
    
    public static void Main()
    {
        DuckCollection.ForEach(duck =>
        {
            duck.Display();
            duck.Quack();
            duck.Swim();
            duck.Fly();
        });                
    }
}

public abstract class Duck
{
    protected IQuackable Quackable;

    protected IFlyable Flyable;

    public void Quack()
    {
        Quackable.Quack();
    }

    public void Fly()
    {
        Flyable.Fly();
    }
    
    public void Swim()
    {
        Console.WriteLine("All ducks float, even decoys");
    }
    
    public abstract void Display();
}

public class RubberDuck : Duck
{
    public RubberDuck()
    {
        Flyable = new NoFly();
        Quackable = new Squeeker();
    }
    
    public override void Display()
    {
        Console.WriteLine("I'm a real rubber duck");
    }
}

public class RedheadDuck : Duck
{
    public RedheadDuck()
    {
        Flyable = new AnimalFly();
        Quackable = new Quacker();
    }
    
    public override void Display()
    {
        Console.WriteLine("I'm a real Redhead duck");
    }
}
