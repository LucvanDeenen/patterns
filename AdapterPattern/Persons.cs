namespace AdapterPattern;

public interface IGreets
{
    string SayHello();
    string SayGoodbye();
}

public class EnglishPersons : IGreets
{
    public string SayHello()
    {
        return "Hello!";
    }

    public string SayGoodbye()
    {
        return "Goodbye!";
    }
}

public class FrenchPerson : IGreets
{
    public string SayHello()
    {
        return "Bonjour!";
    }

    public string SayGoodbye()
    {
        return "Au revoir!";
    }
}

public class DutchPerson : IGreets
{
    public string SayHello()
    {
        return "Hallo!";
    }

    public string SayGoodbye()
    {
        return "Tot ziens!";
    }
}