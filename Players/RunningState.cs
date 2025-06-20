using Godot;

namespace Sandbox.Players;

public sealed class RunningState : State
{
    private const float MoveSpeed = 200f;
    private readonly AnimationPlayer _animPlayer;
    private readonly Sprite2D _sprite;

    public RunningState(AnimationPlayer animPlayer, Sprite2D sprite)
    {
        _animPlayer = animPlayer;
        _sprite = sprite;
    }

    public override void Enter(MainCharacter character)
        => ProcessMovement(character);

    public override void Update(MainCharacter character, double delta)
    {
        if (Input.IsActionJustPressed("attack1") && character.IsOnFloor())
        {
            character.SwitchState(StateName.Attacking);
        }

        else if (!(Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right")))
        {
            character.SwitchState(StateName.Idle);
            ProcessMovement(character);
        }
    }

    private void ProcessMovement(MainCharacter character)
    {
        var x = GetXMovement();
        _sprite.FlipH = GetOrientation(x);
        _animPlayer.Play("run");
        character.Velocity = new Vector2(x, character.Velocity.Y);
        character.MoveAndSlide();
    }

    private static float GetXMovement()
    {
        var raw = Input.IsActionPressed("move_right")
            ? 1
            : Input.IsActionPressed("move_left")
                ? -1
                : 0;
        return raw * MoveSpeed;
    }

    private bool GetOrientation(float x)
        => x switch
        {
            > 0 => false,
            < 0 => true,
            _ => _sprite.FlipH
        };
}