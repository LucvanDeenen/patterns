namespace AdapterPattern;

public class FrenchAdapter : IGreets
{
    private FrenchPerson _frenchPerson;

    public FrenchAdapter(FrenchPerson frenchPerson)
    {
        _frenchPerson = frenchPerson;
    }

    public string SayHello() => _frenchPerson.SayHello();
    public string SayGoodbye() => _frenchPerson.SayGoodbye();
}

public class EnglishAdapter : IGreets
{
    private EnglishPersons _englishPerson;

    public EnglishAdapter(EnglishPersons englishPerson)
    {
        _englishPerson = englishPerson;
    }

    public string SayHello() => _englishPerson.SayHello();
    public string SayGoodbye() => _englishPerson.SayGoodbye();
}

public class DutchAdapter : IGreets
{
    private DutchPerson _dutchPerson;

    public DutchAdapter(DutchPerson dutchPerson)
    {
        _dutchPerson = dutchPerson;
    }

    public string SayHello() => _dutchPerson.SayHello();
    public string SayGoodbye() => _dutchPerson.SayGoodbye();
}