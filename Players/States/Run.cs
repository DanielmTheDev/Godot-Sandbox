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

    public Run(AnimationPlayer animPlayer, Node2D visuals, InputProfile controls, MainCharacter character) : base(character)
    {
        _animPlayer = animPlayer;
        _visuals = visuals;
        _controls = controls;
    }

    protected override void OnEnter()
        => ProcessMovement();

    protected override void OnUpdate(double delta)
    {
        if (_controls.JumpJustPressed() && Character.IsOnFloor())
        {
            Character.SwitchState(StateName.Jumping);
        }

        else if (_controls.AttackJustPressed() && Character.IsOnFloor())
        {
            Character.SwitchState(StateName.Attacking);
        }

        else if (!(Input.IsActionPressed(_controls.MoveLeft) || Input.IsActionPressed(_controls.MoveRight)))
        {
            Character.Velocity = new Vector2(0, Character.Velocity.Y);
            Character.SwitchState(StateName.Idle);
        }
        else if (_controls.CastingJustPressed() && Character.IsOnFloor())
        {
            Character.SwitchState(StateName.CastingProjectile);
        }
        else
        {
            ProcessMovement();
        }
    }

    private void ProcessMovement()
    {
        var xMovement = _controls.GetXMovement();
        AdjustOrientation(xMovement);
        Character.Velocity = new Vector2(xMovement, Character.Velocity.Y);
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