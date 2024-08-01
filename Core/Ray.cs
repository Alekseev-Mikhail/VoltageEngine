using SFML.System;

namespace Core;

public class Ray
{
    private Vector2f _target;

    public Vector2f Target => _target;
    public float Distance { get; private set; } = -1f;
    public bool IsWallExist { get; private set; }

    public void Update(float distance, float targetX, float targetY, bool isWallExist)
    {
        Distance = distance;
        _target.X = targetX;
        _target.Y = targetY;
        IsWallExist = isWallExist;
    }
    
    public void Copy(Ray ray)
    {
        Distance = ray.Distance;
        _target.X = ray.Target.X;
        _target.Y = ray.Target.Y;
        IsWallExist = ray.IsWallExist;
    }
}