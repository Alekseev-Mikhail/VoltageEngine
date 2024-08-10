using SFML.Graphics;
using SFML.Window;

namespace Client;

public class GameWindow
{
    private readonly RenderWindow _window;
    private readonly IViewport _viewport;
    private readonly Render _render;

    private Action? _onClosed;

    public GameWindow(
        uint width,
        uint height,
        string title,
        Styles style,
        uint antialiasingLevel,
        IViewport viewport
    )
    {
        var contextSettings = new ContextSettings { AntialiasingLevel = antialiasingLevel };
        _window = new RenderWindow(new VideoMode(width, height), title, style, contextSettings);
        _viewport = viewport;
        _render = new Render(_window);

        _window.SetKeyRepeatEnabled(false);
        _window.Closed += (_, _) => OnClose();
        _viewport.OnSetup(_render, _window.Size.X, _window.Size.Y);
    }

    public void StartBlocking()
    {
        while (_window.IsOpen)
        {
            _window.DispatchEvents();
            _window.Clear();
            _viewport.OnRender(_render, _window.Size.X, _window.Size.Y);
            _window.Display();
        }
    }

    public void BindKeyboardController(KeyboardController controller)
    {
        controller.OnSetup();
        _window.KeyPressed += (_, args) => controller.OnKeyPressed(args);
        _window.KeyReleased += (_, args) => controller.OnKeyReleased(args);
    }

    public void SetOnClosedAction(Action action) => _onClosed = action;

    private void OnClose()
    {
        _onClosed?.Invoke();
        _window.Dispose();
        _window.Close();
    }
}