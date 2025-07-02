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

    public Jump(AnimationPlayer animPlayer, InputProfile controls)
    {
        _animPlayer = animPlayer;
        _controls = controls;
    }

    public override void Enter(MainCharacter character)
    {
        character.Velocity = new Vector2(character.Velocity.X, JumpVelocity);
        _animPlayer.Play("up");
    }

    public override void Update(MainCharacter character, double delta)
    {
        if (character.Velocity.Y > 0)
        {
            _animPlayer.Play("down");
        }

        character.Velocity = new Vector2(_controls.GetXMovement(), character.Velocity.Y);
        if (character.IsOnFloor())
        {
            character.SwitchState(StateName.Idle);
        }
    }
}