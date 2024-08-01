namespace Core;

public class Player(float x, float y, float direction, float velocity)
{
    public float X = x;
    public float Y = y;
    public float Velocity => velocity;

    public float Direction
    {
        set
        {
            if (value > 360f)
            {
                direction = 0f;
                return;
            }

            if (value < 0f)
            {
                direction = 360f;
                return;
            }

            direction = value;
        }
        get => direction;
    }
}