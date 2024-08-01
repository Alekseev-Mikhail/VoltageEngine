using SFML.Graphics;
using SFML.System;

namespace Client;

public class Render(RenderWindow window)
{
    private Vector2f _vector;
    private List<Shape> _shapePresets = new();
    private List<Vertex[]> _linePresets = new();

    public int AddPreset(float sizeX, float sizeY)
    {
        _vector.X = sizeX;
        _vector.Y = sizeY;
        _shapePresets.Add(new RectangleShape(_vector));
        return _shapePresets.Count - 1;
    }
    
    public int AddPreset(float radius)
    {
        _shapePresets.Add(new CircleShape(radius));
        return _shapePresets.Count - 1;
    }
    
    public int AddPreset(Color startColor, Color endColor)
    {
        _linePresets.Add([new Vertex(), new Vertex()]);
        var index = _linePresets.Count - 1;
        _linePresets[index][0].Color = startColor;
        _linePresets[index][1].Color = endColor;
        return index;
    }
    
    public void Perform(int presetIndex, float x, float y)
    {
        var preset = _shapePresets[presetIndex];

        _vector.X = x;
        _vector.Y = y;
        preset.Position = _vector;
        
        window.Draw(preset);
    }
    
    public void Perform(int presetIndex, float startX, float startY, float targetX, float targetY)
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