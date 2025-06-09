using Godot;

namespace Sandbox;

public partial class FirstSprite : Sprite2D
{
    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.W))
        {
            Position += new Vector2(0, -20);
        }

        if (Input.IsKeyPressed(Key.A))
        {
            Position += new Vector2(-5, 0);
        }

        if (Input.IsKeyPressed(Key.S))
        {
            Position += new Vector2(0, 5);
        }

        if (Input.IsKeyPressed(Key.D))
        {
            Position += new Vector2(5, 0);
        }
    }
}