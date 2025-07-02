using Godot;

namespace Sandbox.Players.Controls;

public class InputProfile
{
    private readonly string _player;
    private const float MoveSpeed = 200f;


    public string MoveLeft => $"{_player}move_left";
    public string MoveRight => $"{_player}move_right";
    public string Jump => $"{_player}jump";
    public string Attack1 => $"{_player}attack1";
    public string Parry => $"{_player}parry";
    public string CastingProjectile => $"{_player}cast_projectile";

    public InputProfile(Player player) => _player = player.ControlsPrefix();

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