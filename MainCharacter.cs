using Godot;

namespace Sandbox;

public partial class MainCharacter : CharacterBody2D
{
    private const float Gravity = 1000f;
    private const float MoveSpeed = 200f;

    public override void _PhysicsProcess(double delta)
    {
        SetVelocity(delta);
        MoveAndSlide();
    }

    private void SetVelocity(double delta)
    {
        var x = GetXMovement();
        var y = GetYMovement(delta);
        Velocity = new Vector2(x, y);
    }

    private float GetYMovement(double delta)
        => Velocity.Y + (!IsOnFloor()
            ? Gravity * (float)delta
            : 0);

    private static float GetXMovement()
    {
        var raw = Input.IsActionPressed("move_right")
            ? 1
            : Input.IsActionPressed("move_left")
                ? -1
                : 0;
        return raw * MoveSpeed;
    }
}