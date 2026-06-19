namespace SimUDuck;

public interface IQuackable
{
    void Quack();
}

public class Quacker : IQuackable
{
    public void Quack()
    {
        Console.WriteLine("Quack");
    }
}

public class Squeeker : IQuackable
{
    public void Quack()
    {
        Console.WriteLine("Squeek");
    }
}