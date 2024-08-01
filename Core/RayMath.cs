namespace Core;

public class RayMath(Player player, Map map)
{
    public Ray ResultRay = new();
    
    public void Release(float angle, float maxDistance, float stepLength)
    {
        for (float distance = 0; distance < maxDistance; distance += stepLength)
        {
            Step(angle, distance);
            if (!ResultRay.IsWallExist) continue;
            return;
        }
    }

    public void Step(float angle, float distance)
    {
        var deltaX = MathF.Cos(angle) * distance;
        var deltaY = MathF.Sin(angle) * distance;
        var targetX = player.X + deltaX;
        var targetY = player.Y + deltaY;
        ResultRay.Update(distance, targetX, targetY, map.GetTile(targetX, targetY) == map.WallTile);
    }
}