using Godot;

namespace Sandbox.Players.Controls;

public class InputProfile
{
    private readonly string _prefix;
    private const float MoveSpeed = 200f;


    public string MoveLeft => $"{_prefix}move_left";
    public string MoveRight => $"{_prefix}move_right";
    public string Jump => $"{_prefix}jump";
    public string Attack1 => $"{_prefix}attack1";

    public InputProfile(string prefix) => _prefix = prefix;

    public float GetX()
    {
        var raw = Input.IsActionPressed(MoveRight)
            ? 1
            : Input.IsActionPressed(MoveLeft)
                ? -1
                : 0;
        return raw * MoveSpeed;
    }
}