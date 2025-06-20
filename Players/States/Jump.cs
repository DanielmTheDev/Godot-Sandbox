using Godot;
using Sandbox.Players.Controls;

namespace Sandbox.Players.States;

public sealed class Jump : State
{
    private const float JumpVelocity = -500f;
    private readonly AnimationPlayer _animPlayer;

    public Jump(AnimationPlayer animPlayer)
    {
        _animPlayer = animPlayer;
    }

    public override void Enter(Scripts.MainCharacter character)
    {
        character.Velocity = new Vector2(character.Velocity.X, JumpVelocity);
        _animPlayer.Play("up");
    }

    public override void Update(Scripts.MainCharacter character, double delta)
    {
        if (character.Velocity.Y > 0)
        {
            _animPlayer.Play("down");
        }

        character.Velocity = new Vector2(Movement.GetX(), character.Velocity.Y);
        if (character.IsOnFloor())
        {
            character.SwitchState(StateName.Idle);
        }
    }
}