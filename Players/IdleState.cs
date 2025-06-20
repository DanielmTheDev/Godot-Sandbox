using Godot;

namespace Sandbox.Players;

public sealed class IdleState : State
{
    private readonly AnimationPlayer _animPlayer;

    public IdleState(AnimationPlayer animPlayer)
    {
        _animPlayer = animPlayer;
    }

    public override void Enter(MainCharacter character) => _animPlayer.Play("idle");

    public override void Update(MainCharacter character, double delta)
    {
        if (Input.IsActionJustPressed("attack1") && character.IsOnFloor())
        {
            character.SwitchState(StateName.Attacking);
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