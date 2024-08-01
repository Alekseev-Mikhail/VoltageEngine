using SFML.Window;
using Timer = System.Timers.Timer;

namespace Client;

public abstract class KeyboardController
{
    private const int TimerInterval = 10;

    private readonly Dictionary<Keyboard.Key, Timer> _repeatKeys = new();

    public abstract void OnSetup();

    public abstract void OnKeyPressed(KeyEventArgs args);

    public abstract void OnKeyReleased(KeyEventArgs args);

    protected void AddRepeatKey(Keyboard.Key key, Action callback)
    {
        var timer = new Timer(TimerInterval);
        timer.Elapsed += (_, _) => callback.Invoke();
        _repeatKeys[key] = timer;
    }

    protected void TurnOnRepeatKey(Keyboard.Key key)
    {
        if (_repeatKeys.TryGetValue(key, out var value)) value.Start();
    }

    protected void TurnOffRepeatKey(Keyboard.Key key)
    {
        if (_repeatKeys.TryGetValue(key, out var value)) value.Stop();
    }
}