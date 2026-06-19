public class Utils
{
    private static int value;

    public static void AddOne()
    {
        if (value == null)
        {
            init();
        }
        
        value++;
    }
    
    private static void init()
    {
        value = 0;
    }
}

public static  class Program
{
    public static void Main()
    {
        Utils.AddOne();
    }
}