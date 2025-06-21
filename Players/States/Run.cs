using Godot;
using Sandbox.Players.Controls;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public sealed class Run : State
{
    private readonly AnimationPlayer _animPlayer;
    private readonly Sprite2D _sprite;
    private readonly InputProfile _controls;

    public Run(AnimationPlayer animPlayer, Sprite2D sprite, InputProfile controls)
    {
        _animPlayer = animPlayer;
        _sprite = sprite;
        _controls = controls;
    }

    public override void Enter(MainCharacter character)
        => ProcessMovement(character);

    public override void Update(MainCharacter character, double delta)
    {
        if (Input.IsActionJustPressed(_controls.Jump) && character.IsOnFloor())
        {
            character.SwitchState(StateName.Jumping);
        }

        else if (Input.IsActionJustPressed(_controls.Attack1) && character.IsOnFloor())
        {
            character.SwitchState(StateName.Attacking);
        }

        else if (!(Input.IsActionPressed(_controls.MoveLeft) || Input.IsActionPressed(_controls.MoveRight)))
        {
            character.Velocity = new Vector2(0, character.Velocity.Y);
            character.SwitchState(StateName.Idle);
        }
        else
        {
            ProcessMovement(character);
        }
    }

    private void ProcessMovement(MainCharacter character)
    {
        var x = _controls.GetX();
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