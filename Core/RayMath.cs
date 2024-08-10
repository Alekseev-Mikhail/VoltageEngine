namespace Core;

public class RayMath
{
    public Ray ResultRay = new();
    
    public void Release(IPlayer player, Map map, float angle, float maxDistance, float stepLength)
    {
        for (float distance = 0; distance < maxDistance; distance += stepLength)
        {
            Step(player, map, angle, distance);
            if (!ResultRay.IsWallExist) continue;
            return;
        }
    }

    public void Step(IPlayer player, Map map, float angle, float distance)
    {
        var angleInRadians = float.DegreesToRadians(angle);
        var deltaX = MathF.Cos(angleInRadians) * distance;
        var deltaY = MathF.Sin(angleInRadians) * distance;
        var targetX = player.X + deltaX;
        var targetY = player.Y + deltaY;
        ResultRay.Update(distance, targetX, targetY, map.GetTile(targetX, targetY) == map.WallTile);
    }
}