using Godot;
using Sandbox.Players.Controls;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public sealed class Idle : State
{
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
        if (Input.IsActionJustPressed(_controls.Attack1) && character.IsOnFloor())
        {
            character.SwitchState(StateName.Attacking);
        }
        else if (Input.IsActionJustPressed(_controls.Jump) && character.IsOnFloor())
        {
            character.SwitchState(StateName.Jumping);
        }
        else if (Input.IsActionPressed(_controls.MoveLeft) || Input.IsActionPressed(_controls.MoveRight))
        {
            character.SwitchState(StateName.Running);
        }
        else
        {
            _animPlayer.Play("idle");
        }
    }
}