using Godot;
using Sandbox.Players.Controls;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public sealed class Idle : State
{
    public override StateName StateName => StateName.Idle;
    private readonly AnimationPlayer _animPlayer;
    private readonly InputProfile _controls;

    public Idle(AnimationPlayer animPlayer, InputProfile controls, MainCharacter character) : base(character)
    {
        _animPlayer = animPlayer;
        _controls = controls;
    }

    protected override void OnEnter() => _animPlayer.Play("idle");

    protected override void OnUpdate(double delta)
    {
        if (_controls.AttackJustPressed() && Character.IsOnFloor())
        {
            Character.SwitchState(StateName.Attacking);
        }
        else if (_controls.JumpJustPressed() && Character.IsOnFloor())
        {
            Character.SwitchState(StateName.Jumping);
        }
        else if (_controls.ParryJustPressed() && Character.IsOnFloor())
        {
            Character.SwitchState(StateName.Parrying);
        }
        else if (_controls.CastingJustPressed() && Character.IsOnFloor())
        {
            Character.SwitchState(StateName.CastingProjectile);
        }
        else if (_controls.MoveLeftJustPressed() || _controls.MoveRightJustPressed())
        {
            Character.SwitchState(StateName.Running);
        }
        else
        {
            _animPlayer.Play("idle");
        }
    }
}