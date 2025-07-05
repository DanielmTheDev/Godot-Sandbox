using Godot;
using Sandbox.Players.Controls;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public sealed class Jump : State
{
    public override StateName StateName => StateName.Jumping;
    private const float JumpVelocity = -500f;
    private readonly AnimationPlayer _animPlayer;
    private readonly InputProfile _controls;

    public Jump(AnimationPlayer animPlayer, InputProfile controls, MainCharacter character) : base(character)
    {
        _animPlayer = animPlayer;
        _controls = controls;
    }

    protected override void OnEnter()
    {
        Character.Velocity = new Vector2(Character.Velocity.X, JumpVelocity);
        _animPlayer.Play("up");
    }

    protected override void OnUpdate(double delta)
    {
        if (Character.Velocity.Y > 0)
        {
            _animPlayer.Play("down");
        }

        Character.Velocity = new Vector2(_controls.GetXMovement(), Character.Velocity.Y);
        if (Character.IsOnFloor())
        {
            Character.SwitchState(StateName.Idle);
        }
    }
}