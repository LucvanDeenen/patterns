ICarFactory GetCarFactory(string location) {
    ICarFactory factory;
    if (location.Equals("Netherlands"))
    {
        factory = new DutchCarFactory(); 
    }
    else
    {
        factory = new UsaCarFactory();
    }

    return factory;
}

var factory = GetCarFactory("Netherlands");
var convertable = factory.CreateConvertable();
convertable.Drive();

var sedan = factory.CreateSedan();
sedan.Drive();
