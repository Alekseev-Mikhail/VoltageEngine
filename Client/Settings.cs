using SFML.Graphics;

namespace Client;

public class Settings(float graphicQuality, float renderDistance, float fov, float sensitivity)
{
    private RenderWindow _window = null!;
    
    public float GraphicQuality = graphicQuality;
    public float RenderDistance = renderDistance;
    public float FOV = fov;
    public float Sensitivity = sensitivity;

    public bool VerticalSync
    {
        set => _window.SetVerticalSyncEnabled(value);
    }

    public void SetWindow(RenderWindow window) => _window = window;
}