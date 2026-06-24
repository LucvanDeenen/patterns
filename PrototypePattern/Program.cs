using PrototypePattern;

var rectangle = new Rectangle();
rectangle.Height = 10;
rectangle.Width = 20;
rectangle.Colour = "Red";
Console.WriteLine("Creating original object: ");
Console.WriteLine($"Rectangle: {rectangle.Height}:{rectangle.Width}:{rectangle.Colour}");
Console.WriteLine();

// Assigning copy to rectangle meaning it's pointing to the same memory.
var copy = rectangle;
copy.Colour = "Orange";
Console.WriteLine("Assigned rectangle to copy var, and adjusted colour this causes both to change as it's a reference not new object:");
Console.WriteLine($"Copy: {copy.Colour}");
Console.WriteLine($"Rectangle: {rectangle.Colour}");
Console.WriteLine();

// Create a clone and a separate memory allocation instead.
copy = rectangle.Clone();
copy.Height = 100;
copy.Colour = "Blue";
rectangle.Colour = "Red";

// Made a new copy instead of a reference to the original object
// meaning they are no longer linked.
Console.WriteLine("Created a clone using the pattern implementation:");
Console.WriteLine($"Copy: {copy.Height}:{copy.Width}:{copy.Colour}");
Console.WriteLine($"Original: {rectangle.Height}:{rectangle.Width}:{rectangle.Colour}");
