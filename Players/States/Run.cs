using Godot;
using Sandbox.Players.Controls;

namespace Sandbox.Players.States;

public sealed class Run : State
{
    private readonly AnimationPlayer _animPlayer;
    private readonly Sprite2D _sprite;

    public Run(AnimationPlayer animPlayer, Sprite2D sprite)
    {
        _animPlayer = animPlayer;
        _sprite = sprite;
    }

    public override void Enter(Scripts.MainCharacter character)
        => ProcessMovement(character);

    public override void Update(Scripts.MainCharacter character, double delta)
    {
        if (Input.IsActionJustPressed("jump") && character.IsOnFloor())
        {
            character.SwitchState(StateName.Jumping);
        }

        else if (Input.IsActionJustPressed("attack1") && character.IsOnFloor())
        {
            character.SwitchState(StateName.Attacking);
        }

        else if (!(Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right")))
        {
            character.Velocity = new Vector2(0, character.Velocity.Y);
            character.SwitchState(StateName.Idle);
        }
        else
        {
            ProcessMovement(character);
        }
    }

    private void ProcessMovement(Scripts.MainCharacter character)
    {
        var x = Movement.GetX();
        _sprite.FlipH = GetOrientation(x);
        _animPlayer.Play("run");
        character.Velocity = new Vector2(x, character.Velocity.Y);
    }

    private bool GetOrientation(float x)
        => x switch
        {
            > 0 => false,
            < 0 => true,
            _ => _sprite.FlipH
        };
}