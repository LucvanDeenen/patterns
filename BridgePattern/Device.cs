namespace BridgePattern;

public interface IDevice
{
    bool IsEnabled();
    void TurnOff();
    void TurnOn();
}

public class TV : IDevice
{
    private bool _state = false;
    
    public bool IsEnabled()
    {
        return _state;
    }

    public void TurnOff()
    {
        _state = false;
    }

    public void TurnOn()
    {
        _state = true;
    }
}