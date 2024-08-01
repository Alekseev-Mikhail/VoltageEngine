namespace Core;

public class Map(string tileSet, int width, char wallTile)
{
    public int Width => width;
    public readonly int Height = tileSet.Length / width;
    public char WallTile => wallTile;

    public char GetTile(int x, int y) => tileSet[width * y + x];

    public char GetTile(float x, float y) => tileSet[width * (int)y + (int)x];
};