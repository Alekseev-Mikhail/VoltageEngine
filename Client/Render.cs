using SFML.Graphics;
using SFML.System;

namespace Client;

public class Render(RenderWindow window)
{
    private Vector2f _vector;
    private readonly List<RectangleShape> _rectanglePresets = [];
    private readonly List<CircleShape> _circlePresets = [];
    private readonly List<Vertex[]> _linePresets = [];

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
    
    public int AddLinePreset(Color startColor, Color endColor)
    {
        _linePresets.Add([new Vertex(), new Vertex()]);
        var index = _linePresets.Count - 1;
        _linePresets[index][0].Color = startColor;
        _linePresets[index][1].Color = endColor;
        return index;
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
    
    public void PerformLine(int presetIndex, float startX, float startY, float targetX, float targetY)
    {
        var preset = _linePresets[presetIndex];
        preset[0].Position.X = startX;
        preset[0].Position.Y = startY;
        preset[1].Position.X = targetX;
        preset[1].Position.Y = targetY;
        window.Draw(preset, 0, 2, PrimitiveType.Lines);
        window.ResetGLStates();
    }
}