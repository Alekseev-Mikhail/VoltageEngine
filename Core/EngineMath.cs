namespace Core;

public static class EngineMath
{
    public static float DistanceBetweenTwoPoints(float x1, float x2, float y1, float y2) =>
        float.RootN(float.Pow(x1 - x2, 2) + float.Pow(y1 - y2, 2), 2);
}