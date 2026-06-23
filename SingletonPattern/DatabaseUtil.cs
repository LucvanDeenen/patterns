namespace Singleton;

public static class DatabaseUtil
{
    private static readonly MockDbContext Instance = new();

    public static void Init()
    {
        if (string.IsNullOrEmpty(Instance.ConnectionString))
        {
            Instance.ConnectionString =
                "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        }
    }

    public static MockDbContext GetInstance()
    {
        if (string.IsNullOrEmpty(Instance.ConnectionString))
        {
            Console.WriteLine("[DB] DatabaseUtil is not initialized. Call init() before accessing the instance.");
            throw new InvalidOperationException(
                "DatabaseUtil is not initialized. Call init() before accessing the instance.");
        }

        return Instance;
    }
}

public class MockDbContext : IMockDb
{
    public string? ConnectionString { get; set; }

    public string Read()
    {
        Console.WriteLine("[DB] Performing read");
        return "Quack!";
    }

    public void Write(string input)
    {
        Console.WriteLine("[DB] Performing write: " + input);
    }

    public void Delete(string input)
    {
        Console.WriteLine("[DB] Performing delete: " + input);
    }
}

public interface IMockDb
{
    string Read();
    void Write(string input);
    void Delete(string input);
}