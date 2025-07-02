using Godot;
using Sandbox.Players.Controls;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public sealed class Idle : State
{
    public override StateName StateName => StateName.Idle;
    private readonly AnimationPlayer _animPlayer;
    private readonly InputProfile _controls;

    public Idle(AnimationPlayer animPlayer, InputProfile controls)
    {
        _animPlayer = animPlayer;
        _controls = controls;
    }

    public override void Enter(MainCharacter character) => _animPlayer.Play("idle");

    public override void Update(MainCharacter character, double delta)
    {
        if (_controls.AttackJustPressed() && character.IsOnFloor())
        {
            character.SwitchState(StateName.Attacking);
        }
        else if (_controls.JumpJustPressed() && character.IsOnFloor())
        {
            character.SwitchState(StateName.Jumping);
        }
        else if (_controls.MoveLeftJustPressed() || _controls.MoveRightJustPressed())
        {
            character.SwitchState(StateName.Running);
        }
        else if (_controls.ParryJustPressed() && character.IsOnFloor())
        {
            character.SwitchState(StateName.Parrying);
        }
        else if (_controls.CastingJustPressed() && character.IsOnFloor())
        {
            character.SwitchState(StateName.CastingProjectile);
        }
        else
        {
            _animPlayer.Play("idle");
        }
    }
}