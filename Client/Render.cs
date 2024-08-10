using SFML.Graphics;
using SFML.System;

namespace Client;

public class Render(RenderWindow window)
{
    private Vector2f _vector;
    private readonly List<RectangleShape> _rectanglePresets = [];
    private readonly List<CircleShape> _circlePresets = [];
    private readonly Vertex[] _point = [new Vertex()];
    private readonly Vertex[] _line = [new Vertex(), new Vertex()];

    public int AddRectanglePreset()
    {
        _rectanglePresets.Add(new RectangleShape());
        return _rectanglePresets.Count - 1;
    }
    
    public int AddCirclePreset(float radius)
    {
        _circlePresets.Add(new CircleShape(radius));
        return _circlePresets.Count - 1;
    }
    
    public void PerformRectangle(int presetIndex, float x, float y, float sizeX, float sizeY)
    {
        var preset = _rectanglePresets[presetIndex];

        _vector.X = x;
        _vector.Y = y;
        preset.Position = _vector;
        
        _vector.X = sizeX;
        _vector.Y = sizeY;
        preset.Size = _vector;
        
        window.Draw(preset);
    }
    
    public void PerformCircle(int presetIndex, float x, float y)
    {
        var preset = _circlePresets[presetIndex];

        _vector.X = x;
        _vector.Y = y;
        preset.Position = _vector;
        
        window.Draw(preset);
    }
    
    public void PerformPoint(float x, float y, Color color)
    {
        _point[0].Position.X = x;
        _point[0].Position.Y = y;
        _point[0].Color = color;
        window.Draw(_point, 0, 1, PrimitiveType.Points);
    }
    
    public void PerformLine(float startX, float startY, float targetX, float targetY, Color color)
    {
        _line[0].Position.X = startX;
        _line[0].Position.Y = startY;
        _line[0].Color = color;
        _line[1].Position.X = targetX;
        _line[1].Position.Y = targetY;
        _line[1].Color = color;
        window.Draw(_line, 0, 2, PrimitiveType.Lines);
    }
}