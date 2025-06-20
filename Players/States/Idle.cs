using Godot;

namespace Sandbox.Players.States;

public sealed class Idle : State
{
    private readonly AnimationPlayer _animPlayer;

    public Idle(AnimationPlayer animPlayer)
    {
        _animPlayer = animPlayer;
    }

    public override void Enter(Scripts.MainCharacter character) => _animPlayer.Play("idle");

    public override void Update(Scripts.MainCharacter character, double delta)
    {
        if (Input.IsActionJustPressed("attack1") && character.IsOnFloor())
        {
            character.SwitchState(StateName.Attacking);
        }
        else if (Input.IsActionJustPressed("jump") && character.IsOnFloor())
        {
            character.SwitchState(StateName.Jumping);
        }
        else if (Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right"))
        {
            character.SwitchState(StateName.Running);
        }
        else
        {
            _animPlayer.Play("idle");
        }
    }
}