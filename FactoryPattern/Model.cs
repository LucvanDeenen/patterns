using System;
using System.Threading;

namespace FactoryPattern;

public interface IContainer
{
    string GetName();
    
    void Boot();
    
    void Display()
    {
        var name = GetName();
        Console.WriteLine(name + " is running");    
    }
    
    void Setup()
    {
        var name = GetName();
        Console.WriteLine("Installing " + name);
        
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("Installing...");
            Thread.Sleep(100); // Simulate installation time
        }
        
        Console.WriteLine("Installation of " + name + " complete.");
    }
}

public class DockerContainer : IContainer
{
    private const string Name = "Docker";
    
    public string GetName()
    {
        return Name;
    }
    
    public void Boot()
    {
        Console.WriteLine("Booting Docker...");
    }
}

public class PodmanContainer : IContainer 
{
    private const string Name = "Podman";

    public string GetName()
    {
        return Name;
    }
    
    public void Boot()
    {
        Console.WriteLine("Booting Podman...");
    }
}