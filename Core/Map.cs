namespace Core;

public class Map(string tileSet, int width, char wallTile)
{
    public string TileSet => tileSet;
    public int Width => width;
    public readonly int Height = tileSet.Length / width;
    public char WallTile => wallTile;

    public char GetTile(float x, float y) => GetTile((int)x, (int)y);

    public char GetTile(int x, int y)
    {
        var index = width * y + x;
        return tileSet.Length > index ? tileSet[index] : tileSet[0];
    }
};