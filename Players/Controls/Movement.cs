using Godot;

namespace Sandbox.Players.Controls;

public static class Movement
{
    private const float MoveSpeed = 200f;

    public static float GetX()
    {
        var raw = Input.IsActionPressed("move_right")
            ? 1
            : Input.IsActionPressed("move_left")
                ? -1
                : 0;
        return raw * MoveSpeed;
    }
}