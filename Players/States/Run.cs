using Godot;
using Sandbox.Players.Controls;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public sealed class Run : State
{
    public override StateName StateName => StateName.Running;
    private readonly AnimationPlayer _animPlayer;
    private readonly Node2D _visuals;
    private readonly InputProfile _controls;

    public Run(AnimationPlayer animPlayer, Node2D visuals, InputProfile controls)
    {
        _animPlayer = animPlayer;
        _visuals = visuals;
        _controls = controls;
    }

    public override void Enter(MainCharacter character)
        => ProcessMovement(character);

    public override void Update(MainCharacter character, double delta)
    {
        if (_controls.JumpJustPressed() && character.IsOnFloor())
        {
            character.SwitchState(StateName.Jumping);
        }

        else if (_controls.AttackJustPressed() && character.IsOnFloor())
        {
            character.SwitchState(StateName.Attacking);
        }

        else if (!(Input.IsActionPressed(_controls.MoveLeft) || Input.IsActionPressed(_controls.MoveRight)))
        {
            character.Velocity = new Vector2(0, character.Velocity.Y);
            character.SwitchState(StateName.Idle);
        }
        else if (_controls.CastingJustPressed() && character.IsOnFloor())
        {
            character.SwitchState(StateName.CastingProjectile);
        }
        else
        {
            ProcessMovement(character);
        }
    }

    private void ProcessMovement(MainCharacter character)
    {
        var xMovement = _controls.GetXMovement();
        AdjustOrientation(xMovement);
        character.Velocity = new Vector2(xMovement, character.Velocity.Y);
        _animPlayer.Play("run");
    }

    private void AdjustOrientation(float xMovement)
    {
        var scaleX = xMovement switch
        {
            > 0 => 1,
            < 0 => -1,
            _ => _visuals.Scale.X
        };
        _visuals.Scale = new Vector2(scaleX, _visuals.Scale.Y);
    }
}