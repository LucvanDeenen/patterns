namespace BridgePattern;

public class Remote
{
    private readonly IDevice _device;

    public Remote(IDevice device)
    {
        _device = device;
    }

    public void TogglePower()
    {
        if (_device.IsEnabled())
            _device.TurnOff();
        else
            _device.TurnOn();
    }
}