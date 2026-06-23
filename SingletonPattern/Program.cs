using Singleton;

MockDbContext context;
try
{
    // This will throw an exception
    context = DatabaseUtil.GetInstance();
}
catch (Exception e)
{
    Console.WriteLine("Caught Exception: " + e.Message);
}

DatabaseUtil.Init();
context = DatabaseUtil.GetInstance();
context.Read(); // This will work now
