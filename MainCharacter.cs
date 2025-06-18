using Godot;

namespace Sandbox;

public partial class MainCharacter : CharacterBody2D
{
    private const float Gravity = 1000f;
    private const float MoveSpeed = 200f;

    public override void _PhysicsProcess(double delta)
    {
        var x = Input.IsActionPressed("move_right") ? 1 : Input.IsActionPressed("move_left") ? -1 : 0;
        var y = Velocity.Y + (!IsOnFloor() ? Gravity * (float)delta : 0);
        Velocity = new Vector2(x * MoveSpeed, y);
        MoveAndSlide();
    }
}