using AdapterPattern;

List<IGreets> greeters =
[
    new EnglishAdapter(new EnglishPersons()),
    new FrenchAdapter(new FrenchPerson()),
    new DutchAdapter(new DutchPerson()),
];

foreach (var greeter in greeters)
{
    Console.WriteLine(greeter.SayHello());
    Console.WriteLine(greeter.SayGoodbye());
    Console.WriteLine();
}

