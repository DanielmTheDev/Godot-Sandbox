using Godot;
using Sandbox.Players.States;

namespace Sandbox.Players.Controls;

public class InputProfile
{
    private readonly string _prefix;
    private const float MoveSpeed = 200f;


    public string MoveLeft => $"{_prefix}move_left";
    public string MoveRight => $"{_prefix}move_right";
    public string Jump => $"{_prefix}jump";
    public string Attack1 => $"{_prefix}attack1";
    public string Parry => $"{_prefix}parry";
    public string CastingProjectile => $"{_prefix}cast_projectile";

    public InputProfile(string prefix) => _prefix = prefix;

    public float GetXMovement()
    {
        var raw = Input.IsActionPressed(MoveRight)
            ? 1
            : Input.IsActionPressed(MoveLeft)
                ? -1
                : 0;
        return raw * MoveSpeed;
    }

    public bool AttackJustPressed() => Input.IsActionJustPressed(Attack1);
    public bool MoveLeftJustPressed() => Input.IsActionJustPressed(MoveLeft);
    public bool MoveRightJustPressed() => Input.IsActionJustPressed(MoveRight);
    public bool JumpJustPressed() => Input.IsActionJustPressed(Jump);
    public bool ParryJustPressed() => Input.IsActionJustPressed(Parry);
    public bool CastingJustPressed() => Input.IsActionJustPressed(CastingProjectile);
}